import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'header-buttons',
  templateUrl: './header-buttons.component.html',
  styleUrls: ['./header-buttons.component.scss']
})
export class HeaderButtonsComponent implements OnInit {

  constructor(private auth: AuthService) {
   }

  ngOnInit(): void {
  }

  logout() {
    this.auth.logout()
  }

}
