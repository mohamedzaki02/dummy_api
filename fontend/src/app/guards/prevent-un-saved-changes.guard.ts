import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertifyService } from '../services/alertify.service';


export interface CanDeactivateComponent {
  canDeactivate(): boolean;
}


@Injectable({
  providedIn: 'root'
})
export class PreventUnSavedChangesGuard implements CanDeactivate<CanDeactivateComponent> {

  constructor(private alertify: AlertifyService) { }


  canDeactivate(component: CanDeactivateComponent, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if (!component.canDeactivate()) return confirm("data will not be saved !");
    else return true;
  }

}
