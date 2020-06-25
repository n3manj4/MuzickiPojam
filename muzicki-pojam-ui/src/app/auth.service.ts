import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  get isAuthenticated() {
    return !!localStorage.getItem('token')
  }

  constructor(private http: HttpClient, private router: Router) {}
  
  register(credentials){
    this.http.post('http://localhost:63291/api/account', credentials,
    {responseType: 'text'}).subscribe(res =>{
      this.authenticate(res)
    })
  }

  login(credentials){
    this.http.post('http://localhost:63291/api/account/login', credentials,
    {responseType: 'text'}).subscribe(res =>{
        this.authenticate(res)
    })
  }

  logout(){
    localStorage.removeItem("token")
  }

  authenticate(res) {
    localStorage.setItem('token', res)

    this.router.navigate(["/home"])
  }



  home(){
      console.log('home')
      this.http.get('http://localhost:63291/api/home').subscribe(result => {
        console.log(result)
      })
    }
  }

