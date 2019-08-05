import { connect } from "mithril-redux";
import { nextGame, setLevel, setGameType, NEXT_GAME }  from "./home.actions";
import './home.style.scss';
import {OptionsComponent} from "../options/options.component";
class HomeComponent_factory {
	public static eventActionMapping = {
		// its because this 'mithril-redux' shit wires up all events in the markeup as a store action,
		// and need to use the thunk middleware
		dispatchNextGame: () => () => {
			let a = Math.ceil(Math.random() * 10);
			let b = Math.ceil(Math.random() * 10);

			(window as any).store.dispatch({
				type: NEXT_GAME,
				payload: {left: a, right: b, solution: a + b},
				redraw: true});
		}
	};

	public static selector(state) {
		const reducer = "gameReducer";
		const reducedState = state[reducer][state[reducer].length - 1];

		return { ...reducedState }; 
	};

	public static getComponent() {
		return {
			view: (ctrl, stateProjection, children) => { 
				return (
					<div class="container">
						<div class="row">
							<h3>Head computing sharepr</h3>
						</div>
						<div class="row">
							<button onclick={ctrl.dispatchNextGame({left: 3, right: 5, solution: 8})}>Next</button>
							<OptionsComponent></OptionsComponent>
						</div>
						<div class="row">
							<p>Level: {stateProjection.level}, Type: {stateProjection.type}</p>
							<p>{stateProjection.left} + {stateProjection.right}</p>
						</div>
					</div>
				)
			}
		}
	}
}

export const HomeComponent = connect(
	HomeComponent_factory.selector,
	HomeComponent_factory.eventActionMapping
)(HomeComponent_factory.getComponent());