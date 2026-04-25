import {
  ChangeDetectionStrategy,
  Component,
  inject,
  Signal,
} from '@angular/core';
import { Button } from 'primeng/button';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-habit-add-modal-footer',
  imports: [Button],
  template: ` <div class="flex gap-2">
    <p-button
      severity="primary"
      label="Add"
      [disabled]="!$isFormValid()"
      (click)="addNewHabit()"
    ></p-button>
    <p-button severity="secondary" label="Cancel" (click)="close()"></p-button>
  </div>`,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HabitAddModalFooter {
  private readonly _dialogRef = inject(DynamicDialogRef);
  private readonly _config = inject(DynamicDialogConfig);

  protected readonly $isFormValid: Signal<boolean> =
    this._config.data.$isFormValid;

  protected addNewHabit() {
    this._dialogRef.close();
  }

  protected close() {
    this._dialogRef.close();
  }
}
