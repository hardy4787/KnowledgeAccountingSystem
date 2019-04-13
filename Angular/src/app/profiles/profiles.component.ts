import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ManagerService } from '../shared/manager/manager.service';
import { SkillService } from '../shared/skill/skill.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.css']
})
export class ProfilesComponent implements OnInit {

  constructor(private service: ManagerService, private toastr: ToastrService, private skillService: SkillService, private sanitizer: DomSanitizer, private router: Router) { }

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
      data => saveAs(data),
      error => console.error(error)
    );
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
