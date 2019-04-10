import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Profile } from '../profile/profile.model';
import { ProgrammerModel } from './manager.model';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class ManagerService {
  formData : ProgrammerModel = {
    SkillId : null,
    KnowledgeLevel : 0,
  };
  profileList : Profile[] = [];
  readonly rootUrl = 'http://localhost:16143/api/manager'

  constructor(private http : HttpClient, private router: Router) { }


  refreshInfo(programmer : ProgrammerModel){
    const params = new HttpParams({
      fromString: `skillId=${programmer.SkillId}&knowledgeLevel=${programmer.KnowledgeLevel}`
    });
    this.router.navigate(['/manager', 'profiles'], { queryParams: { skillId : this.formData.SkillId, knowledgeLevel : this.formData.KnowledgeLevel } });
    return this.http.get(this.rootUrl + "/profiles", {
      params : params }).toPromise().then(res=>this.profileList = res as Profile[]);
  }
  createReport(programmer : ProgrammerModel){
    const body: ProgrammerModel = {
      SkillId : programmer.SkillId,
      KnowledgeLevel: programmer.KnowledgeLevel
    }
    return this.http.post(this.rootUrl + "/profiles/" + body.SkillId + "/" + body.KnowledgeLevel + "/create-report",this.profileList, {
      responseType : 'blob'
    });
  }
}
