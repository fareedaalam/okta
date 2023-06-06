import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MeetGridComponent } from './meet-grid/meet-grid.component';
import { FormsModule } from '@angular/forms';
import { AgGridModule } from 'ag-grid-angular';




@NgModule({
  declarations: [
    MeetGridComponent
  ],
  imports: [CommonModule, FormsModule, AgGridModule],
  exports:[
    MeetGridComponent
  ]
})
export class MeetGridModule { }
