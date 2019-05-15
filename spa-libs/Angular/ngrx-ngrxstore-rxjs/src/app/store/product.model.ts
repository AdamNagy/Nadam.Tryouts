export class Product {
    id: number;
    name: string;
    price: number;
}

export class CartItem extends Product {
    CartItemId: number;

    constructor(prod: Product) {

        super();

        this.id = prod.id;
        this.name = prod.name;
        this.price = prod.price;
    }
}
