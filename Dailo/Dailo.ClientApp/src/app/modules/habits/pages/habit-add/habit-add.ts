import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-habit-add',
  imports: [],
  templateUrl: './habit-add.html',
  styleUrl: './habit-add.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HabitAdd {}
