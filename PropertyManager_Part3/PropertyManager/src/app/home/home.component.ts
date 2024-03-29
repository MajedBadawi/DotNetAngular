import { UserService } from './../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
     selector: 'app-home',
     templateUrl: './home.component.html',
     styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
     userInfo;
     constructor(private router: Router, private service: UserService) { }

     ngOnInit() {
          this.service.getUserInfo().subscribe(
               res => {
                    this.userInfo = res;
               },
               err => {
                    console.log(err);
               },
          );
     }

     onLogout() {
          localStorage.removeItem('token');
          this.router.navigate(['/user/login']);
     }

}
