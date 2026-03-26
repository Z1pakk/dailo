import { Routes } from '@angular/router';
import { authRoutes } from '@auth/auth.routes';
import { habitRoutes } from '@habits/habit.routes';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('@layout/main-layout/main-layout').then((m) => m.MainLayout),
    children: [...habitRoutes],
  },
  {
    path: '',
    loadComponent: () =>
      import('@layout/auth-layout/auth-layout').then((m) => m.AuthLayout),
    children: [...authRoutes],
  },
  ...habitRoutes,
  ...authRoutes,
  // otherwise redirect to home
  { path: '**', redirectTo: '' },
];
