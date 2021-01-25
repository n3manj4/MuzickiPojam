import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignalRService } from '../services/signal-r.service';
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
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})

export class ResultComponent implements OnInit {
  
  dataSource = ELEMENT_DATA;
  constructor(private  router: Router, private signalService: SignalRService) { }

  ngOnInit(): void {
    let id = this.router.url.split("/")[2]
    this.signalService.getResults(id)
  }
}
