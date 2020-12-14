import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../services/signal-r.service';
import { GameService} from "../services/game.service"
import { GroupViewModel } from '../models/signal-models/signal-view-model';
import { TeamEnum } from '../models/app-enums';

@Component({
  selector: 'app-join-game',
  templateUrl: './join-game.component.html',
  styleUrls: ['./join-game.component.css']
})
export class JoinGameComponent implements OnInit {

  disableRed
  disableBlue

  displayedColumns = ['position', 'name', 'duration', 'noOfPlayers', 'maxPlayers', 'join'];
  dataSource = [];
  groupToJoin = new GroupViewModel
  
  constructor(private signalService: SignalRService, private gameService: GameService,) {
    signalService.groupReceived.subscribe((res: any) => {
      this.dataSource = res;
      console.log(this.dataSource) })

    gameService.getGroups().subscribe((res: any) => {
      this.dataSource = res
    })
      
   }

  ngOnInit(): void {
  }

  joinRedTeam(element: GroupViewModel){
    if (element.maxPlayers / 2 == element.noOfPlayers + 1)
    {
      this.disableRed = true
    }
    element.team = TeamEnum.Red
    this.signalService.addToGroup(element)
  }

  joinBlueTeam(element: any){
    if (element.maxPlayers / 2 == element.noOfPlayers + 1)
    {
      this.disableBlue = true
    }
    element.team = TeamEnum.Blue
    this.signalService.addToGroup(element)
  }
}

