import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { GameComponent } from './game/game.component';
import { NewGameComponent } from './new-game/new-game.component';
import { JoinGameComponent } from './join-game/join-game.component';
import { ResultComponent } from './result/result.component';
import { SingleResultsComponent } from './single-results/single-results.component';


const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'home', component: HomeComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'game', component: GameComponent},
  { path: 'game/:id', component: GameComponent},
  { path: 'new-game', component: NewGameComponent},
  { path: 'join-game', component: JoinGameComponent},
  { path: 'game/single/result', component: SingleResultsComponent},
  { path: 'game/:id/result', component: ResultComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
