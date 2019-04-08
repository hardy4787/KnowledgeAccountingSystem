import { Injectable } from '@angular/core';
import { Education } from './education.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EducationService {

  readonly rootUrl : string = 'http://localhost:16143/api/profile/';

  constructor(private http : HttpClient) { }

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
    return this.http.get(this.rootUrl + localStorage.getItem('userId') + "/education").toPromise().then(res=>this.educationList = res as Education[]);
  }
  
  deleteEducation(id : number){
    return this.http.delete(this.rootUrl + localStorage.getItem('userId')+'/education/'+id);
  }
}
