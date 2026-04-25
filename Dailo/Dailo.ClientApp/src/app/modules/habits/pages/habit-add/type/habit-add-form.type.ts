import * as v from 'valibot';
import { FormControl, FormGroup } from '@angular/forms';

export const HabitNameSchema = v.pipe(
  v.string(),
  v.nonEmpty('Name is required'),
  v.maxLength(100, 'Maximum of 100 characters'),
);

export const HabitDescriptionSchema = v.pipe(
  v.string(),
  v.maxLength(500, 'Maximum of 500 characters'),
);

export const HabitAddFormSchema = v.object({
  name: HabitNameSchema,
  description: HabitDescriptionSchema,
});

export type HabitAddFormValue = v.InferOutput<typeof HabitAddFormSchema>;

export type HabitAddForm = {
  name: FormControl<string>;
  description: FormControl<string>;
};

export type HabitAddFormGroup = FormGroup<HabitAddForm>;
