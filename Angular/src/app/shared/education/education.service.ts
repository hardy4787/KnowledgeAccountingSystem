import { Injectable } from '@angular/core';
import { Education } from './education.model';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class EducationService {

  readonly rootUrl : string = 'http://localhost:16143/api/profile/';

  constructor(private http : HttpClient, private toastr : ToastrService) { }

  formData : Education = new Education();
  educationList: Education[] = [];

    //done: boolean = false;
  putInfo(education : Education){
    const body: Education = {
      Id: education.Id,
      Level: education.Level,
      NameInstitution: education.NameInstitution,
      EntryDate : education.EntryDate,
      CloseDate: education.CloseDate,
      ProgrammerId: education.ProgrammerId,
    }
    return this.http.put(this.rootUrl + localStorage.getItem('userId') + "/education/" + body.Id,body);
  }
  
  postEducation(formData : Education){
    formData.ProgrammerId = localStorage.getItem('userId');
    return this.http.post(this.rootUrl + localStorage.getItem('userId') + "/education", formData);
  }
  refreshInfo(){
    return this.http.get(this.rootUrl + localStorage.getItem('userId') + "/education").subscribe((data:any)=>{
      this.educationList = data as Education[]
    },
    (error: HttpErrorResponse) => {
      if(error.status === 500){
        this.toastr.error("Not possible to get information!");
      }
    });
  }

  deleteEducation(id : number){
    return this.http.delete(this.rootUrl + localStorage.getItem('userId')+'/education/'+id);
  }
}
