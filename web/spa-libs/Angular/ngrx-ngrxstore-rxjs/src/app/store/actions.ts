import { Action } from '@ngrx/store';

export enum CartActionTypes {
    ADD_PRODUCT = '[Cart] ADD_PRODUCT',
    REMOVE_PRODUCT = '[Cart] REMOVE_PRODUCT',
    INCRISE_PRICE = '[Cart] INCRISE_PRICE'
}

export class AddProduct implements Action {
    readonly type = CartActionTypes.ADD_PRODUCT;
    constructor(public payload: any) {}
}

export class RemoveProduct implements Action {
    readonly type = CartActionTypes.REMOVE_PRODUCT;
    constructor(public payload: any) {}
}

export class IncrisePrice implements Action {
    readonly type = CartActionTypes.INCRISE_PRICE;
    constructor(public payload: any) {}
}

export type CartActions = AddProduct | RemoveProduct | IncrisePrice;
