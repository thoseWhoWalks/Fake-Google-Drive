import { Pipe, PipeTransform } from '@angular/core';
import { IDeletable } from '../models/interfaces/IDeletable';

@Pipe({
    name: 'filterNotDeleted',
    pure: false
})
export class FilterNotDeletedPipe implements PipeTransform {
    transform(items: IDeletable[]): any {
        return items.filter(item => item.isDeleted === true);
    }
}