import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user/user.service';
import { User } from 'src/app/shared/user/user.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  user: User = new User();
  constructor(private router : Router, private userService: UserService, private toastr: ToastrService) {
  }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.user = {
      UserName: '',
      FullName: '',
      Password: '',
      ConfirmPassword: '',
      Email: ''
    }
  }

  OnSubmit(form: NgForm) {
    this.userService.registerUser(form.value)
      .subscribe((data: any) => {
        if (data.Succeeded) {
          this.resetForm(form);
          this.toastr.success(data.Message);
          this.router.navigate(['/login']);
        }
      },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else {
          this.toastr.error("Cannot register an user!");
        }
      });
  }
}
