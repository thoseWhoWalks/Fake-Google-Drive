

export default class SubscriptionInfoModel {

    public constructor(){}

    public WithId(id:number):SubscriptionInfoModel{
         this.id = id;
         return this;
    }
 
    public WithTitle(title:string):SubscriptionInfoModel{
        this.title = title;
        return this;
    }

    public WithTotlaSpace(totalSpace:number):SubscriptionInfoModel{
        this.totalSpace = totalSpace;
        return this;
    }

    public WithFreeSpace(freeSpace:number):SubscriptionInfoModel{
        this.freeSpace = freeSpace;
        return this;
    }

    public WithTakenSpace(takenSpace:number):SubscriptionInfoModel{
        this.takenSpace = takenSpace;    
        return this;
    }

    id:number;
    title:string;
    totalSpace: number;
    takenSpace : number;
    freeSpace: number;

}