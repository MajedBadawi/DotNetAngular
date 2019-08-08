import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as Material from '@angular/material';

@NgModule({
     declarations: [],
     imports: [
          CommonModule,
          Material.MatToolbarModule,
          Material.MatGridListModule,
          Material.MatFormFieldModule,
          Material.MatInputModule,
          Material.MatSelectModule,
          Material.MatButtonModule,
          Material.MatTableModule,
          Material.MatIconModule,
          Material.MatPaginatorModule,
          Material.MatDialogModule
     ],
     exports: [
          Material.MatToolbarModule,
          Material.MatGridListModule,
          Material.MatFormFieldModule,
          Material.MatInputModule,
          Material.MatSelectModule,
          Material.MatButtonModule,
          Material.MatTableModule,
          Material.MatIconModule,
          Material.MatPaginatorModule,
          Material.MatDialogModule
     ]
})
export class MaterialModule { }
