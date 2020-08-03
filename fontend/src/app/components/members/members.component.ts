import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user';
import { AlertifyService } from '../../services/alertify.service';
import { ActivatedRoute, Data } from '@angular/router';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {

  constructor(private usersSerivce: UsersService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  users: User[];

  ngOnInit(): void {
    this.route.data.subscribe((usersData: Data) => this.users = usersData['users']);
  }



}
