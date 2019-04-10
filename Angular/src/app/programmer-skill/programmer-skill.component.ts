import { Component, OnInit, ViewChild } from '@angular/core';
import { Skill } from '../shared/skill/skill.model';
import { ProgrammerSkillService } from '../shared/programmer-skill/programmer-skill.service';
import { SkillService } from '../shared/skill/skill.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { ProgrammerSkill } from '../shared/programmer-skill/programmer-skill.model';
import { ModalDirective } from 'ngx-bootstrap';
import { RoleService } from '../shared/user/role.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-skills',
  templateUrl: './programmer-skill.component.html',
  styleUrls: ['./programmer-skill.component.css']
})
export class ProgrammerSkillComponent implements OnInit {
  @ViewChild('programmerSkillModal') public programmerSkillModal: ModalDirective;
  skill: Skill = new Skill();
  constructor(private service : ProgrammerSkillService, private skillService : SkillService,private roleService : RoleService, private toastr : ToastrService) {
  }
  conditionAddForm : boolean = true;
  ngOnInit() {
    this.skillService.getSkills();
    this.service.refreshInfo();
  }
  valueSlider : number = 0;

  getUntouchedSkills(){
    this.service.getUntouchedSkills();
  }

  getNameSkillByIdSkill(id : number) : string {
    if(id !== null)
      return this.skillService.fullSkillList.find(x => x.Id== id).Name;
  }

  getTooltipSkillByIdSkill(id : number) : string {
    if(id != null)
      return this.skillService.fullSkillList.find(x => x.Id== id).Description;
  }

  resetForm(form? : NgForm){
    if(form!=null)
      form.resetForm();
    this.service.formData = {
      SkillId : null,
      KnowledgeLevel : null,
      ProgrammerId : null
    }
  }

  populateForm(programmerSkill : ProgrammerSkill){
    this.service.formData = Object.assign({},programmerSkill);
  }

  onSubmit(form : NgForm){
    this.programmerSkillModal.hide();
  if(this.conditionAddForm)
    this.insertProgrammerSkill(form);
  else
    this.updateProgrammerSkill(form);
  } 
  
  updateProgrammerSkill(form:NgForm){
    this.service.putProgrammerSkill(form.value).subscribe((data : any) =>{
      this.toastr.warning(data.Message)
      this.service.refreshInfo();
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

  insertProgrammerSkill(form: NgForm){
    this.service.postProgrammerSkill(form.value).subscribe((data : any)=>{
    this.toastr.success(data.Message);
    this.service.refreshInfo();
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

  onDelete(id : number){
    this.service.deleteProgrammerSkill(id).subscribe((data : any)=>{
    this.toastr.error(data.Message)
    this.service.refreshInfo();
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
