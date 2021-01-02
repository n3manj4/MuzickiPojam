import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../services/auth.service'
import { Router } from '@angular/router';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup
  hide = true;

  constructor(public auth: AuthService, private formBuildr: FormBuilder, private router: Router) {
    this.form = formBuildr.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
   }

  ngOnInit(): void {
    if (this.auth.isAuthenticated) {
      this.router.navigate(["/home"])
    }
  }
  
}
