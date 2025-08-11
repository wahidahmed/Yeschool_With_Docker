import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatIconModule} from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatSortModule } from '@angular/material/sort';
import {MatInputModule } from '@angular/material/input';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    // MatButtonToggleModule, MatIconModule,MatTableModule,MatPaginatorModule,MatSortModule
  ],
  exports:[
    MatButtonToggleModule, MatIconModule,MatTableModule,MatPaginatorModule,MatSortModule,MatInputModule
  ]
})
export class MaterialModuleModule { }