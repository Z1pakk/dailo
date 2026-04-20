import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-tag-list',
  imports: [],
  templateUrl: './tag-list.html',
  styleUrl: './tag-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TagList {}
