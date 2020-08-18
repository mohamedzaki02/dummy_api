import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  baseUrl: string = environment.baseUrl + 'users/';

  constructor(private http: HttpClient, private authService: AuthService) { }

  public upload(formData) {

    return this.http.post<any>(this.baseUrl + this.authService.currentUser.userId + '/photos', formData, {
      reportProgress: true,
      observe: 'events',
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    });
  }
}
