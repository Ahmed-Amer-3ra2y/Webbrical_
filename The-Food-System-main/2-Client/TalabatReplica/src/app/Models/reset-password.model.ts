export class ResetPassword {
    userID :string = "";
    newPassword:string|null = "";
    confirmPassword:string |null = "";
    token:string = "";
    constructor(userID:string, newPassword:string|null,confirmPassword:string|null,token:string){
        this.userID = userID;
        this.newPassword = newPassword;
        this.confirmPassword = confirmPassword;
        this.token = token;
    }
}
