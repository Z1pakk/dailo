import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-habit-edit',
  imports: [],
  templateUrl: './habit-edit.html',
  styleUrl: './habit-edit.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HabitEdit {}
