import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'dailo',
  imports: [],
  templateUrl: './habit-list.html',
  styleUrl: './habit-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HabitList {}
