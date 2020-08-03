import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../../models/user';
import { UsersService } from '../../services/users.service';
import { AlertifyService } from '../../services/alertify.service';



@Injectable()
export class MembersListResolver implements Resolve<User[]>{

    constructor(private usersService: UsersService, private alertify: AlertifyService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User[] | Observable<User[]> | Promise<User[]> {
        return this.usersService.getUsers().pipe(catchError(err => {
            this.alertify.error(err);
            this.router.parseUrl('/');
            return of(null);
        }))
    }

}