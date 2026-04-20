import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Menu } from 'primeng/menu';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-main-topbar',
  imports: [Menu],
  templateUrl: './main-topbar.html',
  styleUrl: './main-topbar.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainTopbar {
  protected readonly profileMenuItems: MenuItem[] = [
    {
      label: 'Profile',
      icon: 'pi pi-user',
    },
    {
      label: 'Settings',
      icon: 'pi pi-cog',
    },
    {
      label: 'Log out',
      icon: 'pi pi-power-off',
    },
  ];
}
