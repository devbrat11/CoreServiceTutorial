import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { UserDetails } from 'src/model/user';
import { UserService } from 'src/service/user.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm!:FormGroup

  constructor(private formBuilder:FormBuilder, private userService : UserService){
    this.registerForm = formBuilder.group({
      'firstName':[null, Validators.required],
      'lastName':[null,Validators.required],
      'dateOfBirth':[null,Validators.required],
      'team':[null,Validators.required],
      'emailId':[null,Validators.required],
      'password':[null,Validators.required],
    })
  }

  register(){
    let userDetails = new UserDetails();
    userDetails.firstName = this.registerForm.get('firstName')?.value;
    userDetails.lastName = this.registerForm.get('lastName')?.value;
    userDetails.dateOfBirth = this.registerForm.get('dateOfBirth')?.value;
    userDetails.team = this.registerForm.get('team')?.value;
    userDetails.emailId = this.registerForm.get('emailId')?.value;
    userDetails.password = this.registerForm.get('password')?.value;
    let status:any;

    this.userService.registerUser(userDetails).subscribe(
      res =>{
        console.log('HTTP response', res)
        this.registerForm.reset();
      },
      err => {
        this.handleError(err);
      },
      () => console.log('HTTP request completed.')
    );

  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }

}
