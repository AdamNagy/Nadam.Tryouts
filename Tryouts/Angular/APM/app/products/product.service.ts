import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

import { IProduct } from './product';

@Injectable()
export class ProductService {
    //private _productUrl = 'http://localhost:29790/Home/GetProducts';  // this one works of service is running
    private _productUrl = 'api/products/products.json';

    constructor(private _http: Http) {}

    getProducts(): Observable<IProduct[]> {
        let ret = this._http.get(this._productUrl)
                    //.map((response: Response) => <IProduct[]> response.json())
                    //.do(data => this.testResponse)
                    .catch(this.handleError);
        //this.testResponse(ret);
        return ret;
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }

    private testResponse(input: IProduct[]) {
        input.forEach(i => console.log(i.productCode));
    }
}
