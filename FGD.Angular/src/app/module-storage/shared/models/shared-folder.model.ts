
export class SharedFolderModel {

    public constructor(){}

    public WithId(id:number):SharedFolderModel{
         this.id = id;
         return this;
    } 

    public WithStoredFolderId(id:number){
        this.storedFolderId = id
        return this;
    }

    public WithAccountEmail(email:string){
        this.accountEmail = email;
        return this;
    }

    id:number; 
    storedFolderId : number;
    accountEmail : string

}