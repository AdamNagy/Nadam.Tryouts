class HtmlDoc extends HTMLElement {

	lazyLoad = false;
	sourceUrl = "";

	constructor() {
		super();

		let parser = new DOMParser();
		var shadow = this.attachShadow({mode: 'open'});
		var wrapper = document.createElement('div');

		// will be a bool type attribute, it will set if the doc is loaded immediately or later by a trigger
		this.lazyLoad = this.hasAttribute('lazy') || false;
		
		if(this.hasAttribute('src')) {
			this.sourceUrl = this.getAttribute('src');
		}
		
		if( this.sourceUrl !== "" && !this.lazyLoad) {
			$.get(this.sourceUrl)
				.done((response) => {
					var doc = parser.parseFromString(response, "text/html")
									.querySelector("body").childNodes;
		
					doc.forEach(element => {
						wrapper.appendChild(element);
					});
				})
				.fail((err) => {
					console.warn(err);
				});
		}

		shadow.appendChild(wrapper);		
	}
}

customElements.define('html-doc', HtmlDoc);