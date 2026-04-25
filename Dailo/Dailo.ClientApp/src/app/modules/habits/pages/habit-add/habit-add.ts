import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
} from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HabitAddFacadeService } from '@habits/pages/habit-add/habit-add-facade.service';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { InputText } from 'primeng/inputtext';

@Component({
  selector: 'app-habit-add',
  imports: [ReactiveFormsModule, InputText],
  providers: [HabitAddFacadeService],
  templateUrl: './habit-add.html',
  styleUrl: './habit-add.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HabitAdd {
  private readonly _config = inject(DynamicDialogConfig);
  private readonly _habitAddService = inject(HabitAddFacadeService);

  protected readonly addHabitForm = this._habitAddService.addHabitForm;

  constructor() {
    effect(() => {
      this._config.data.$isFormValid.set(this._habitAddService.$isFormValid());
    });
  }
}
