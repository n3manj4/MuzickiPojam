import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.css']
})
export class NewGameComponent implements OnInit {

  name: any

  constructor(private signalService: SignalRService) { }

  ngOnInit(): void {
  }

  createGame(){
    this.signalService.newGame(this.name)
  }

}
