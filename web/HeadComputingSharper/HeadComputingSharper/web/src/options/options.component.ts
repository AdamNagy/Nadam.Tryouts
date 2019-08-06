import m from "mithril";
import { connect } from "mithril-redux";

class OptionsComponent_factory {
	public static eventActionMapping = {
		
	};

	public static selector(state) { return undefined; };

	public static getComponent() {
		return {
			view: (ctrl, stateProjection, children) => { 
				return m("div",
					m("form", 
						m("div", {class: "form-group"}, [
							m("label", {for: "game-level-selection"}, "Game level"),
							m("select", {class: "form-control"}, ["addition", "multiply", "dates"].map(function(item) {
								return m("option", item) } )
							)
						])
					)
				);	
			}
		}
	}
}

export const OptionsComponent = connect(
	OptionsComponent_factory.selector,
	OptionsComponent_factory.eventActionMapping
)(OptionsComponent_factory.getComponent());