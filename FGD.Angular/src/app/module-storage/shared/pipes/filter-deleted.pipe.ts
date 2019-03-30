import { Pipe, PipeTransform } from '@angular/core';
import { IDeletable } from '../models/interfaces/IDeletable';

@Pipe({
    name: 'filterDeleted',
    pure: false
})

export class FilterDeletedPipe implements PipeTransform {
    transform(items: IDeletable[]): any {
        return items.filter(item => item.isDeleted !== true);
    }
}