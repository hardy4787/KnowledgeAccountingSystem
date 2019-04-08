import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user/user.service';
import { User } from 'src/app/shared/user/user.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  user: User = new User();
  constructor(private userService: UserService, private toastr: ToastrService) {
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
        if (data.Succedeed) {
          this.resetForm(form);
          this.toastr.success(data.Message);
        }
      },
        (error: HttpErrorResponse) => {
          alert(error.status);
          this.toastr.error(error.error.ModelState.Email);
        });
  }
}
