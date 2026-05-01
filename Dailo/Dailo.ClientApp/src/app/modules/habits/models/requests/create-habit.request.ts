import { HabitType } from '@habits/enums/habit-type.enum';
import { FrequencyType } from '@habits/enums/frequency-type.enum';

export interface CreateHabitFrequencyModel {
  type: FrequencyType;
  timesPerPeriod: number;
}

export interface CreateHabitTargetModel {
  value: number;
  unit: string;
}

export interface CreateHabitMilestoneModel {
  target: number;
  current: number;
}

export interface CreateHabitRequestModel {
  name: string;
  description: string | null;
  type: HabitType;
  frequency: CreateHabitFrequencyModel;
  target: CreateHabitTargetModel;
  endDate: string | null;
  milestone: CreateHabitMilestoneModel | null;
  tagIds: string[];
}
