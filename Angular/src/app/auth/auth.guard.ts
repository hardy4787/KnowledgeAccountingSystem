import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CanActivate } from '@angular/router/src/utils/preactivation';
import { RoleService } from '../shared/user/role.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  path: ActivatedRouteSnapshot[]; route: ActivatedRouteSnapshot;
  constructor(private router: Router, private service: RoleService) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('userToken') != null) {
      let roles = route.data["roles"] as Array<string>;
      if (roles) {
        var match = this.service.roleMatch(roles);
        if (match) return true;
        else {
          this.router.navigate(['forbidden']);
          return false;
        }
      }
      else
        return true;
    }
    this.router.navigate(['/login']);
    return false;
  }
}
