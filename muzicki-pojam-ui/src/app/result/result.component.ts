import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {

  constructor(private router: Router, private signalService: SignalRService) { }

  ngOnInit(): void {
    let id = this.router.url.split("/")[2]
    this.signalService.getResults(id)
  }

}
