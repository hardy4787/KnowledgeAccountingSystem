import { Component, OnInit, ViewChild } from '@angular/core';
import { ProfileService } from 'src/app/shared/profile/profile.service';
import { NgForm } from '@angular/forms';
import { Profile } from '../shared/profile/profile.model';
import { ToastrService } from 'ngx-toastr';
import {DomSanitizer, SafeStyle} from '@angular/platform-browser';
import { Pipe, PipeTransform } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { RoleService } from '../shared/user/role.service';
import { ActivatedRoute } from '@angular/router';

@Pipe({ name: 'safe' })
@Component({
  selector: 'app-main-info-profile',
  templateUrl: './main-info-profile.component.html',
  styleUrls: ['./main-info-profile.component.css']
})
export class MainInfoProfileComponent implements OnInit {
  @ViewChild('mainInfoModal') public mainInfoModal: ModalDirective;
  @ViewChild('imageProfileModal') public imageProfileModal: ModalDirective;

  imageUrl : string = "/assets/default-image.jpg";
  modalImageUrl : string = "/assets/default-image.jpg";
  fileToUpload : File = null;
  profile: Profile = new Profile();
  
  constructor(private service : ProfileService, private roleService : RoleService, private toastr : ToastrService, private sanitizer:DomSanitizer, private route: ActivatedRoute) { 
  }

  sanitize(style) {
    return this.sanitizer.bypassSecurityTrustUrl(style);
  }

  ngOnInit() {
    this.service.refreshInfo();
  }


  setImageProfile(){
    if(this.service.profile.ImageProfileUrl == null ){
      return this.imageUrl;
    }
    return this.service.profile.ImageProfileUrl;
  }

  populateForm(profile : Profile){
    this.service.formData = Object.assign({},profile);
  }

  OnSubmit(form:NgForm){
    this.mainInfoModal.hide();
    this.service.putMainInfo(form.value).subscribe(res =>{
      this.toastr.warning('Edited successfully');
      this.service.refreshInfo();
    });
  }
  OnImageProfileSubmit(Image){
    this.imageProfileModal.hide();
    this.service.postFile(this.fileToUpload).subscribe(
      (data : any) =>{
       Image.value = null;
      }
    );
  }
  handleFileInput(file : FileList){
    this.fileToUpload = file.item(0);


    var reader = new FileReader();
    reader.onload = (event:any) =>{
      this.modalImageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }
}
