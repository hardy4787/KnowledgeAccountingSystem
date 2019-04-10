import { Component, OnInit, ViewChild } from '@angular/core';
import { WorkExperienceService } from '../shared/work-experience/work-experience.service';
import { RoleService } from '../shared/user/role.service';
import { ToastrService } from 'ngx-toastr';
import { ModalDirective, BsDatepickerConfig } from 'ngx-bootstrap';
import { WorkExperience } from '../shared/work-experience/work-experience.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-work-experience',
  templateUrl: './work-experience.component.html',
  styleUrls: ['./work-experience.component.css']
})
export class WorkExperienceComponent implements OnInit {

  datePickerConfig: Partial<BsDatepickerConfig>;
  @ViewChild('workExperienceModal') public workExperienceModal: ModalDirective;
  workExperience: WorkExperience = new WorkExperience();
  constructor(private service : WorkExperienceService, private roleService : RoleService, private toastr : ToastrService) {
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
      Company : '',
      Position : '',
      EntryDate : null,
      CloseDate : null,
      Description : '',
      ProgrammerId : null
    }
  }

  populateForm(workExperience : WorkExperience){
    this.service.formData = Object.assign({},workExperience);
  }

  onSubmit(form : NgForm){
    this.workExperienceModal.hide();
    if(form.value.Id == null)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  } 
  
  updateRecord(form:NgForm){
    this.service.putWorkExperience(form.value).subscribe(res =>{
      this.toastr.warning('Updated successfully')
      this.service.refreshInfo();
    });
  }

  insertRecord(form: NgForm){
    this.service.postWorkExperience(form.value).subscribe(res=>{
    this.toastr.success('Added successfully');
    this.service.refreshInfo();
    });
  }

  onDelete(id : number){
    this.service.deleteWorkExperience(id).subscribe(res=>{
    this.toastr.error('Deleted successfully')
    this.service.refreshInfo();
    });
  }

}
