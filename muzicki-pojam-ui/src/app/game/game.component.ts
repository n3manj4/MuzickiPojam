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
  styleUrls: ['./game.component.scss']
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
  inProgress: boolean;

  constructor(private gameService: GameService, private signalService: SignalRService, private router: Router) {     
    this.answer = new AnswerViewModel
    this.game = new GameViewModel
    this.game.answers = []
    this.minWordNumber = false
    this.inProgress = true
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
      if (this.timer >= 0) {
        document.getElementById("timer").innerHTML = this.timer
        this.timer -= 10
      }
      else {
        //this.goToResultPage()
        subscription.unsubscribe();
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
      /*this.gameService.finish(end).subscribe(res =>{
        console.log(res)
        this.game.answers = res
        this.submited = true
      })*/
      let id = this.router.url.split("/")[2]
      this.signalService.validateAnswers(id, this.game.answers)
      this.goToResultPage()
    }

    clearLyric() {
      this.answer.lyric = ''
      this.minWordNumber = false
    }

    goToResultPage() {
      this.router.navigate([this.router.url + "/result"])
    }
}
