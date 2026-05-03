import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
} from '@angular/core';
import { Menu } from 'primeng/menu';
import { MenuItem, MenuItemCommandEvent } from 'primeng/api';
import { Store } from '@ngxs/store';
import { AuthLogout } from '@auth/state/auth.action';
import { MainSidebarService } from '@layout/service/main-sidebar.service';

@Component({
  selector: 'app-main-topbar',
  imports: [Menu],
  templateUrl: './main-topbar.html',
  styleUrl: './main-topbar.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainTopbar {
  private readonly _store = inject(Store);
  private readonly _mainSidebarService = inject(MainSidebarService);

  protected readonly $isMenuOpened = computed(() => this._mainSidebarService.$isMenuOpened());

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
      command: (_: MenuItemCommandEvent) => {
        this._store.dispatch(new AuthLogout());
      },
    },
  ];

  protected toggleMenu() {
    this._mainSidebarService.toggleMenu();
  }
}
