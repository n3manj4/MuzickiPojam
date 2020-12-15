import { Component, OnInit, ViewChild } from '@angular/core';
import { SignalRService } from '../services/signal-r.service';
import { GameService} from "../services/game.service"
import { GroupViewModel } from '../models/signal-models/signal-view-model';
import { TeamEnum } from '../models/app-enums';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-join-game',
  templateUrl: './join-game.component.html',
  styleUrls: ['./join-game.component.css']
})
export class JoinGameComponent implements OnInit {
  @ViewChild('table') table: MatTable<Element>;

  displayedColumns = ['position', 'name', 'duration', 'noOfPlayers', 'maxPlayers', 'join'];
  dataSource = [];
  groupToJoin = new GroupViewModel
  
  constructor(private signalService: SignalRService, private gameService: GameService,) {
    signalService.groupReceived.subscribe((res: any) => {
      let rowIndex = this.dataSource.findIndex(x => x.id == res.id)
      if (rowIndex < 0) {
        this.dataSource.push(res.room)
      }
      else
        this.dataSource[rowIndex] = res.room
        
      this.table.renderRows()
    })

    gameService.getGroups().subscribe((res: any) => {
      this.dataSource = res
    })
      
   }

  ngOnInit(): void {

  }

  shouldDisable(element: GroupViewModel, team: TeamEnum)
  {
    let count = team == 0 ? element.redPlayersCount : element.bluePlayersCount
    if (count == element.maxPlayers/2)
      return true;
  }

  joinRedTeam(element: GroupViewModel){
    element.team = TeamEnum.Red
    this.signalService.addToGroup(element)
  }

  joinBlueTeam(element: any){
    element.team = TeamEnum.Blue
    this.signalService.addToGroup(element)
  }
}

