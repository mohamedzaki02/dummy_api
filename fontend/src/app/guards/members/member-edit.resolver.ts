import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from '../../models/user';
import { UsersService } from '../../services/users.service';
import { AlertifyService } from '../../services/alertify.service';
import { AuthService } from '../../services/auth.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class MemberEditResolver implements Resolve<User>{

    constructor(private usersService: UsersService, private authService: AuthService, private alertify: AlertifyService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
        return this.usersService.getUserById(+this.authService.currentUser.userId).pipe(catchError(error => {
            this.alertify.error(error);
            this.router.parseUrl('/');
            return of(null);
        }));
    }

}