import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Skill } from './skill.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SkillService {
  readonly rootUrl : string = 'http://localhost:16143/api/';

  constructor(private http : HttpClient) { }

	fullSkillList: Skill[] = [];
  formData : Skill = new Skill();
	
  getSkills(){
    return this.http.get(this.rootUrl + "skills").toPromise().then(res=>this.fullSkillList = res as Skill[]);
  }

  deleteSkill(id : number){
    return this.http.delete(this.rootUrl + 'skills/' + id);
  }

  postSkill(formData : Skill){
    return this.http.post(this.rootUrl + "skills", formData);
  }
  
  putSkill(skill : Skill){
    const body: Skill = {
      Id: skill.Id,
      Name: skill.Name,
      Description: skill.Description
    }
    return this.http.put(this.rootUrl + "skills/" + body.Id,body);
  }
}
