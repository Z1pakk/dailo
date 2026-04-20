import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-entry-list',
  imports: [],
  templateUrl: './entry-list.html',
  styleUrl: './entry-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EntryList {}
