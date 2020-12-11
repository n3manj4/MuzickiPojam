import { Component, OnInit } from '@angular/core';
import { GroupViewModel } from '../models/signal-models/signal-view-model';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.css']
})
export class NewGameComponent implements OnInit {

  group: GroupViewModel

  constructor(private signalService: SignalRService) {
    this.group = new GroupViewModel
    this.group.duration = 60
    this.group.maxPlayers = 2
    this.group.name = ""
   }

  ngOnInit(): void {
  }

  createGame(){
    console.log("slider" + this.group)
    this.signalService.addToGroup(this.group)
  }

}
