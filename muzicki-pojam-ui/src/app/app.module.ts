import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService} from './services/auth.service'
import { GameService} from './services/game.service'

import { AuthInterceptor } from "./auth.interceptor";
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component'
import { GameComponent } from './game/game.component'

import { MatExpansionModule } from '@angular/material/expansion';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { NewGameComponent } from './new-game/new-game.component';
import { JoinGameComponent } from './join-game/join-game.component';
import {MatSliderModule} from '@angular/material/slider';
import {MatTableModule} from '@angular/material/table';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatRadioModule} from '@angular/material/radio';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HomeComponent,
    LoginComponent,
    GameComponent,
    NewGameComponent,
    JoinGameComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatDividerModule,
    MatListModule,
    MatSliderModule,
    MatTableModule,
    MatTooltipModule,
    MatRadioModule,
    MatProgressSpinnerModule,

  ],
  providers: [
    GameService,
    AuthService, {
    provide: HTTP_INTERCEPTORS,
    useClass : AuthInterceptor,
    multi : true 
  }],
  bootstrap: [AppComponent], 
})
export class AppModule { }
