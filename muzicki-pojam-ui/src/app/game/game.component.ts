import { Component, OnInit } from '@angular/core';
import { AnswerViewModel } from "../models/answer-view-model";
import { GameViewModel } from "../models/game-view-model";
import { GameService} from "../services/game.service"
import { Router } from '@angular/router'

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

  constructor(private gameService: GameService, private router: Router) {     
    this.answer = new AnswerViewModel
    this.game = new GameViewModel
    this.game.answers = []
    this.words
    this.minWordNumber = false
  }

  ngOnInit(): void {
    this.gameService.start().subscribe(res => {
      this.game = res
      console.log(this.game)
      this.title = this.game.term
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
      this.gameService.finish(this.game).subscribe(res =>{
        console.log(res)
        this.game = res
        this.submited = true
      })
    }

    clearLyric() {
      this.answer.lyric = ''
      this.minWordNumber = false
    }
}
