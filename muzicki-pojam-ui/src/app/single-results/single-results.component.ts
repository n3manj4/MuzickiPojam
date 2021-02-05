import { Component, OnInit } from '@angular/core';
import { ResultsViewModel } from '../models/ResultsViewModel';
import { ResultElement } from '../result/result.component';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-single-results',
  templateUrl: './single-results.component.html',
  styleUrls: ['./single-results.component.scss']
})
export class SingleResultsComponent implements OnInit {

  results

  constructor(private signalService: SignalRService) { }

  ngOnInit(): void {
    this.signalService.results.subscribe((res: ResultsViewModel) => {
      this.results = res.redTeamResults
    })
  }

}
