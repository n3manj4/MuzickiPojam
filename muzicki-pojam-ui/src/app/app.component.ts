import { Component, OnInit } from "@angular/core";
import { SignalRService } from "../app/services/signal-r.service";
import { SignalViewModel } from "../app/models/signal-models/signal-view-model";
import { AuthService} from './services/auth.service'

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {

  signalList: SignalViewModel[] = [];

  username: any

  constructor(public auth: AuthService, private signalRService: SignalRService) {}

  ngOnInit() {
   this.signalRService.signalReceived.subscribe((signal: SignalViewModel) => {
      this.signalList.push(signal);
    });
  }
}