import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService} from '../auth.service'

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  form: FormGroup
  hide = true;

  constructor(public auth: AuthService, private formBuildr: FormBuilder) {
    this.form = formBuildr.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
   }

  ngOnInit(): void {
  }

}
