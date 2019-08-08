import { Component, OnInit } from '@angular/core';
import { PropertyDetailService } from 'src/app/shared/property-detail.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

@Component({
    selector: 'app-property-detail',
    templateUrl: './property-detail.component.html',
    styleUrls: ['../home.component.css']
})
export class PropertyDetailComponent implements OnInit {
    
    constructor(private service : PropertyDetailService, private toastr : ToastrService, public dialogRef: MatDialogRef<PropertyDetailComponent>) { }

    ngOnInit() {
    }

    onClick(){
        this.service.propertyform.reset();
    }

    getOperationStatus(){
        return (this.service.propertyform.controls['Id'].value == null);
    }

    onSubmit() {
        if (this.service.propertyform.controls['Id'].value == null){
            this.insertRecord();
            console.log("insertion done");
        } else {
            this.updateRecord();
            console.log("update done");
        }
        this.onClose();
        location.reload();
    }

    onClose(){
        this.service.propertyform.reset();
        this.dialogRef.close();
    }
    
    insertRecord() {
        this.service.addProperty().subscribe(
            res => {
                this.service.propertyform.reset();
                this.toastr.success('Submitted successfully', 'Property added.');
                this.service.getPropertyList();
            },
            err => {
                debugger;
                console.log(err);
            }
        )
    }
      
    updateRecord() {
        this.service.updateProperty().subscribe(
            res => {
                this.service.propertyform.reset();
                this.toastr.info('Submitted successfully', 'Property updated.');
                this.service.getPropertyList();
            },
            err => {
                debugger;
                console.log(err);
            }
        )
    }

}
