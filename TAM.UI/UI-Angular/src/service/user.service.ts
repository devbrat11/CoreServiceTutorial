import { UserCredential, UserDetails } from "src/model/user";
import { HttpClient, HttpHeaders, HttpParams, HttpParamsOptions } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class UserService{

    baseUrl :string = "https://localhost:44331/api/Users";

    constructor(private http:HttpClient){

    }

    login(userCredential:UserCredential) {
        let content = JSON.stringify(userCredential);
        return this.http.post(this.baseUrl+"/authenticate",content,httpOptions);
    }

    registerUser(userDetails : UserDetails) {
        let content = JSON.stringify(userDetails);
        return this.http.post(this.baseUrl,content,httpOptions);
    }

    getUser(userId:string){
        return this.http.get(this.baseUrl+"/"+userId);
    }

}