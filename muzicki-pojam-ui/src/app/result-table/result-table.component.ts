import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTable } from '@angular/material/table';

export interface ResultElement {
  position: number;
  answer: string;
  points: number;
}

const ELEMENT_DATA: ResultElement[] = [
  {position: 1, answer: 'Mile Kitić - Vuk samotnjak', points: 3},
  {position: 2, answer: 'Džej - Ugasila si me', points: 5},
  {position: 3, answer: 'Mile Kitić - Vuk samotnjak', points: 0},
  {position: 4, answer: 'Mile Kitić - Vuk samotnjak', points: 3},
  ];

@Component({
  selector: 'app-result-table',
  templateUrl: './result-table.component.html',
  styleUrls: ['./result-table.component.css']
})
export class ResultTableComponent implements OnInit {

  @Input() dataSource: ResultElement[]

  @ViewChild('table') table: MatTable<Element>;
  displayedColumns: string[] = ['position', 'answer', 'points'];

  constructor() { }

  ngOnInit(): void {
  }

  getTotalCost() {
    return this.dataSource.map(t => t.points).reduce((acc, value) => acc + value, 0);
  }
}
