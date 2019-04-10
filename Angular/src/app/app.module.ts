import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import{ FormsModule}from '@angular/forms'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule }   from '@angular/forms';
import { BsDatepickerModule, CollapseModule, ModalModule} from 'ngx-bootstrap'
import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { UserService } from './shared/user/user.service';
import { ProfileService } from './shared/profile/profile.service';
import { ProjectService } from './shared/project/project.service';
import { ProgrammerSkillService } from './shared/programmer-skill/programmer-skill.service';
import { SkillService } from './shared/skill/skill.service';
import { EducationService } from './shared/education/education.service';
import { ManagerService } from './shared/manager/manager.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserComponent } from './user/user.component';
import { ProfileComponent } from './profile/profile.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { appRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { MainInfoProfileComponent } from './main-info-profile/main-info-profile.component';
import { EducationComponent } from './education/education.component';
import { ProgrammerSkillComponent } from './programmer-skill/programmer-skill.component';
import { ProjectComponent } from './project/project.component';
import { NgxBootstrapSliderModule } from 'ngx-bootstrap-slider';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { AuthInterceptor } from './auth/auth.interceptor';
import { ManagerComponent } from './manager/manager.component';
import { ForbiddenComponent } from './errors/forbidden/forbidden.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { RoleService } from './shared/user/role.service';
import { WorkExperienceComponent } from './work-experience/work-experience.component';
import { WorkExperienceService } from './shared/work-experience/work-experience.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { SkillComponent } from './skill/skill.component';
import { ProfilesComponent } from './profiles/profiles.component';
@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    UserComponent,
    SignInComponent,
    ProfileComponent,
    MainInfoProfileComponent,
    EducationComponent,
    ProgrammerSkillComponent,
    SkillComponent,
    ProjectComponent,
    ManagerComponent,
    ForbiddenComponent,
    NotFoundComponent,
    WorkExperienceComponent,
    ProfilesComponent,
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CollapseModule.forRoot(),
    BrowserAnimationsModule,
    NgxBootstrapSliderModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ToastrModule.forRoot({
      preventDuplicates : false
    }),
    RouterModule.forRoot(appRoutes),
    ProgressbarModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot()
  ],
  providers: [UserService,RoleService,ProfileService,WorkExperienceService, ManagerService, EducationService, ProjectService, SkillService, ProgrammerSkillService, AuthGuard,
  ,{
    provide : HTTP_INTERCEPTORS,
    useClass : AuthInterceptor,
    multi : true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
