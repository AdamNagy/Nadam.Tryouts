import m from "mithril";
import { store } from '../store/store';

class Greeter {
    view(vnode) {
        return m("div", vnode.attrs, ["Hello ", vnode.children])
    }
}

let greeter = new Greeter();

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
		return (
			<jsx-child>
				<h3>Hello from child component: Child component</h3>
				<p>Name: {this.name}</p>
				<input type="text" id="qwe"></input>
				<button onclick={this.increment}>Increment age</button>
				<greeter></greeter>
			</jsx-child>
		)
    }
}
