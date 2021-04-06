
export class UserCredential{
    constructor(public emailId:string, public password:string){
      
    }
}

export class UserDetails{
    firstName ! : string;
    lastName ! : string;
    dateOfBirth ! : Date;
    team ! : string;
    emailId ! : string;
    password ! : string;

}

export class UserData{
    id ! : string
    name ! : string;
    dateOfBirth ! : Date;
    team ! : string;
    emailId ! : string;
}