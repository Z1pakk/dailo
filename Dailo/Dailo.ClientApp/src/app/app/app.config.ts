import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideStore } from '@ngxs/store';
import { environment } from '@environment';
import Aura from '@primeuix/themes/aura';
import { providePrimeNG } from 'primeng/config';
import { states } from './states.const';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideStore([], {
      developmentMode: !environment.production,
    }),
    providePrimeNG({
      theme: { preset: Aura, options: { darkModeSelector: '.app-dark' } },
    }),
    provideStore(states, {
      developmentMode: !environment.production,
    }),
  ],
};
