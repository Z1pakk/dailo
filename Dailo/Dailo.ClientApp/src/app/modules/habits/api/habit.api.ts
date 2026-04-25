import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environment';
import { Observable } from 'rxjs';
import { GetHabitsResponse } from '@habits/responses/get-habits.response';

@Injectable({
  providedIn: 'root',
})
export class HabitApi {
  private readonly _http = inject(HttpClient);

  private readonly baseUrl = environment.apiUrl;

  public get(): Observable<GetHabitsResponse> {
    return this._http.get<GetHabitsResponse>(`${this.baseUrl}/habits`);
  }
}
