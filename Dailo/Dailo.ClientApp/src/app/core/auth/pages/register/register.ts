import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { Button } from 'primeng/button';
import { Checkbox } from 'primeng/checkbox';
import {
  FormsModule,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { InputText } from 'primeng/inputtext';
import { LogoWidget } from '@shared/ui/logo-widget/logo-widget';
import { Password } from 'primeng/password';
import { RouterLink } from '@angular/router';
import {
  valibotCrossFieldValidator,
  valibotValidator,
} from '@shared/lib/form/valibot.validator';
import { AuthRegister } from '@auth/state/auth.actions';
import { Store } from '@ngxs/store';
import { AuthRouterService } from '@auth/services/auth-router.service';
import {
  AcceptedPrivacyTermsSchema,
  RegisterConfirmPasswordSchema,
  RegisterEmailSchema,
  RegisterFirstNameSchema,
  RegisterForm,
  RegisterFormGroup,
  RegisterFormValue,
  RegisterLastNameSchema,
  RegisterPasswordSchema,
} from '@auth/pages/register/types/register-form.type';
import { RegisterRequest } from '@auth/requests/register.request';
import { markAllAsDirty } from '@shared/lib/form/mark-as-dirty';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-register',
  imports: [
    Button,
    Checkbox,
    FormsModule,
    InputText,
    LogoWidget,
    Password,
    ReactiveFormsModule,
    RouterLink,
  ],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register implements OnInit {
  private readonly _store = inject(Store);
  private readonly _authRouterService = inject(AuthRouterService);
  private readonly _fb = inject(NonNullableFormBuilder);
  private readonly _destroyRef = inject(DestroyRef);

  protected readonly registerForm: RegisterFormGroup =
    this._fb.group<RegisterForm>({
      email: this._fb.control<string>('', {
        validators: valibotValidator(RegisterEmailSchema),
        updateOn: 'blur',
      }),
      firstName: this._fb.control<string>(
        '',
        valibotValidator(RegisterFirstNameSchema),
      ),
      lastName: this._fb.control<string>(
        '',
        valibotValidator(RegisterLastNameSchema),
      ),
      password: this._fb.control<string>(
        '',
        valibotValidator(RegisterPasswordSchema),
      ),
      confirmPassword: this._fb.control<string>(
        '',
        valibotCrossFieldValidator('password', RegisterConfirmPasswordSchema),
      ),
      isAcceptedPrivacyTerms: this._fb.control<boolean>(
        false,
        valibotValidator(AcceptedPrivacyTermsSchema),
      ),
    });

  ngOnInit() {
    this.registerForm.controls.password.valueChanges
      .pipe(takeUntilDestroyed(this._destroyRef))
      .subscribe(() => {
        this.registerForm.controls.confirmPassword.updateValueAndValidity();
      });
  }

  protected emailInput(event: Event) {
    const control = this.registerForm.controls.email;
    if (control.touched) {
      control.setValue((event.target as HTMLInputElement).value);
    }
  }

  protected register() {
    markAllAsDirty(this.registerForm);

    if (this.registerForm.valid) {
      const value: RegisterFormValue = this.registerForm.getRawValue();

      const loginRequest = (<RegisterRequest>{
        email: value.email,
        firstName: value.firstName,
        lastName: value.lastName,
        password: value.password,
        confirmPassword: value.confirmPassword,
        isAcceptedPrivacyTerms: value.isAcceptedPrivacyTerms,
      }) satisfies RegisterRequest;

      this._store.dispatch(new AuthRegister(loginRequest)).subscribe({
        next: () => {
          this._authRouterService.goToMainAppPage();
        },
      });
    }
  }
}
