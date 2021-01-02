import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router'

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  title

  get isAuthenticated() {
    return !!localStorage.getItem('token')
  }

  constructor(private http: HttpClient, private router: Router) {}
  
  register(credentials){
    this.http.post('http://localhost:63291/api/account', credentials,
    {responseType: 'text'}).subscribe(res =>{
      localStorage.setItem("user", credentials.username)
      this.authenticate(res)
    })
  }

  login(credentials){
    this.http.post('http://localhost:63291/api/account/login', credentials,
    {responseType: 'text'}).subscribe(res =>{
        localStorage.setItem("user", credentials.username)
        this.authenticate(res)
    })
  }

  logout(){
    localStorage.removeItem("token")
    localStorage.removeItem("user")
    this.router.navigate(["#"])
  }

  authenticate(res) {
    localStorage.setItem('token', res)
    this.home()
  }

  home() {
    this.router.navigate(["/home"])
  }
 }

