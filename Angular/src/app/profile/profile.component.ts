import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { RoleService } from '../shared/user/role.service';
import { UserService } from '../shared/user/user.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public id: string;
  public routeSubscription: Subscription;
  constructor(private router: Router,private route: ActivatedRoute,private service : RoleService,private userService : UserService, private toastr : ToastrService) {
    route.params.subscribe(params=>this.id=params['id']);
    if(this.id != localStorage.getItem('userId'))
      localStorage.setItem('userId',this.id);
  }
  isOpened : boolean = true;
  isCollapsed : boolean  = true;
  isMainMenuOpened : boolean  = true;
  
  ngOnInit() {
  }
  deleteUser(){
    this.userService.deleteUser().subscribe((data:any) =>{
      this.toastr.warning('Deleted successfully', 'Profile deleted')
      this.router.navigate(['/login']);
    },
    (error: HttpErrorResponse) => {
      if (error.status === 400) {
            this.toastr.error(error.error.Message);
      } else {
        this.toastr.error("Cannot edit a profile!");
      }
    });
  }
  LogOut(){
    localStorage.removeItem('userToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('userRoles');
    localStorage.removeItem('const_userId');
    this.router.navigate(['/login']);
  }

  toggleMenu(){
    this.isOpened = !this.isOpened;
  }
}
