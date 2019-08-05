import { connect } from "mithril-redux";
import { nextGame, setLevel, setGameType, NEXT_GAME }  from "./home.actions";
import './home.style.scss';

class OptionsComponent_factory {
	public static eventActionMapping = {
		
	};

	public static selector(state) { return undefined; };

	public static getComponent() {
		return {
			view: (ctrl, stateProjection, children) => { 
				return (
					<div>
						<select>
							<option value="volvo">Volvo</option>
							<option value="saab">Saab</option>
							<option value="mercedes">Mercedes</option>
							<option value="audi">Audi</option>
						</select>
					</div>
				)
			}
		}
	}
}

export const OptionsComponent = connect(
	OptionsComponent_factory.selector,
	OptionsComponent_factory.eventActionMapping
)(OptionsComponent_factory.getComponent());