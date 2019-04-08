import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"; 
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from './user.model';


@Injectable({
  providedIn: 'root'
})
export class RoleService {
  
  userMatch() : boolean{
    if(localStorage.getItem('userId')!=localStorage.getItem('constant_userId'))
      return true;
    return false;
  }


  roleMatch(allowedRoles : any): boolean{
    var isMatch = false;
    var userRoles: string[] = JSON.parse(localStorage.getItem('userRoles'));
    allowedRoles.forEach((element: string) => {
      if(userRoles.indexOf(element) > -1){
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
}
