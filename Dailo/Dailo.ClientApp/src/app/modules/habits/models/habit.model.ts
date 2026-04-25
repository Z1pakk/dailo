export interface HabitModel {
  id: number;
  name: string;
  description?: string;
  createdAtUtc: Date;
  lastModifiedUtc: Date;
}
