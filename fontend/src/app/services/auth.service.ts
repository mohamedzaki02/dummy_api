import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  baseUrl = 'http://localhost:5000/api/auth/';

  login(user: User): Observable<any>{
    return this.http.post(this.baseUrl + 'login', user).pipe(map((response: any) => {
      if (response) localStorage.setItem('token', response.token);
      return response;
    }));
  }

  loggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  logOut(): void {
    localStorage.removeItem('token');
  }
}
