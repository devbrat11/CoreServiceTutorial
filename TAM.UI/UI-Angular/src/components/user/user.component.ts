import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserData } from 'src/model/user';
import { UserService } from 'src/service/user.service';

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
   userId! :string;
   userName! :string;
   dob! : Date;
   team! : string;
   emailId! : string;

  constructor(private router:Router, private userService:UserService) { }

  ngOnInit(): void {
   this.userId = history.state.data;

   this.userService.getUser(this.userId).subscribe(
    res =>{
      console.log('HTTP response', res)
      let data = res as UserData;
      this.userName = data.name
      this.dob = data.dateOfBirth;
      this.team = data.team;
      this.emailId= data.emailId;
    },
    err => {
      console.log('HTTP Error', err)
    },
    () => console.log('HTTP request completed.')
  );
}

}
