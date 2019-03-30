export class AccountModel{
    
    public Id : number;
    public Email : string;
    public FirstName : string;
    public LastName : string;
    public Age : number;
    public Password : string;
    public Role : string;


    public WithId(id:number){
        this.Id = id;
        return this;
    }

    public WithRole(role:string){
        this.Role = role;
        return this;
    }

    public WithAge(age:number){
        this.Age = age;
        return this;
    }

    public WithEmail(email:string){
        this.Email = email;
        return this;
    }

    public WithLastName(lastName:string){
        this.LastName = lastName;
        return this;
    }

    public WithFirstName(firstName:string){
        this.FirstName = firstName;
        return this;
    }

    public WithPassword(password:string){
        this.Password = password;
        return this;
    }
}