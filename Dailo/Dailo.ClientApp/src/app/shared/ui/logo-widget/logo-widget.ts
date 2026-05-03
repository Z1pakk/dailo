import {
  booleanAttribute,
  ChangeDetectionStrategy,
  Component,
  input,
} from '@angular/core';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-logo-widget',
  imports: [NgOptimizedImage],
  templateUrl: './logo-widget.html',
  styleUrl: './logo-widget.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LogoWidget {
  public readonly $widthPx = input(120, { alias: 'widthPx' });
  public readonly $heightPx = input(35, { alias: 'heightPx' });

  public readonly $isWhite = input(false, {
    transform: booleanAttribute,
    alias: 'isWhite',
  });
}
