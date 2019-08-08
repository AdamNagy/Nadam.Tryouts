import m from "mithril";
import { store } from '../store/store';

class Greeter {
    view(vnode) {
        return m("div", vnode.attrs, ["Hello ", vnode.children])
    }
}

let greeter = new Greeter();

export class ChildComponent {

	constructor() {
	}

	view(vnode) {
		return m("h1",vnode.attrs, `Hello H1 ${vnode.attrs.custAttribute}`);
	}
}

export class TodosComponent {
	count: number = 123;
	name: string;
	age: number;

	constructor(vnode) {
		const state = store.getState();
		this.name = state.name;
		this.age = state.age;		
	}

    increment() {
        this.count += 1
    }
    decrement() {
        this.count -= 1
	}
	
	dispatchIncrement() {
		store.dispatch({type: "INCREMENT_AGE", redraw: true} );
		const state = store.getState();
		this.name = state.name;
		this.age = state.age;
	}

    view(vnode) {
        return m("div", [
            m("p", "Count: " + this.count),
            m("button", { onclick: () => {this.increment()} }, "Increment"),
			m("button", { onclick: () => {this.decrement()} }, "Decrement"),
			m("p", this.name),
			m("p", m(Greeter)),
			m("button", { onclick: () => {this.dispatchIncrement()} }, "Dispatch increment")]
        )
    }
}
