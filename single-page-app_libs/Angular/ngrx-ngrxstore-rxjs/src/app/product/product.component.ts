import { Component, OnInit } from '@angular/core';
import { PRODUCTS } from '../store/cataloge';
import { Product } from './../store/product.model';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import * as Cart from './../store/actions';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styles: []
})
export class ProductComponent implements OnInit {

  private product: Product = PRODUCTS[0];

  constructor(
    private route: ActivatedRoute,
    private store: Store<any>) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.product = PRODUCTS[id];
  }

  addToCart(product): void {
        this.store.dispatch(new Cart.AddProduct(product));
  }

  incrisePrice(product): void {
    const incrisedPrice: Product = product;
    product.price *= 1.2;
    this.store.dispatch(new Cart.IncrisePrice(incrisedPrice));
  }

}
