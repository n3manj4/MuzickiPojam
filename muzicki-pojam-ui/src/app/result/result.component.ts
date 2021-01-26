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
  {position: 4, answer: 'Mile Kitić - Vuk samotnjak', points: 3},
  {position: 4, answer: 'Mile Kitić - Vuk samotnjak', points: 3},
  ];

  const ELEMENT_DATA2: ResultElement[] = [
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

  redPoints: number
  bluePoints: number
  
  dataSource1 = ELEMENT_DATA 
  dataSource2 = ELEMENT_DATA2
  constructor(private  router: Router, private signalService: SignalRService) {
    this.redPoints = 0;
    this.bluePoints = 0;
   }

  ngOnInit(): void {
    let id = this.router.url.split("/")[2]
    this.signalService.getResults(id)
  }

  onTotalRed(total: number) {
    this.redPoints = total;
  }

  onTotalBlue(total: number) {
    this.bluePoints = total;
  }

  isBlueWon() {
    return this.bluePoints > this.redPoints
  }

  isRedWon() {
    return this.bluePoints < this.redPoints
  }
}
