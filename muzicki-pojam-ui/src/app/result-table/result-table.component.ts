import { Component, OnInit, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { MatTable } from '@angular/material/table';

export interface ResultElement {
  position: number;
  answer: string;
  points: number;
}

@Component({
  selector: 'app-result-table',
  templateUrl: './result-table.component.html',
  styleUrls: ['./result-table.component.css']
})
export class ResultTableComponent implements OnInit {

  @ViewChild('table') table: MatTable<Element>;
  @Input() dataSource: ResultElement[]
  @Output() total = new EventEmitter<number>();

  displayedColumns: string[] = ['position', 'answer', 'points'];

  constructor() { }

  ngOnInit(): void {
  }

  getTotalPoints() {
    let result = this.dataSource.map(t => t.points).reduce((acc, value) => acc + value, 0);
    this.total.emit(result)
    return result
  }
}
