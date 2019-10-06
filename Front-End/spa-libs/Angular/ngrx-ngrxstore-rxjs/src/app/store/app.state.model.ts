import { Product, CartItem } from './product.model';

export interface IAppState {
    cataloge: Array<Product>;
    cart: Array<CartItem>;
}

export class AppState implements IAppState {
    cataloge: Array<Product>;
    cart: Array<CartItem>;

    constructor(_Cataloge: Array<Product>, currentCart: Array<CartItem>) {
        this.cataloge = this.cataloge;
        this.cart = currentCart;
    }
}
