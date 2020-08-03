import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
import { map } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  currentUser: any;

  login(user: User): Observable<any> {
    return this.http.post(this.baseUrl + 'login', user).pipe(map((response: any) => {
      if (response) localStorage.setItem('token', response.token);
      return response;
    }));
  }

  loggedIn(): boolean {
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
  }

  logOut(): void {
    localStorage.removeItem('token');
  }

  deCodeToken(): void {
    let token = localStorage.getItem('token');
    if (token) {
      let token_val = this.jwtHelper.decodeToken(token);
      this.currentUser = { username: token_val.unique_name };
    }
  }
}
