import m from "mithril";

export class MyComponent {

	constructor(parent) {

		this.model = {
			title: "My title",
			subTitle: "This is a sub title",
			todos: ["Todo1", "Todo2", "Todo3"]
		},

		this.container = parent;
	}

	Render() {

		m.render(this.container, this.model.title);
	};
}