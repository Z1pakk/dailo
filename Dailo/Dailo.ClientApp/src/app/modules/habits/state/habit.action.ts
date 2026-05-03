import { CreateHabitRequestModel } from '@habits/models/requests/create-habit.request';
import { UpdateHabitRequestModel } from '@habits/models/requests/update-habit.request';

const scope = '[Habit]';

export class HabitGetHabits {
  static readonly type = `${scope} GetHabits`;
}

export class HabitCreateHabit {
  static readonly type = `${scope} CreateHabit`;

  constructor(public payload: CreateHabitRequestModel) {}
}

export class HabitUpdateHabit {
  static readonly type = `${scope} UpdateHabit`;

  constructor(public id: number, public payload: UpdateHabitRequestModel) {}
}
