import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registeration',
  templateUrl: './registeration.component.html',
  styles: []
})
export class RegisterationComponent implements OnInit {

  constructor(public service : UserService, private toastr : ToastrService) { }

  ngOnInit() {
     this.service.formModel.reset();
  }

  onSubmit() {
       this.service.register().subscribe(   
          (res: any) => {
               if (res.Succeeded) {        
                    this.service.formModel.reset();
                    this.toastr.success('New user created!', 'Registration successful.');
               } else { //failed
                    var err = res.Errors;                  
                    err.forEach(element => {
                         switch (element.Code) {
                              case 'DuplicateUserName':
                                   this.toastr.error('Username is already taken','Registration failed.');
                                   break;
                              default:
                                   this.toastr.error(element.Description,'Registration failed.');
                                   break;
                         }
                    });
               }
          },
          err => {
               console.log(err);
          }
     );

  }

}