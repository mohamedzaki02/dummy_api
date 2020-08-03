import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { User } from '../../models/user';
import { UsersService } from '../../services/users.service';
import { AlertifyService } from '../../services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class MemberDetailsResolver implements Resolve<User>{
    constructor(private usersService: UsersService, private alertify: AlertifyService, private router: Router) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
        return this.usersService.getUserById(+ route.params['id']).pipe(catchError(error => {
            this.alertify.error(error);
            this.router.parseUrl('/members');
            return of(null);
        }));
    }

}