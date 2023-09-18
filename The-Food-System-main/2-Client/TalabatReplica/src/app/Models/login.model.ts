export class Login {
    email:string|null="";
    password:string|null="";
    constructor(email:string|null , pass:string|null)
    {
        this.email=email;
        this.password=pass;
    };

}