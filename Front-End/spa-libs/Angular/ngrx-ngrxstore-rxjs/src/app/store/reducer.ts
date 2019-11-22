import { CartActionTypes, CartActions } from './actions';
import { PRODUCTS } from './cataloge';
import { IAppState, AppState } from './app.state.model';
import { CartItem } from './product.model';

export let initialState: IAppState = {
    cataloge: PRODUCTS,
    cart: []
};

export function reducer(state: IAppState = initialState, action: CartActions) {
    switch (action.type) {
        case CartActionTypes.ADD_PRODUCT:
            const newCartItem = new CartItem(action.payload);
            newCartItem.CartItemId = state.cart.length;
            return new AppState(PRODUCTS,  [...state.cart, newCartItem]);

        case CartActionTypes.REMOVE_PRODUCT:
            const cartItem = action.payload;
            return new AppState(PRODUCTS, state.cart.filter((el) => el.CartItemId !== cartItem.CartItemId));

        case CartActionTypes.INCRISE_PRICE:
            const incrisedPrice = action.payload;
            const newCataloge = state.cataloge.filter((product) => product.id !== incrisedPrice.id);
            newCataloge.push(incrisedPrice);
            return new AppState(newCataloge, state.cart);

        default:
            return state;
    }
}
