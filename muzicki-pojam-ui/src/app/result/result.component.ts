import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResultsViewModel } from '../models/ResultsViewModel';
import { SignalRService } from '../services/signal-r.service';
export interface ResultElement {
  position: number;
  answer: string;
  points: any;
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
  styleUrls: ['./result.component.scss']
})

export class ResultComponent implements OnInit {

  redPoints: number
  bluePoints: number
  
  redTeamResults  
  blueTeamResults 

  constructor(private  router: Router, private signalService: SignalRService) {
    this.redPoints = 0;
    this.bluePoints = 0;
   }

  ngOnInit(): void {
    this.signalService.results.subscribe((res: ResultsViewModel) => {
      this.redTeamResults = res.redTeamResults
      this.blueTeamResults = res.blueTeamResults
    })
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
