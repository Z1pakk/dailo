import { inject, Injectable } from '@angular/core';
import { Action, State, StateContext } from '@ngxs/store';
import { AuthLogin, AuthRefresh, AuthRegister } from '@auth/state/auth.action';
import { AuthApi } from '@auth/auth.api';
import { finalize, tap } from 'rxjs';
import { LoginResponse } from '@auth/responses/login.response';
import { RegisterResponse } from '@auth/responses/register.response';
import { RefreshResponse } from '@auth/responses/refresh.response';

export interface AuthStateModel {
  isLoading: boolean;
  isAuthenticated: boolean;
  authToken: string;
}

const defaultState: AuthStateModel = {
  isLoading: false,
  isAuthenticated: false,
  authToken: '',
};

@Injectable()
@State<AuthStateModel>({ name: 'authState', defaults: defaultState })
export class AuthState {
  private readonly _authApi = inject(AuthApi);

  @Action(AuthLogin)
  public login(ctx: StateContext<AuthStateModel>, action: AuthLogin) {
    ctx.patchState({
      isLoading: true,
    });

    return this._authApi.login(action.payload).pipe(
      tap({
        next: (result: LoginResponse) => {
          ctx.patchState({
            isAuthenticated: true,
            authToken: result.accessTokens.accessToken,
          });
        },
      }),
      finalize(() => ctx.patchState({ isLoading: false })),
    );
  }

  @Action(AuthRegister)
  public register(ctx: StateContext<AuthStateModel>, action: AuthRegister) {
    ctx.patchState({
      isLoading: true,
    });

    return this._authApi.register(action.payload).pipe(
      tap({
        next: (result: RegisterResponse) => {
          ctx.patchState({
            isAuthenticated: true,
            authToken: result.accessTokens.accessToken,
          });
        },
      }),
      finalize(() => ctx.patchState({ isLoading: false })),
    );
  }

  @Action(AuthRefresh)
  public refresh(ctx: StateContext<AuthStateModel>, action: AuthRefresh) {
    ctx.patchState({
      isLoading: true,
    });

    return this._authApi.refresh().pipe(
      tap({
        next: (result: RefreshResponse) => {
          ctx.patchState({
            isAuthenticated: true,
            authToken: result.accessTokens.accessToken,
          });
        },
      }),
      finalize(() => ctx.patchState({ isLoading: false })),
    );
  }
}
