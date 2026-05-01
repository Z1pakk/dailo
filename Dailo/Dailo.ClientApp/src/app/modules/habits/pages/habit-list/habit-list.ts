import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { DataView } from 'primeng/dataview';
import { Store } from '@ngxs/store';
import { HabitGetHabits } from '@habits/state/habit.action';
import { HabitStateSelectors } from '@habits/state/habit.selector';
import { Button } from 'primeng/button';
import { DialogService } from 'primeng/dynamicdialog';
import { HabitAdd } from '@habits/pages/habit-add/habit-add';
import { HabitAddModalFooter } from '@habits/pages/habit-add/habit-add-modal-footer';
import { HabitListItem } from '@habits/pages/habit-list/ui/habit-list-item/habit-list-item';

@Component({
  selector: 'dailo',
  imports: [DataView, Button, HabitListItem],
  templateUrl: './habit-list.html',
  styleUrl: './habit-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HabitList implements OnInit {
  private readonly _store = inject(Store);
  private readonly _dialogService = inject(DialogService);

  protected readonly $habits = this._store.selectSignal(
    HabitStateSelectors.getSlices.habits,
  );

  ngOnInit() {
    this._store.dispatch(new HabitGetHabits());
  }

  protected addHabit() {
    this._dialogService.open(HabitAdd, {
      header: 'Create a new habit',
      width: '40rem',
      modal: true,
      closable: true,
      dismissableMask: false,
      keepInViewport: true,
      data: { $isFormValid: signal(false) },
      templates: {
        footer: HabitAddModalFooter,
      },
    });
  }

  protected viewHabit(id: string) {}
}
