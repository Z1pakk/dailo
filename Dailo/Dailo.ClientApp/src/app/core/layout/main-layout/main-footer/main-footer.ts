import { ChangeDetectionStrategy, Component } from '@angular/core';
import { LogoWidget } from '@shared/ui/logo-widget/logo-widget';

@Component({
  selector: 'app-main-footer',
  imports: [LogoWidget],
  templateUrl: './main-footer.html',
  styleUrl: './main-footer.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainFooter {
  protected readonly currentYear = new Date().getFullYear();
}
