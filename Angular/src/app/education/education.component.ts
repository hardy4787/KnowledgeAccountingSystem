import { Component, OnInit, TemplateRef, ViewChild, HostListener } from '@angular/core';
import { Education } from '../shared/education/education.model';
import { EducationService } from '../shared/education/education.service';
import { ToastrService } from 'ngx-toastr';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { NgForm } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { RoleService } from '../shared/user/role.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent implements OnInit {
  datePickerConfig: Partial<BsDatepickerConfig>;
  @ViewChild('educationModal') public educationModal: ModalDirective;
  education: Education = new Education();
  constructor(private service: EducationService, private roleService: RoleService, private toastr: ToastrService) {
    this.datePickerConfig = Object.assign({},
      {
        containerClass: 'theme-dark-blue',
        dateInputFormat: 'YYYY-MM-DD'
      });
  }

  ngOnInit() {
    this.service.refreshInfo();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.resetForm();
    this.service.formData = {
      Id: null,
      Level: '',
      NameInstitution: '',
      EntryDate: null,
      CloseDate: null,
      ProgrammerId: null
    }
  }

  populateForm(education: Education) {
    this.service.formData = Object.assign({}, education);
  }

  onSubmit(form: NgForm) {
    this.educationModal.hide();
    if (form.value.Id == null)
      this.insertEducation(form);
    else
      this.updateRecord(form);
  }

  updateRecord(form: NgForm) {
    this.service.putInfo(form.value).subscribe((data: any) => {
      this.toastr.warning(data.Message)
      this.service.refreshInfo();
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400 && error.error.ModelState !== undefined) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else if (error.status === 400) {
          this.toastr.error(error.error.Message);
        } else {
          this.toastr.error("Cannot updated an skill!");
        }
      });
  }

  insertEducation(form: NgForm) {
    this.service.postEducation(form.value).subscribe((data: any) => {
      this.toastr.success(data.Message);
      this.service.refreshInfo();
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400 && error.error.ModelState !== undefined) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else if (error.status === 400) {
          this.toastr.error(error.error.Message);
        } else {
          this.toastr.error("Cannot inserted an education!");
        }
      });
  }

  onDelete(id: number) {
    this.service.deleteEducation(id).subscribe((data: any) => {
      this.toastr.warning(data.Message)
      this.service.refreshInfo();
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          this.toastr.error(error.error.Message);
        } else {
          this.toastr.error("Cannot deleted an education!");
        }
      });
  }
}
