export  class SharedFileModel {

    public constructor(){}

    public WithId(id:number):SharedFileModel{
         this.id = id;
         return this;
    } 

    public WithStoredFileId(id:number){
        this.storedFileId = id
        return this;
    }

    public WithAccountEmail(email:string){
        this.accountEmail = email;
        return this;
    }

    id:number; 
    storedFileId : number;
    accountEmail : string

}