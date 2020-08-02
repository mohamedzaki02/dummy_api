import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AlertifyService } from '../../services/alertify.service';
import { User } from 'src/app/models/user.model';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  currentUser: User;

  @ViewChild('loginForm', { static: false }) loginForm: NgForm;

  ngOnInit(): void {
  }

  onSubmit() {
    this.authService.login(this.loginForm.value).subscribe(
      next => {
        this.authService.deCodeToken();
        this.alertify.success('User loggedIn Successfully');
      },
      error => this.alertify.error(error),
      () => this.router.navigate(['/members'])
    )
  }

  loggenIn(): boolean {
    return this.authService.loggedIn();
  }

  logout() {
    this.authService.logOut();
    this.router.navigate(['/']);
  }


}
