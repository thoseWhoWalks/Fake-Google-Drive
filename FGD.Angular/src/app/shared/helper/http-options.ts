import { HttpHeaders } from '@angular/common/http';

export class HttpOptions {
    private options = {};
    constructor(params?: {}) {
        if (params !== undefined) {
            this.options = params;
            return;
        }
        this.options = {
            headers: new HttpHeaders({
                'Access-Control-Allow-Origin': '*',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            })
        };
    }
    toObject() {
        return this.options;
    }
}
