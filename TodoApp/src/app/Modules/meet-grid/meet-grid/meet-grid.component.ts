import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-meet-grid',
  templateUrl: './meet-grid.component.html',
  styleUrls: ['./meet-grid.component.scss']
})
export class MeetGridComponent implements OnInit {
  @Input() rowData!: any[];
  @Input() columnDefs!: any[];
  @Output() rowClick: EventEmitter<any> = new EventEmitter<any>();

  gridOptions: any;

  constructor() {
    this.gridOptions = {
      onGridReady: () => {
        this.gridOptions.api.sizeColumnsToFit();
      },
      onRowClicked: (event: any) => {
        this.rowClick.emit(event.data);
      }
    };
  }
  ngOnInit(): void {
  }

}
