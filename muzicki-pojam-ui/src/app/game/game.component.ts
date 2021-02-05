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
    })
  }

  initializeGame() {
    this.gameService.getGame(this.gameId).subscribe(res => {
      this.game = res
      let term = this.game.room.term
      document.getElementById("title").innerHTML = term.toUpperCase()
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
      let id = this.router.url.split("/")[2];
      if (id == "single") {
        this.gameService.finish(end).subscribe(res =>{
          console.log(res)
          this.game.answers = res
          this.submited = true
        })
      }
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
