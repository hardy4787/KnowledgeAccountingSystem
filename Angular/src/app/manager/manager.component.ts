import { Component, OnInit, ViewChild } from '@angular/core';
import { ManagerService } from '../shared/manager/manager.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { SkillService } from '../shared/skill/skill.service';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.css']
})
export class ManagerComponent implements OnInit {
  isCollapsed: boolean = true;
  constructor(private service: ManagerService, private skillService: SkillService, private sanitizer: DomSanitizer, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    localStorage.removeItem('userId');
  }
  LogOut() {
    localStorage.removeItem('userToken');
    localStorage.removeItem('userId');
    console.log('userToken');
    this.router.navigate(['/login']);
  }
}
