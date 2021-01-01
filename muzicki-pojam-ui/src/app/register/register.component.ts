import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../services/auth.service'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup
  hide = true;

  constructor(public auth: AuthService, private formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }
  
  ngOnInit(): void {
  }

}
