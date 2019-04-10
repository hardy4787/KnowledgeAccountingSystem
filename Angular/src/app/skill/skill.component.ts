import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { Skill } from '../shared/skill/skill.model';
import { SkillService } from '../shared/skill/skill.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-skill',
  templateUrl: './skill.component.html',
  styleUrls: ['./skill.component.css']
})
export class SkillComponent implements OnInit {
  @ViewChild('skillModal') public skillModal: ModalDirective;
  skill: Skill = new Skill();

  constructor(private service: SkillService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.getSkills();
  }
  resetForm(form?: NgForm) {
    if (form != null)
      form.resetForm();
    this.service.formData = {
      Id: null,
      Name: '',
      Description: ''
    }
  }
  populateForm(education: Skill) {
    this.service.formData = Object.assign({}, education);
  }
  onSubmit(form: NgForm) {
    this.skillModal.hide();
    if (form.value.Id == null)
      this.insertSkill(form);
    else
      this.updateSkill(form);
  }
  updateSkill(form: NgForm) {
    this.service.putSkill(form.value).subscribe((data: any) => {
      this.toastr.warning(data.Message)
      this.service.getSkills();
    },
    (error: HttpErrorResponse) => {
      if (error.status === 400) {
        for (var key in error.error.ModelState)
          for (var i = 0; i < error.error.ModelState[key].length; i++)
            this.toastr.error(error.error.ModelState[key][i]);
      } else {
        this.toastr.error("Cannot updated a skill!");
      }
    });
  }

  insertSkill(form: NgForm) {
    this.service.postSkill(form.value).subscribe((data: any) => {
      this.resetForm(form);
      this.toastr.success(data.Message);
      this.service.getSkills();
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else {
          this.toastr.error("Cannot inserted a skill!");
        }
      });
  }
  onDelete(id: number) {
    this.service.deleteSkill(id).subscribe((data: any) => {
      this.toastr.error(data.Message)
      this.service.getSkills();
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else {
          this.toastr.error("Cannot deleted a skill!");
        }
      });
  }
}