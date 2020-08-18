import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { User } from 'src/app/models/user';
import { NgForm } from '@angular/forms';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CanDeactivateComponent } from '../../../guards/prevent-un-saved-changes.guard';
import { AuthService } from 'src/app/services/auth.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit, CanDeactivateComponent {

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private authService: AuthService, private userService: UsersService) { }


  canDeactivate(): boolean {
    return !(this.editForm.dirty && this.editForm.touched);
  }

  @HostListener("window:beforeunload", ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) $event.returnValue = true;
  }

  @ViewChild('editForm', { static: false }) editForm: NgForm;
  user: User;

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(): void {
    this.route.data.subscribe((resolveData: Data) => this.user = resolveData['user']);
  }

  photoUploaded(photo) {
    this.user.photos.push(photo);
    this.alertify.success('photo uploaded successfully!');
  }

  saveUser() {
    this.userService.updateUser(+this.authService.currentUser.userId, this.editForm.value).subscribe(next => {
      this.alertify.success('profile data saved');
      this.editForm.reset(this.editForm.value);
    }, error => this.alertify.error(error));
  }

  

}
