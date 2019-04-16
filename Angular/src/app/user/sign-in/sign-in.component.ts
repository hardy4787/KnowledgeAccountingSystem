import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/shared/user/role.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  isLoginError: boolean = false;
  constructor(private userService: UserService, private roleService: RoleService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
  }
  OnSubmit(userName: string, passWord: string) {
    this.userService.userAunthentication(userName, passWord).subscribe((data: any) => {
      localStorage.setItem('userToken', data.access_token);
      localStorage.setItem('userRoles', data.role);
      localStorage.setItem('userId', data.Id);
      localStorage.setItem('constant_userId', data.Id);
      if (this.roleService.roleMatch(['admin']))
        this.router.navigate(['/manager/profiles']);
      else
        this.router.navigate(['/profile/' + data.Id]);
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          this.toastr.error(error.error.error_description);
        } else {
          this.toastr.error("Cannot sign-in a user!");
        }
      });
  }
}
