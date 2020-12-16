import { Component, OnInit } from '@angular/core';
import { GroupViewModel } from '../models/signal-models/signal-view-model';
import { SignalRService } from '../services/signal-r.service';
import { TeamEnum } from '../models/app-enums';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.css']
})
export class NewGameComponent implements OnInit {

  group: GroupViewModel

  constructor(private signalService: SignalRService, private router: Router) {
    this.group = new GroupViewModel
    this.group.duration = 60
    this.group.maxPlayers = 2
    this.group.name = ""
   }

  ngOnInit(): void {
  }

  createGame(){
    this.signalService.addToGroup(this.group)
    console.log(this.group)
    //this.router.navigate(["/home", this.group.id])
  }

  radioChanged(team: TeamEnum){
    this.group.team = team
  }

}
