import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainSidebar } from '@layout/main-layout/main-sidebar/main-sidebar';
import { MainTopbar } from '@layout/main-layout/main-topbar/main-topbar';
import { MainFooter } from '@layout/main-layout/main-footer/main-footer';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, MainSidebar, MainTopbar, MainFooter],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
})
export class MainLayout {}
