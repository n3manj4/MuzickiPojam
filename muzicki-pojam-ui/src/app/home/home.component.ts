import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  answers
  title 

  constructor(private auth: AuthService) {
  }

  ngOnInit(): void {
    this.auth.home().subscribe(res => {
      this.title = res
    })
  }

  step = 0;

    setStep(index: number) {
        this.step = index;
    }

    nextStep(value) {
        this.answers[this.step].answer = value;
        this.step++;
    }

    prevStep() {
        this.step--;
    }

    finish() {
      this.auth.finish(this.answers)
    }
}
