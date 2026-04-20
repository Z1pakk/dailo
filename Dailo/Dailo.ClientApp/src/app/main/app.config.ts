import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideStore } from '@ngxs/store';
import { environment } from '@environment';
import { providePrimeNG } from 'primeng/config';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { credentialsInterceptor } from '@main/interceptors/credentials.interceptor';
import { authInterceptor } from '@auth/interceptors/auth.interceptor';
import { DefaultPreset } from './default-preset';
import { AuthState } from '@auth/state/auth.state';
import { provideInitialAuth } from '@auth/providers/provide-initial-auth.provider';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(
      withInterceptors([credentialsInterceptor, authInterceptor]),
    ),
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideStore([AuthState], {
      developmentMode: !environment.isProduction,
    }),
    providePrimeNG({
      theme: {
        preset: DefaultPreset,
        options: {
          cssLayer: {
            name: 'primeng',
            order: 'theme, base, primeng',
          },
          darkModeSelector: '.app-dark',
        },
      },
    }),
    provideInitialAuth(),
  ],
};
