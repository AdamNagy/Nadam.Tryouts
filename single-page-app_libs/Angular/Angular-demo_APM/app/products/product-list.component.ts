import { Component, OnInit } from '@angular/core';
import { IProduct } from './product';
import { ProductService } from './product.service';

@Component({
    // selector: 'pm-products',
    moduleId: module.id,
    templateUrl: 'product-list.component.html',
    styleUrls: ['product-list.component.css']
})
export class ProductListComponent implements OnInit {
    pageTitle: string = 'Product List';
    imageWidth: number = 50;
    imageMargin: number = 2;
    showImage: boolean = false;
    listFilter: string;
    errorMessage: string;
    products: IProduct[] = [];

    constructor(private _productService: ProductService) {

    }

    toggleImage(): void {
        this.showImage = !this.showImage;
    }

    ngOnInit(): void {
        console.log('On Init');
        this._productService.getProducts()
            .subscribe(
                prod => this.testResponse(prod),
                error => this.errorMessage = <any>Error);

        this.products.forEach(i => console.log(i.productCode));
    }

    private testResponse(input: any) {
        let prods = <IProduct[]> input.json();
        prods.forEach(i => console.log(i.productCode));
        this.products = prods;
    }

    onRatingClicked(message: string): void {
        this.pageTitle = 'Product list ' + message;
    }
}
