import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user',
  template:`
  <h2>{{title}}</h2>
  <ul>
  <li *ngFor="let user of users">
  {{user}}
  </li>
  </ul>
  `
})
export class UserComponent {
  title = "Users";
  users;

  constructor(service:UserService){
    this.users = service.getUsers();
  }

}
