import { Component, OnInit } from '@angular/core';
import { AnswerViewModel } from "../models/answer-view-model";
import { GameViewModel } from "../models/game-view-model";
import { GameService} from "../services/game.service"
import { SignalRService } from '../services/signal-r.service';
import { interval } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  answer
  game
  title 
  submited 
  words
  minWordNumber
  startGame
  timer
  gameId: any;

  constructor(private gameService: GameService, private signalService: SignalRService, private router: Router) {     
    this.answer = new AnswerViewModel
    this.game = new GameViewModel
    this.game.answers = []
    this.minWordNumber = false
    this.startGame = false
    this.timer = 0

    signalService.startGame.subscribe((res) => {
      this.startGame = true
      this.gameId = res.id
      this.router.navigate(["/game", res.id])
      this.initializeGame()
    })
  }

  ngOnInit(): void {
    
    // Create an Observable that will publish a value on an interval
    const secondsCounter = interval(1000);
    // Subscribe to begin publishing values
    const subscription = secondsCounter.subscribe(n => {
      if (this.timer == 0)
        subscription.unsubscribe();
      else
        this.timer -= 10
    });
  }

  initializeGame() {
    this.gameService.getGame(this.gameId).subscribe(res => {
      this.game = res
      document.getElementById("title").innerHTML = this.game.room.term
    })
  }
  
  clearInput()
    {
      this.answer.title = ''
      this.answer.singer = ''
      var words = this.answer.lyric.match(/\b[-?(\w+)?]+\b/gi);
      if (words != null)
        this.minWordNumber = words.length > 3 
      else
        this.minWordNumber = false
    }
    
    next() {
      console.log(this.answer)
      this.game.answers.push(this.answer)
      this.answer = new AnswerViewModel
    }

    finish()
    {
      /* this.gameService.finish(this.game).subscribe(res =>{
        console.log(res)
        this.game = res
        this.submited = true
      }) */
    }

    clearLyric() {
      this.answer.lyric = ''
      this.minWordNumber = false
    }
}
