import { IDeletable } from './interfaces/IDeletable';

export class FileModel implements IDeletable{

    public constructor(){}

    public WithId(id:number):FileModel{
         this.id = id;
         return this;
    }

    public WithIsDeleted(isDelted:boolean):FileModel{
        this.isDeleted = isDelted;
        return this;
   }

   public WithSize(size:number):FileModel{
        this.sizeInKbs = size;
        return this;
    }

    public WithTitle(title:string):FileModel{
        this.title = title;
        return this;
    }

    public WithDate(date:number):FileModel{
        this.dateCreated = date;
        return this;
    }

    public WithFile(file:File):FileModel{
        this.file = file;
        return this;
    }

    public WithStoredFolderId(id:number){
        this.StoredFolderId = id
        return this;
    }

    id:number;
    title:string;
    isDeleted: boolean;
    sizeInKbs: number;
    dateCreated: number;
    StoredFolderId : number;
    file : File; 
    thumbnailPath : string;
    extention : string;

}