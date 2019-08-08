import { AuthGuard } from './auth/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegisterationComponent } from './user/registeration/registeration.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
     {
          path:'', redirectTo: '/user/register', pathMatch:'full' // URI: /
     },
     {
          path:'user', component:UserComponent, // URI: /user/
               children:[
                    { path:'register', component:RegisterationComponent }, // URI: /user/register
                    { path:'login', component:LoginComponent } // URI: /user/login
               ]
     },
     { path:'home', component:HomeComponent, canActivate:[AuthGuard] } // URI: /home
];

@NgModule({
     imports: [RouterModule.forRoot(routes)],
     exports: [RouterModule]
})
export class AppRoutingModule { }
