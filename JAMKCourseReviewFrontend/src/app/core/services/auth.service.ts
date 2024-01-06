import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from '../../shared/models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5073/api/users';
  private _currentUser = new BehaviorSubject<User | null>(null);
  currentUser$ = this._currentUser.asObservable();

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http
      .post<User>(
        `${this.apiUrl}/login`,
        { username, password },
        { withCredentials: true }
      )
      .pipe(
        tap((response) => {
          if (response && response.userId) {
            this._currentUser.next(response);
          }
        })
      );
  }

  // TODO: Implement register

  logout(): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/logout`, {}, { withCredentials: true })
      .pipe(
        tap(() => {
          this._currentUser.next(null);
        })
      );
  }
}
