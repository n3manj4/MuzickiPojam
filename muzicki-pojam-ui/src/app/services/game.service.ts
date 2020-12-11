import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { GameViewModel } from '../models/game-view-model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  game

  constructor(private http: HttpClient) {
    this.game = GameViewModel
  }


  finish(game) {
    return this.http.post(`http://localhost:63291/api/game/`, game)
  }

  start(){
    return this.http.get('http://localhost:63291/api/game')
  }

  getGroups(){
    return this.http.get('http://localhost:63291/api/game/groups')
  }
}
