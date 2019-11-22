import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { CartItem } from './../store/product.model';
import { AppState } from './../store/app.state.model';

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
      .subscribe((state: AppState) => this.cart = state.cart);

    console.log(this.cart);
  }

  removeFromCart(product) {
    this.store.dispatch(new Cart.RemoveProduct(product));
  }

}
