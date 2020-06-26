import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  numbers = []
  constructor() {
    this.numbers = Array.from(Array(10),(x,i)=>i)
    console.log(this.numbers)
   }

  ngOnInit(): void {
  }

  step = 0;

    setStep(index: number) {
        this.step = index;
    }

    nextStep() {
        this.step++;
        console.log(this.step)
    }

    prevStep() {
        this.step--;
    }

}
