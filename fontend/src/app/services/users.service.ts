import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }
  baseUrl = environment.baseUrl + 'users/';
  httpOptions = {
    headers: new HttpHeaders({
      'Accept' : 'application/json'
    })
  };

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl, this.httpOptions);
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + id, this.httpOptions);
  }

}
