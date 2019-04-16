import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { Profile } from './profile.model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  readonly rootUrl = 'http://localhost:16143'

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  formData: Profile = new Profile();
  profile: Profile = new Profile();
  putMainInfo(user: Profile) {
    const body: Profile = {
      Id: localStorage.getItem('userId'),
      FullName: user.FullName,
      Age: user.Age,
      Email: user.Email,
      Address: user.Address,
      Phone: user.Phone,
      GitHub: user.GitHub,
      ImageProfileUrl: user.ImageProfileUrl
    }
    return this.http.put(this.rootUrl + '/api/profile/' + body.Id, body);
  }

  refreshInfo() {
    return this.http.get(this.rootUrl + '/api/profile/' + localStorage.getItem('userId')).subscribe((data) => {
      this.profile = data as Profile
    },
      (error: HttpErrorResponse) => {
        if (error.status === 500) {
          this.toastr.error("Not possible to get information!");
        }
      });
  }

  postFile(fileToUpload: File) {
    const endpoint = this.rootUrl + '/api/profile/' + localStorage.getItem('userId') + '/imageProfile';
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload, fileToUpload.name);
    return this.http.put(endpoint, formData);
  }
}