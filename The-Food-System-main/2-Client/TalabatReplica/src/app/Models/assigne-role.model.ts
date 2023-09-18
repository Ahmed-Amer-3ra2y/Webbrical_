export class AssigneRole {

    userId: string|null="";
    role: string|null="";

    constructor(userId: string|null,  role: string|null)
    {
        this.userId=userId;
        this.role=role;
    }
}
