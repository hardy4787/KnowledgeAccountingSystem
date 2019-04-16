import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ManagerService } from '../shared/manager/manager.service';
import { SkillService } from '../shared/skill/skill.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { saveAs } from 'file-saver';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.css']
})
export class ProfilesComponent implements OnInit {

  constructor(private service: ManagerService, private toastr: ToastrService, private skillService: SkillService, private sanitizer: DomSanitizer, private router: Router) { }
  isLoadingReport : boolean = false;
  imageUrl: string = "/assets/default-image.jpg";
  ngOnInit() {
    this.skillService.getSkills();
    this.service.refreshInfo(this.service.formData);
  }
  sanitize(style) {
    return this.sanitizer.bypassSecurityTrustUrl(style);
  }

  onSubmit(form: NgForm) {
    this.service.refreshInfo(form.value);
  }
  createReport(form: NgForm) {
    this.service.createReport(form.value).subscribe(
      (data : any) => {
        saveAs(data);
        this.isLoadingReport = false;
      },
      (error: HttpErrorResponse) => {
        this.isLoadingReport = false;
        if (error.status === 400 && error.error.ModelState !== undefined) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else if (error.status === 400) {
          this.toastr.error(error.error.Message);
        } else {
          this.toastr.error("Cannot created a report!");
        }
      });
  }
  filterRow(str: string, newObj: any) {
    if (str.length > 25)
      return str.substring(0, 25) + '...';
    alert(newObj.value)
  }
  setImageProfile(imageUrl: string) {
    if (imageUrl == null) {
      return this.imageUrl;
    }
    return imageUrl;
  }

  goToProfile(profileId: string) {
    localStorage.setItem('userId', profileId);
    this.router.navigate(['/profile/' + profileId]);
  }
}
