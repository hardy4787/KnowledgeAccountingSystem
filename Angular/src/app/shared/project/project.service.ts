import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Project } from './project.model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  readonly rootUrl : string = 'http://localhost:16143/api/profile/';

  constructor(private http : HttpClient, private toastr : ToastrService) { }

  formData : Project = new Project();
  projectList: Project[] = [];

  putInfo(project : Project){
    const body: Project = {
      Id: project.Id,
      Name: project.Name,
      ReferenceToTheProject: project.ReferenceToTheProject,
      DescriptionOfTasks: project.DescriptionOfTasks,
      ProgrammerId: project.ProgrammerId,
    }
    return this.http.put(this.rootUrl + localStorage.getItem('userId') + "/projects/" + body.Id,body);
  }
  
  postProject(formData : Project){
    formData.ProgrammerId = localStorage.getItem('userId');
    return this.http.post(this.rootUrl + localStorage.getItem('userId') + "/projects", formData);
  }
  refreshInfo(){
    return this.http.get(this.rootUrl + localStorage.getItem('userId') + "/projects").subscribe((data:any)=>{
      this.projectList = data as Project[]
    },
    (error: HttpErrorResponse) => {
      if(error.status === 500){
        this.toastr.error("Not possible to get information!");
      }
    });
  }
  
  deleteProject(id : number){
    return this.http.delete(this.rootUrl + localStorage.getItem('userId')+'/projects/'+id);
  }
}
