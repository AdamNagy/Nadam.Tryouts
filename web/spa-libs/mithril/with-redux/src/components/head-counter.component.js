import m from "mithril";
import { addTask } from '../store/actions/head-counter.actions';



class _HeadCounterComponent {

	content = "Hello World JSX from component property";
	tasks = [];
	
	add() {
		window.store.dispatch(addTask({a: 3, b: 5, sum: 8}))
	}
	
	oncreate(vnode) {
        this.tasks = window.store.getState();
	}
	
	view(ctrl, {a, b, c}) {
		return (
			<main>
				<h1>{ this.content }</h1>
				<ol>
					{ this.tasks.map(task => <li>{task}</li>) }
				</ol>
			</main>
		)
	}			
}

export const HeadCounterComponent = connect(
	(state) => ({a: state.a, b: state.b, c: state.c}),	// seelctor function
	{ inc: addTask }	// controller object that maps events (button click) to actions
  )(_HeadCounterComponent);
  
