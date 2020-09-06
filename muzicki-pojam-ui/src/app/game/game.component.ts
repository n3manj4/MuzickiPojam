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

  constructor(private gameService: GameService, private router: Router) {     
    this.answer = new AnswerViewModel
    this.game = new GameViewModel
    this.game.answers = []
    this.title = ''
  }

  ngOnInit(): void {
    this.gameService.start().subscribe(res => {
      this.game = res
      console.log(this.game)
      this.title = this.game.term
      //this.router.navigate(['/game', this.game.id]);
    })
  }
  

  clearInput()
    {
      this.answer.title = ''
      this.answer.singer = ''
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

    getColor(answer){
      if (answer.isCorrectAnswer)
      {
        return "green"
      }
      else{
        return "red"
      }
    }
}
