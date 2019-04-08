import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Profile } from '../profile/profile.model';
import { ProgrammerModel } from './manager.model';
import { NgForm } from '@angular/forms';
import { Skill } from '../skill/skill.model';
import { Observable } from 'rxjs';


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

  constructor(private http : HttpClient) { }


  refreshInfo(programmer : ProgrammerModel){
    const body: ProgrammerModel = {
      SkillId : programmer.SkillId,
      KnowledgeLevel: programmer.KnowledgeLevel
    }
    return this.http.get(this.rootUrl + "/profiles/" + body.SkillId + "/" + body.KnowledgeLevel).toPromise().then(res=>this.profileList = res as Profile[]);
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
