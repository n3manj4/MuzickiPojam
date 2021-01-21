import { Component, OnInit } from '@angular/core';
import { AnswerViewModel } from "../models/answer-view-model";
import { GameViewModel } from "../models/game-view-model";
import { GameService} from "../services/game.service"
import { SignalRService } from '../services/signal-r.service';
import { interval } from 'rxjs';
import { Router } from '@angular/router';
import { GroupViewModel } from '../models/signal-models/signal-view-model';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  answer
  game
  submited 
  words
  minWordNumber
  timer
  gameId: any;
  term

  constructor(private gameService: GameService, private signalService: SignalRService, private router: Router) {     
    this.answer = new AnswerViewModel
    this.game = new GameViewModel
    this.game.answers = []
    this.minWordNumber = false
  }
  
  ngOnInit(): void {
    this.signalService.startGame.subscribe((res) => {
      this.gameId = res.id
      this.router.navigate(["/game", res.id])
      this.initializeGame()
      this.timer = res.duration
      this.startTimer()
    })
  }

  startTimer() {
    const secondsCounter = interval(1000);
    
    const subscription = secondsCounter.subscribe(n => {
      if (this.timer == 0)
        subscription.unsubscribe();
      else
      {
        document.getElementById("timer").innerHTML = this.timer
        this.timer -= 1
      }
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
      this.game.answers.push(this.answer)
      this.answer = new AnswerViewModel
    }

    finish()
    {
      let end = { 
        answers: this.game.answers,
        term: this.term
      }
      this.gameService.finish(end).subscribe(res =>{
        console.log(res)
        this.game.answers = res
        this.submited = true
      })
    }

    clearLyric() {
      this.answer.lyric = ''
      this.minWordNumber = false
    }
}
