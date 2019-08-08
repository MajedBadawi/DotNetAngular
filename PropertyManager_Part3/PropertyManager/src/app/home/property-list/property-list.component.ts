import { Component, OnInit, ViewChild } from '@angular/core';
import { PropertyDetailService } from 'src/app/shared/property-detail.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource, MatPaginator, MatDialog, MatDialogConfig } from '@angular/material';
import { PropertyDetailComponent } from '../property-detail/property-detail.component';
import { DialogService } from 'src/app/shared/dialog.service';

@Component({
    selector: 'app-property-list',
    templateUrl: './property-list.component.html',
    styleUrls: ['../home.component.css']
})
export class PropertyListComponent implements OnInit {
    propertyList = new MatTableDataSource<Element>();
    displayedColumns: string[] = ['Id', 'Title', 'Address', 'NumberOfRooms', 'Price', 'Update', 'Purchase'];
    @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;

    constructor(private service: PropertyDetailService,
         private toastr: ToastrService,
         private dialog: MatDialog, 
         private dialogService: DialogService) { }

    items:any;
    ngOnInit() {
        this.service.getPropertyList().subscribe(
            data => {
                this.items = data;
                this.propertyList.data = this.items;
            }
        );
    }

    onCreate(){
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.width = "60%";
        this.dialog.open(PropertyDetailComponent, dialogConfig);
    }

    onUpdate(row){
        this.service.populateForm(row);
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.width = "60%";
        this.dialog.open(PropertyDetailComponent, dialogConfig);
    }

    onPurchase(propertyId){
        // if(confirm('Are you sure you want to purchase this property?')){
        //     this.service.purchaseProperty(propertyId).subscribe(res =>
        //         this.showPurchaseStatus(res['body'])
        //     );
        // }
        this.dialogService.openConfirmDialog('Are you sure you want to purchase this property?')
            .afterClosed().subscribe(res => {
                if(res)
                    this.service.purchaseProperty(propertyId).subscribe(res =>
                        this.showPurchaseStatus(res['body'])
                    );
            });
    }

    showPurchaseStatus(status){
        if(status)
            this.toastr.success('Purchased successfully');
        else
            this.toastr.warning('Purchased failed', "Not enough credit");
        location.reload();
    }

}
