import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from '@auth/requests/login.request';
import { LoginResponse } from '@auth/responses/login.response';
import { environment } from '@environment';
import { RegisterRequest } from '@auth/requests/register.request';
import { RegisterResponse } from '@auth/responses/register.response';
import { RefreshResponse } from '@auth/responses/refresh.response';

@Injectable({
  providedIn: 'root',
})
export class AuthApi {
  private readonly http = inject(HttpClient);

  private readonly baseUrl = environment.apiUrl;

  public login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/auth/login`, request);
  }

  public register(request: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(
      `${this.baseUrl}/auth/register`,
      request,
    );
  }

  public refresh(): Observable<RefreshResponse> {
    return this.http.post<RefreshResponse>(`${this.baseUrl}/auth/refresh`, {});
  }
}
