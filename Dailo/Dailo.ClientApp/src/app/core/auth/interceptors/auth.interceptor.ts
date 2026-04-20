import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { AuthRefresh } from '@auth/state/auth.actions';
import { catchError, switchMap, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.url.includes('/auth/refresh')) {
    return next(req);
  }

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status !== 401) {
        return throwError(() => error);
      }

      const store = inject(Store);
      const router = inject(Router);

      return store.dispatch(new AuthRefresh()).pipe(
        switchMap(() => next(req)),
        catchError(() => {
          router.navigate(['/login']);
          return throwError(() => error);
        }),
      );
    }),
  );
};
