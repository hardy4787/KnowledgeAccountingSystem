import {	Routes	} from '@angular/router'
import { ProfileComponent } from './profile/profile.component';
import { UserComponent } from './user/user.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { AuthGuard } from './auth/auth.guard';
import { EducationComponent } from './education/education.component';
import { MainInfoProfileComponent } from './main-info-profile/main-info-profile.component';
import { ProgrammerSkillComponent } from './programmer-skill/programmer-skill.component';
import { ProjectComponent } from './project/project.component';
import { ManagerComponent } from './manager/manager.component';
import { ForbiddenComponent } from './errors/forbidden/forbidden.component';
import { WorkExperienceComponent } from './work-experience/work-experience.component';
import { SkillComponent } from './skill/skill.component';
import { ProfilesComponent } from './profiles/profiles.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';

const profileRoutes: Routes =[
	{ path: '', component: MainInfoProfileComponent},
	{ path: 'education', component: EducationComponent},
	{ path: 'skills', component: ProgrammerSkillComponent},
	{ path: 'projects', component: ProjectComponent},
	{ path: 'work-experience', component: WorkExperienceComponent}
];

export const appRoutes : Routes = [
	{	path : 'profile/:id', component : ProfileComponent, children: profileRoutes, canActivate:[AuthGuard]} ,
	{	path : 'signup', component : UserComponent,
		children : [{path: '', component:SignUpComponent}]
	},
	{ path : 'login', component : UserComponent,
		children : [{path: '', component : SignInComponent}]
	},
	{	path: '', redirectTo:'/login', pathMatch : 'full'	},
	{	path: 'manager', component: ManagerComponent, children:
	[{path:'profiles', component: ProfilesComponent},{ path: 'skills', component : SkillComponent }], canActivate: [AuthGuard], data :{roles:['admin']}	},
	{	path: 'forbidden', component: ForbiddenComponent, canActivate: [AuthGuard]},
	{	path: 'not-found', component: NotFoundComponent, canActivate: [AuthGuard]}
];