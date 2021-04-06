import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit, NgModule, } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserCredential } from '../../model/user';
import { UserService } from '../../service/user.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent  {
  formGroup : FormGroup;
  error !:any

  constructor(private formBuilder:FormBuilder, private userService : UserService, private router:Router){
    this.formGroup = formBuilder.group({
      'userName':[null, Validators.required],
      'password':[null,Validators.required]
    })
  }

  login(){
    let userCredential = new UserCredential(this.formGroup.get('userName')?.value,this.formGroup.get('password')?.value)

    this.userService.login(userCredential).subscribe(
      res =>{
        console.log('HTTP response', res)
        this.router.navigate(['../user'], {state: {data: res}});
      },
      err => {
        console.log('HTTP Error', err)
      },
      () => console.log('HTTP request completed.')
    );
  }
}
