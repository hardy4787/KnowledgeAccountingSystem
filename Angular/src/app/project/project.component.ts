import { Component, OnInit, ViewChild } from '@angular/core';
import { Project } from '../shared/project/project.model';
import { ToastrService } from 'ngx-toastr';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ProjectService } from '../shared/project/project.service';
import { NgForm } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap';
import { RoleService } from '../shared/user/role.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-projects',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {
  @ViewChild('projectModal') public projectModal: ModalDirective;
  datePickerConfig: Partial<BsDatepickerConfig>;
  project: Project = new Project();
  constructor(private service : ProjectService, private roleService : RoleService, private toastr : ToastrService) {
    this.datePickerConfig = Object.assign({},
      {
        containerClass : 'theme-dark-blue',
        dateInputFormat : 'YYYY-MM-DD'
      });
  }

  ngOnInit() {
    this.service.refreshInfo();
  }

  resetForm(form? : NgForm){
    if(form!=null)
      form.resetForm();
    this.service.formData = {
      Id : null,
      Name : '',
      ReferenceToTheProject : '',
      DescriptionOfTasks : '',
      ProgrammerId : null
    }
  }

  populateForm(project : Project){
    this.service.formData = Object.assign({},project);
  }

  onSubmit(form : NgForm){
    this.projectModal.hide();
    if(form.value.Id == null)
      this.insertProject(form);
    else
      this.updateRecord(form);
  } 
  
  updateRecord(form:NgForm){
    this.service.putInfo(form.value).subscribe((data:any) =>{
      this.toastr.warning(data.Message)
      this.service.refreshInfo();
    },
    (error: HttpErrorResponse) => {
      if (error.status === 400) {
        for (var key in error.error.ModelState)
          for (var i = 0; i < error.error.ModelState[key].length; i++)
            this.toastr.error(error.error.ModelState[key][i]);
      } else {
        this.toastr.error("Cannot updated a project!");
      }
    });
  }

  insertProject(form: NgForm){
    this.service.postProject(form.value).subscribe((data:any)=>{
    this.toastr.success(data.Message);
    this.service.refreshInfo();
    },
    (error: HttpErrorResponse) => {
      if (error.status === 400) {
        for (var key in error.error.ModelState)
          for (var i = 0; i < error.error.ModelState[key].length; i++)
            this.toastr.error(error.error.ModelState[key][i]);
      } else {
        this.toastr.error("Cannot inserted a project!");
      }
    });
  }

  onDelete(id : number){
    this.service.deleteProject(id).subscribe((data:any)=>{
    this.toastr.error(data.Message)
    this.service.refreshInfo();
    },
    (error: HttpErrorResponse) => {
      if (error.status === 400) {
        for (var key in error.error.ModelState)
          for (var i = 0; i < error.error.ModelState[key].length; i++)
            this.toastr.error(error.error.ModelState[key][i]);
      } else {
        this.toastr.error("Cannot deleted a project!");
      }
    });
  }
}
