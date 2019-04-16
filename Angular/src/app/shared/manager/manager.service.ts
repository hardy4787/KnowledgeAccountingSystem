import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Profile } from '../profile/profile.model';
import { ProgrammerModel } from './manager.model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


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

  constructor(private http : HttpClient, private router: Router, private toastr : ToastrService) { }


  refreshInfo(programmer : ProgrammerModel){
    const params = new HttpParams({
      fromString: `skillId=${programmer.SkillId}&knowledgeLevel=${programmer.KnowledgeLevel}`
    });
    this.router.navigate(['/manager', 'profiles'], { queryParams: { skillId : this.formData.SkillId, knowledgeLevel : this.formData.KnowledgeLevel } });
    return this.http.get(this.rootUrl + "/profiles", {
      params : params }).subscribe((data:any)=>{
        this.profileList = data as Profile[]
      },
      (error: HttpErrorResponse) => {
        if(error.status === 500){
          this.toastr.error("Not possible to get information!");
        }
      });
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
