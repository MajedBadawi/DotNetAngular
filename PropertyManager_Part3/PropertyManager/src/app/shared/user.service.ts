import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from "@angular/common/http";

@Injectable({
     providedIn: 'root'
})
export class UserService {
     constructor(private fb:FormBuilder, private http: HttpClient) { }
     readonly BaseURI = 'http://localhost:5000/api';
     
     formModel = this.fb.group({
          UserName : ['', Validators.required],
          Name : ['', Validators.required],    
          Credit : ['', Validators.required],
          Passwords : this.fb.group({
               Password : ['', [Validators.required, Validators.minLength(6)]],
               ConfirmPassword : ['', Validators.required]
          },{ validator : this.comparePasswords })
     });

     comparePasswords(fb: FormGroup) {
          let confirmPswrdCtrl = fb.get('ConfirmPassword');
          if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
            if (fb.get('Password').value != confirmPswrdCtrl.value)
              confirmPswrdCtrl.setErrors({ passwordMismatch: true });
            else
              confirmPswrdCtrl.setErrors(null);
          }
     }

     register() {
          var body = {
               UserName: this.formModel.value.UserName,
               Name: this.formModel.value.Name,
               Credit: this.formModel.value.Credit,
               Password: this.formModel.value.Passwords.Password
          };
          return this.http.post(this.BaseURI + '/Auth/Register', body);
     }

     login(formData) {
          return this.http.post(this.BaseURI + '/Auth/Login', formData);
     }

     getUserInfo() {
          return this.http.get(this.BaseURI + '/Users');
     }

}
