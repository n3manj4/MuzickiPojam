import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  numbers = []
  title
  constructor(private auth: AuthService) {
    this.numbers = Array.from(Array(10),(x,i)=>i)
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

    nextStep() {
        this.step++;
        console.log(this.step)
    }

    prevStep() {
        this.step--;
    }

}
