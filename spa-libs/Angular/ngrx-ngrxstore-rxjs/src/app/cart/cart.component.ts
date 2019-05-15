import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Product, CartItem } from './../store/product.model';
import { IAppState, AppState } from './../store/app.state.model';

import * as Cart from './../store/actions';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styles: []
})
export class CartComponent implements OnInit {

  cart: Array<CartItem> = new Array<CartItem>();

  constructor(private store: Store<AppState>) {  }

  ngOnInit() {

    this.store
      .select(state => state)
      .subscribe(state => this.cart = state.cart.cart);

    console.log(this.cart);
  }

  removeFromCart(product) {
    this.store.dispatch(new Cart.RemoveProduct(product));
  }

}
