import m from "mithril";

export const MyJSXComponent = {
	content: "Hello World JSX from component property",
	view: function() {
		return (
			<main>
				<h1>{this.content}</h1>
			</main>
		)
	}
}