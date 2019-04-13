import { Component, OnInit, ViewChild } from '@angular/core';
import { ProfileService } from 'src/app/shared/profile/profile.service';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { Profile } from '../shared/profile/profile.model';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
import { Pipe, PipeTransform } from '@angular/core';
import { ModalDirective, BsModalRef } from 'ngx-bootstrap';
import { RoleService } from '../shared/user/role.service';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Pipe({ name: 'safe' })
@Component({
  selector: 'app-main-info-profile',
  templateUrl: './main-info-profile.component.html',
  styleUrls: ['./main-info-profile.component.css']
})
export class MainInfoProfileComponent implements OnInit {
  @ViewChild('mainInfoModal') public mainInfoModal: ModalDirective;
  @ViewChild('imageProfileModal') public imageProfileModal: ModalDirective;
  imageUrl: string = "/assets/default-image.jpg";
  modalImageUrl: string = null;
  fileToUpload: File = null;
  profile: Profile = new Profile();

  constructor(private service: ProfileService, private roleService: RoleService, private toastr: ToastrService, private sanitizer: DomSanitizer, private route: ActivatedRoute) {
  }

  sanitize(style) {
    return this.sanitizer.bypassSecurityTrustUrl(style);
  }

  ngOnInit() {
    this.service.refreshInfo();
  }
  setImageProfile() {
    if (this.service.profile.ImageProfileUrl == null) {
      return this.imageUrl;
    }
    return this.service.profile.ImageProfileUrl;
  }

  populateForm(profile: Profile) {
    this.service.formData = Object.assign({}, profile);
  }

  OnSubmit(form: NgForm) {
    this.mainInfoModal.show();
    this.mainInfoModal.hide();
    this.service.putMainInfo(form.value).subscribe((data: any) => {
      this.toastr.warning(data.Message);
      this.service.refreshInfo();
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else {
          this.toastr.error("Cannot edit a profile!");
        }
      });
  }
  handlerCloseModal(Image: any) {
    console.log(Image.value);
    Image = null;
    this.modalImageUrl = null;
  }
  OnImageProfileSubmit(Image) {
    this.imageProfileModal.hide();
    this.service.postFile(this.fileToUpload).subscribe(() => {
      Image.value = null;
    },
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          for (var key in error.error.ModelState)
            for (var i = 0; i < error.error.ModelState[key].length; i++)
              this.toastr.error(error.error.ModelState[key][i]);
        } else {
          this.toastr.error("Cannot change an image!");
        }
      });
  }


  fileExt: string[] = ['.jpeg', '.png', '.jpg'];

  handleFileInput(file: FileList, Image) {
    this.fileToUpload = file.item(0);
    var ext = this.fileToUpload.name.match(/\..+$/)[0];
    console.log(ext);
    if (this.fileExt.join().search(ext[ext.length - 1]) == -1) {
      this.toastr.warning('Your file must have a format: .png .jpeg .jpg');
      Image.value = null;
      return;
    }

    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.modalImageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }
}
