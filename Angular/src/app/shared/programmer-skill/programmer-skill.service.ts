import { Injectable } from '@angular/core';
import { Skill } from '../skill/skill.model';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ProgrammerSkill } from './programmer-skill.model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ProgrammerSkillService {
  formData: ProgrammerSkill = new ProgrammerSkill();
  skillList: Skill[] = [];
  programmerSkillList: ProgrammerSkill[] = [];


  readonly rootUrl: string = 'http://localhost:16143/api/profile/';

  constructor(private http: HttpClient, private toastr : ToastrService) { }


  getUntouchedSkills() {
    return this.http.get(this.rootUrl + localStorage.getItem('userId') + "/untouched-skills").toPromise().then(res => this.skillList = res as Skill[]);
  }

  putProgrammerSkill(programmerSkill: ProgrammerSkill) {
    const body: ProgrammerSkill = {
      SkillId: programmerSkill.SkillId,
      KnowledgeLevel: this.formData.KnowledgeLevel,
      ProgrammerId: programmerSkill.ProgrammerId
    }
    return this.http.put(this.rootUrl + localStorage.getItem('userId') + "/skills/" + body.SkillId, body);
  }

  postProgrammerSkill(formData: ProgrammerSkill) {
    formData.KnowledgeLevel = this.formData.KnowledgeLevel;
    formData.ProgrammerId = localStorage.getItem('userId');
    return this.http.post(this.rootUrl + localStorage.getItem('userId') + "/skills", formData);
  }

  refreshInfo() {
    return this.http.get(this.rootUrl + localStorage.getItem('userId') + "/skills").subscribe((data:any)=>{
      this.programmerSkillList = data as ProgrammerSkill[]
    },
    (error: HttpErrorResponse) => {
      if(error.status === 400){
        this.toastr.error(error.error.Message);
      } else {
        this.toastr.error("Not possible to get information!");
      }
    });
  }

  deleteProgrammerSkill(id: number) {
    return this.http.delete(this.rootUrl + localStorage.getItem('userId') + '/skills/' + id);
  }
}
