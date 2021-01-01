import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { GameViewModel } from '../models/game-view-model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  game: GameViewModel

  constructor(private http: HttpClient) {
  }


   finish(game) {
     console.log(game)
    return this.http.post(`http://localhost:63291/api/game/`, game)
  }

  start(){
    return this.http.get('http://localhost:63291/api/game')
  }

  getGame(id: string) {
    return this.http.get('http://localhost:63291/api/game/' + id)
  }

  getGroups(){
    return this.http.get('http://localhost:63291/api/game/rooms')
  } 
}
