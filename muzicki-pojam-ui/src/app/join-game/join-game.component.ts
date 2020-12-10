import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-join-game',
  templateUrl: './join-game.component.html',
  styleUrls: ['./join-game.component.css']
})
export class JoinGameComponent implements OnInit {

  displayedColumns = ['position', 'name', 'duration', 'noOfPlayers', 'maxPlayers', 'join'];
  dataSource = [];
  
  constructor(private signalService: SignalRService) {
    signalService.groupReceived.subscribe((res: any) => {
      this.dataSource = res;
      console.log(this.dataSource) })
      
   }

  ngOnInit(): void {
  }

  onClick(element: any){
    console.log(element);
  }
}

