export class AccountSignUpModel{
    
    public Email : string;
    public FirstName : string;
    public LastName : string;
    public Password : string;
    public Age : number;

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

    public WithAge(age:number){
        this.Age = age;
        return this;
    }
}