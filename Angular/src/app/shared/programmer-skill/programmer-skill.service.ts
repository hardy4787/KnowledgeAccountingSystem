import { Injectable } from '@angular/core';
import { Skill } from '../skill/skill.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { ProgrammerSkill } from './programmer-skill.model';

@Injectable({
  providedIn: 'root'
})
export class ProgrammerSkillService {
  formData: ProgrammerSkill = new ProgrammerSkill();
  skillList: Skill[] = [];
  programmerSkillList: ProgrammerSkill[] = [];


  readonly rootUrl: string = 'http://localhost:16143/api/profile/';

  constructor(private http: HttpClient) { }


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
    alert(this.rootUrl + localStorage.getItem('userId') + "/skills");
    return this.http.post(this.rootUrl + localStorage.getItem('userId') + "/skills", formData);
  }

  refreshInfo() {
    return this.http.get(this.rootUrl + localStorage.getItem('userId') + "/skills").toPromise().then(res => this.programmerSkillList = res as ProgrammerSkill[]);
  }

  deleteProgrammerSkill(id: number) {
    return this.http.delete(this.rootUrl + localStorage.getItem('userId') + '/skills/' + id);
  }
}
