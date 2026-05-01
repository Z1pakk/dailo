import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Button } from 'primeng/button';
import { Menu } from 'primeng/menu';
import { MenuItem } from 'primeng/api';
import { HabitModel } from '@habits/models/habit.model';
import { frequencyTypesLabels } from '@habits/enums/frequency-type.enum';

@Component({
  selector: 'app-habit-list-item',
  imports: [DatePipe, Button, Menu],
  templateUrl: './habit-list-item.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HabitListItem {
  readonly habit = input.required<HabitModel>();

  protected readonly frequencyTypesLabels = frequencyTypesLabels;

  protected readonly actionsItems: MenuItem[] = [
    { label: 'Edit', icon: 'pi pi-pencil' },
    { label: 'Delete', icon: 'pi pi-trash' },
  ];
}
