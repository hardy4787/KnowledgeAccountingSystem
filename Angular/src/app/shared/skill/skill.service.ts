import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Skill } from './skill.model';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root'
})
export class SkillService {
  readonly rootUrl : string = 'http://localhost:16143/api/';

  constructor(private http : HttpClient, private toastr : ToastrService) { }

	fullSkillList: Skill[] = [];
  formData : Skill = new Skill();
	
  getSkills(){
    return this.http.get(this.rootUrl + "skills").subscribe((data:any)=>{
      this.fullSkillList = data as Skill[]
    },
    (error: HttpErrorResponse) => {
      if(error.status === 500){
        this.toastr.error("Not possible to get information!");
      }
    });
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
