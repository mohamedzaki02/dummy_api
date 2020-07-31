import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private authService: AuthService) { }

  @ViewChild('loginForm', { static: false }) loginForm: NgForm;

  ngOnInit(): void {
  }

  onSubmit() {
    this.authService.login(this.loginForm.value).subscribe(
      next => console.log('user siggned in'),
      error => console.log(error)
    )
  }

  loggenIn(): boolean {
    return this.authService.loggedIn();
  }

  logout() {
    this.authService.logOut();
    console.log('user logged out');
  }


}
