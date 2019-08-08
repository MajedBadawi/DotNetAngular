import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PropertyDetail } from './property-detail.model';

@Injectable({
     providedIn: 'root'
})
export class PropertyDetailService {
     readonly rootURL = 'http://localhost:5000/api';

     constructor(private http: HttpClient) { }

     propertyform: FormGroup = new FormGroup({
          Id: new FormControl(null),
          Title: new FormControl('', Validators.required),
          Address: new FormControl('', Validators.required),
          NumberOfRooms: new FormControl('', Validators.required),
          Price: new FormControl('')
     });

     filterform: FormGroup = new FormGroup({
          FromPrice: new FormControl(''),
          ToPrice: new FormControl(''),
          NumberOfRooms: new FormControl(''),
          Address: new FormControl('')
     });

     getPropertyList(){
          return this.http.get(this.rootURL + '/Properties');
     }

     addProperty(){
          var property = {
               "Title":this.propertyform.controls['Title'].value,
               "Address":this.propertyform.controls['Address'].value,
               "NumberOfRooms":this.propertyform.controls['NumberOfRooms'].value
          }
          return this.http.post(this.rootURL + '/Properties', property);
     }

     populateForm(property){
          this.propertyform.setValue({
               Id: property.Id,
               Title: property.Title,
               Address: property.Address,
               NumberOfRooms: property.NumberOfRooms,
               Price: property.Price
          });
     }
     
     updateProperty() {
          var property = {
               "Title":this.propertyform.controls['Title'].value,
               "Address":this.propertyform.controls['Address'].value,
               "NumberOfRooms":this.propertyform.controls['NumberOfRooms'].value,
               "Price":this.propertyform.controls['Price'].value,
          }
          return this.http.put(this.rootURL + '/Properties/' + this.propertyform.controls['Id'].value, property);
     }

     purchaseProperty(propertyId){
          return this.http.put(this.rootURL + '/Users/' + propertyId, null, { observe: 'response' });
     }

     // filterPropertyList(){
     //      var filter = this.filterform;
     //      var query = '?';
     //      if(filter.fromPrice != null && filter.toPrice != null)
     //           query += 'FromPrice=' + filter.fromPrice + '&ToPrice=' + filter.toPrice;
     //      if(filter.numberOfRooms != null){
     //           if(query == '?')
     //                query += 'NumberOfRooms=' + filter.numberOfRooms;
     //           else
     //                query += '&NumberOfRooms=' + filter.numberOfRooms;
     //      }
     //      if(filter.address != ''){
     //           if(query == '?')
     //                query += 'Address=' + filter.address;
     //           else
     //                query += '&Address=' + filter.address;
     //      }
     //      this.http.get(this.rootURL + '/Properties/filter' + query)
     //           .toPromise()
     //           .then(res => this.list = res as PropertyDetail[]);
     // }

}
