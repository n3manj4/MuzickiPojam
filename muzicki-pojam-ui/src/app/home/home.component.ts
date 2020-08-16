import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { AnswerViewModel } from "../models/signal-models/answer-view-model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  answer
  answers
  title 

  constructor(private auth: AuthService) {
    this.answer = new AnswerViewModel
    this.answers = []
  }

  ngOnInit(): void {
    this.auth.home().subscribe(res => {
      this.title = res
    })
  }

    clearInput()
    {
      this.answer.title = ''
      this.answer.singer = ''
    }
    
    next() {
      console.log(this.answer)
      this.answers.push(this.answer)
      this.answer = new AnswerViewModel
    }

    finish()
    {
      //this.auth.finish(this.answers)
    }
}
