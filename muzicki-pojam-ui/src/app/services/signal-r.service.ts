import { Injectable, EventEmitter } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { GameViewModel } from "../models/game-view-model";
import { GroupViewModel, SignalViewModel } from "../models/signal-models/signal-view-model";

@Injectable({
  providedIn: "root"
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  signalReceived = new EventEmitter<SignalViewModel>();
  groupReceived = new EventEmitter<GroupViewModel>();
  startGame = new EventEmitter<GameViewModel>();
  results = new EventEmitter();

  
  constructor() {
    this.buildConnection();
    this.startConnection();
  }
  
  private buildConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:63291/signalHub") //use your api adress here and make sure you use right hub name.
    .build();
  };
  
  private startConnection = () => {
    this.hubConnection
    .start()
    .then(() => {
      console.log("Connection Started...");
      this.registerSignalEvents();
    })
    .catch(err => {
      console.log("Error while starting connection: " + err);

      //if you get error try to start connection again after 3 seconds.
      setTimeout(function() {
        this.startConnection();
      }, 3000);
    });
  };
  
  private registerSignalEvents() {
    this.hubConnection.on("SignalMessageReceived", (data: SignalViewModel) => {
      this.signalReceived.emit(data);
    });
    this.hubConnection.on("GroupReceived", (data: GroupViewModel) => {
      console.log(data)
      this.groupReceived.emit(data);
    });
    this.hubConnection.on("StartGame", (data: GameViewModel) => {
      this.startGame.emit(data)
    });
    this.hubConnection.on("getResults", () => {
      this.results.emit()
    });
  }
  
  getResults(id: string) {
    this.hubConnection.send("getResults", id)
  }
  
  public addToGroup(group: any) {
    console.log("Group: ", group)
    let username = localStorage.getItem("name")
    this.hubConnection.send("joinGroup", group, username)
  }
  
  public startSingleGame(userName) {
    this.hubConnection.send("StartSingleGame", userName)
  }
}