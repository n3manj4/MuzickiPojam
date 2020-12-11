import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../services/signal-r.service';
import { GameService} from "../services/game.service"
import { GroupViewModel } from '../models/signal-models/signal-view-model';

@Component({
  selector: 'app-join-game',
  templateUrl: './join-game.component.html',
  styleUrls: ['./join-game.component.css']
})
export class JoinGameComponent implements OnInit {

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

  joinRedTeam(element: any){
    this.groupToJoin.duration = element.duration
    this.groupToJoin.maxPlayers = element.maxPlayers
    this.groupToJoin.name = element.name
    this.groupToJoin.noOfPlayers = element.noOfPlayers
    this.groupToJoin.position = this.groupToJoin.position
    this.signalService.addToGroup(this.groupToJoin)
  }

  joinBlueTeam(element: any){
    console.log(element);
  }
}

