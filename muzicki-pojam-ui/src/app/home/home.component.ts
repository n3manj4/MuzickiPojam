import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  user
 
  constructor(private router: Router, private signal: SignalRService) {
      this.user = localStorage.getItem("user")
  }

  ngOnInit(): void {
  }

  startSingleGame() {
    this.signal.startSingleGame(this.user)
    this.router.navigate(["/game/single"])
  }
}
