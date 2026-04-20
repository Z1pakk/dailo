import { Routes } from '@angular/router';

export const entryRoutes: Routes = [
  {
    title: 'Dailu - My Entries',
    path: 'entries',
    loadComponent: () =>
      import('./pages/entry-list/entry-list').then((m) => m.EntryList),
  },
];
