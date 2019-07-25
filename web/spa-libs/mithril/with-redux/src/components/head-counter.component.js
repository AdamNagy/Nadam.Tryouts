import m from "mithril";
import { addTask } from '../store/actions/head-counter.actions';

export const HeadCounterComponent = {
	add: function(e) {
		window.store.dispatch(addTask({a: 3, b: 5, sum: 8}))
	},
	content: "Hello World JSX from component property",
	tasks: [],
	// oninit: function() {
	// 	this.tasks = window.store.getState();
	// },
	oncreate: function(vnode) {
        this.tasks = window.store.getState();
    },
	// onbeforeupdate: state.computed,
	// view: () =>	 (
	// 		<div>
	// 			<div>
	// 				<button click='this.add'>Dispatch action</button>
	// 			</div>
	// 			<div>{this.tasks}</div>
	// 		</div>
	// 	)
	view: function() {
		return (
			<main>
				<h1>{this.content}</h1>
				<ol>
					{this.tasks.map(task => <li>{task}</li>)}
				</ol>
			</main>
		)
	}
			
}
