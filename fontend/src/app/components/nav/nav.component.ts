import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AlertifyService } from '../../services/alertify.service';
import { User } from 'src/app/models/user.model';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  currentUser: User;

  @ViewChild('loginForm', { static: false }) loginForm: NgForm;

  ngOnInit(): void {
  }

  onSubmit() {
    this.authService.login(this.loginForm.value).subscribe(
      next => {
        console.log('user siggned in');
        this.alertify.success('User loggedIn Successfully');
      },
      error => this.alertify.error(error)
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
