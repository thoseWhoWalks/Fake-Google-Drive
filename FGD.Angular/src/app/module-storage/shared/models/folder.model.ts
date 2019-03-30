import { IDeletable } from './interfaces/IDeletable';

export class FolderModel implements IDeletable{
    
    constructor() { }

    public WithId(id : number):FolderModel{
        this.id = id;
        return this;
    }

    public WithTitle(title : string):FolderModel{
        this.title = title;
        return this;
    }

    public WithDateCreated(dateCreated : number):FolderModel{
        this.dateCreated = dateCreated;
        return this;
    }

    public WithIsDeleted(isDeleted : boolean):FolderModel{
        this.isDeleted = isDeleted;
        return this;
    }

    public WithStoredFolderId(storedFolderId : number):FolderModel{
        this.storedFolderId = storedFolderId;
        return this;
    }

    id:number;
    title:string;
    dateCreated: number;
    storedFolderId: number;
    isDeleted: boolean;
    sizeInKbs: number;

} 