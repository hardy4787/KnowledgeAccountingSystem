import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"; 
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from './user.model';
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly rootUrl = 'http://localhost:16143'
  constructor(private http : HttpClient, private toastr : ToastrService) { }
  deleteUser(){
    return this.http.delete(this.rootUrl + '/api/Account/' + localStorage.getItem('userId'));
  }
  registerUser(user : User){
    const body: User = {
      UserName: user.UserName,
      FullName: user.FullName,
      Password: user.Password,
      ConfirmPassword: user.ConfirmPassword,
      Email : user.Email
    }
    var reqHeader = new HttpHeaders({'No-Auth': 'True'});
    return this.http.post(this.rootUrl + '/api/Account/Register',body, {headers : reqHeader});
  }

  userAunthentication(userName: string, password: string){
    var data = "username="+userName+"&password="+password+"&grant_type=password";
    var reqHeader = new HttpHeaders({'Content-Type':'application/x-www-urlencoded','No-Auth': 'True'});
    return this.http.post(this.rootUrl+'/token',data,{headers: reqHeader});
  }
}
