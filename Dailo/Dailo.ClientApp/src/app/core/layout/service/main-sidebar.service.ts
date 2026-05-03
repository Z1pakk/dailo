import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class MainSidebarService {
  private readonly _isMenuOpened = signal(true);

  public readonly $isMenuOpened = this._isMenuOpened.asReadonly();

  public toggleMenu() {
    this._isMenuOpened.update((isOpened) => !isOpened);
  }
}
