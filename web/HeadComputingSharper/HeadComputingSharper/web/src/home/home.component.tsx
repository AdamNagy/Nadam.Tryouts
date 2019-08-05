import { connect } from "mithril-redux";
import { nextGame, setLevel, setGameType }  from "./home.actions";
import './home.style.scss';

class HomeComponent_factory {
	public static eventActionMapping = {
		// its because this 'mithril-redux' shit wires up all events in the markeup as a store action,
		// and need to use the thunk middleware
		dispatchNewAge: () => () => {
			(window as any).store.dispatch({type: "INCREMENT_AGE"} );
		},

		decrement: () => () => {
			(window as any).store.dispatch({type: DECREMENT_AGE} ); 
		},
	};

	public static selector(state) {return {age: state.age}};

	public static getComponent() {
		return {
			view: (ctrl, stateProjection, children) => { 
				return (
					<m-home class="container">
						<h3>Hello from child component: Child component</h3>
						<p>html attributes: {stateProjection["some-attribute"]}</p>
						<input type="text" id="qwe"></input>
						<button onclick={ctrl.dispatchNewAge()}>Increment age</button>
						<button onclick={ctrl.decrement()}>Decrement age</button>
					</m-home>
				)
			}
		}
	}
}

export const HomeComponent = connect(
	HomeComponent_factory.selector,
	HomeComponent_factory.eventActionMapping
)(HomeComponent_factory.getComponent());