import { Component, inject } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Password } from 'primeng/password';
import { Checkbox } from 'primeng/checkbox';
import { Button } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { RouterLink } from '@angular/router';
import { LogoWidget } from '@shared/ui/logo-widget/logo-widget';
import { Store } from '@ngxs/store';
import { valibotValidator } from '@shared/lib/form/valibot.validator';
import {
  LoginEmailSchema,
  LoginForm,
  LoginFormGroup,
  LoginFormValue,
  LoginPasswordSchema,
} from './types/login-form.type';
import { AuthLogin } from '@auth/state/auth.actions';
import { LoginRequest } from '@auth/requests/login.request';
import { AuthRouterService } from '@auth/services/auth-router.service';
import { markAllAsDirty } from '@shared/lib/form/mark-as-dirty';

@Component({
  selector: 'app-login',
  imports: [
    Checkbox,
    Button,
    ReactiveFormsModule,
    InputText,
    RouterLink,
    LogoWidget,
    Password,
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private readonly _store = inject(Store);
  private readonly _authRouterService = inject(AuthRouterService);
  private readonly _fb = inject(NonNullableFormBuilder);

  protected readonly loginForm: LoginFormGroup = this._fb.group<LoginForm>({
    email: this._fb.control<string>('', {
      validators: valibotValidator(LoginEmailSchema),
      updateOn: 'blur',
    }),
    password: this._fb.control('', valibotValidator(LoginPasswordSchema)),
    isRememberMe: this._fb.control(false),
  });

  protected emailInput(event: Event) {
    const control = this.loginForm.controls.email;
    if (control.touched) {
      control.setValue((event.target as HTMLInputElement).value);
    }
  }

  protected login() {
    markAllAsDirty(this.loginForm);

    if (this.loginForm.valid) {
      const value: LoginFormValue = this.loginForm.getRawValue();

      const loginRequest = (<LoginRequest>{
        email: value.email,
        password: value.password,
      }) satisfies LoginRequest;

      this._store.dispatch(new AuthLogin(loginRequest)).subscribe({
        next: () => {
          this._authRouterService.goToMainAppPage();
        },
      });
    }
  }
}
