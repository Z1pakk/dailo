import { inject, Injectable, Signal } from '@angular/core';
import { NonNullableFormBuilder } from '@angular/forms';
import {
  HabitAddForm,
  HabitAddFormGroup,
  HabitDescriptionSchema,
  HabitNameSchema,
} from '@habits/pages/habit-add/type/habit-add-form.type';
import { valibotValidator } from '@shared/lib/form/valibot.validator';
import { toSignal } from '@angular/core/rxjs-interop';
import { map, startWith } from 'rxjs';

@Injectable()
export class HabitAddFacadeService {
  private readonly _fb = inject(NonNullableFormBuilder);

  public readonly addHabitForm: HabitAddFormGroup = this._fb.group<HabitAddForm>({
    name: this._fb.control<string>('', valibotValidator(HabitNameSchema)),
    description: this._fb.control<string>(
      '',
      valibotValidator(HabitDescriptionSchema),
    ),
  });

  public readonly $isFormValid: Signal<boolean> = toSignal(
    this.addHabitForm.statusChanges.pipe(
      startWith(this.addHabitForm.status),
      map((status) => status === 'VALID'),
    ),
    { initialValue: false },
  );
}
