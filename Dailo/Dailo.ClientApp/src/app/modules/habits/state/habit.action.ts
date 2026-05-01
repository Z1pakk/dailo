import { CreateHabitRequestModel } from '@habits/models/requests/create-habit.request';

const scope = '[Habit]';

export class HabitGetHabits {
  static readonly type = `${scope} GetHabits`;
}

export class HabitCreateHabit {
  static readonly type = `${scope} CreateHabit`;

  constructor(public payload: CreateHabitRequestModel) {}
}
