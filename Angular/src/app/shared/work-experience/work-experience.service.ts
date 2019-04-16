import { Injectable } from '@angular/core';
import { WorkExperience } from './work-experience.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class WorkExperienceService {
  readonly rootUrl : string = 'http://localhost:16143/api/profile/';

  constructor(private http : HttpClient, private toastr : ToastrService) { }

  formData : WorkExperience = new WorkExperience();
  workExperienceList: WorkExperience[] = [];

  putWorkExperience(workExperience : WorkExperience){
    const body: WorkExperience = {
      Id: workExperience.Id,
      Position: workExperience.Position,
      Company: workExperience.Company,
      EntryDate : workExperience.EntryDate,
      CloseDate: workExperience.CloseDate,
      Description: workExperience.Description,
      ProgrammerId: workExperience.ProgrammerId,
    }
    return this.http.put(this.rootUrl + localStorage.getItem('userId') + "/work-experience/" + body.Id,body);
  }
  
  postWorkExperience(formData : WorkExperience){
    alert(formData.Id);
    formData.ProgrammerId = localStorage.getItem('userId');
    return this.http.post(this.rootUrl + localStorage.getItem('userId') + "/work-experience", formData);
  }
  refreshInfo(){
    return this.http.get(this.rootUrl + localStorage.getItem('userId') + "/work-experience").subscribe((data:any)=>{
      this.workExperienceList = data as WorkExperience[]
    },
    (error: HttpErrorResponse) => {
      if(error.status === 500){
        this.toastr.error("Not possible to get information!");
      }
    });
  }
  
  deleteWorkExperience(id : number){
    return this.http.delete(this.rootUrl + localStorage.getItem('userId')+'/work-experience/'+id);
  }
}
