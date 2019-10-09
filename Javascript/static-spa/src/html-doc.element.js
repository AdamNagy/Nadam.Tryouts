class HtmlDoc extends HTMLElement {

	// lazyLoad = false;
	// src = "";
	loaded = false;
	parser = new DOMParser();
	wrapper = document.createElement('div');

	get src() {
	  return this.getAttribute('src');
	}

	set src(newValue) {
	  this.setAttribute('src', newValue);
	}
	
	get lazy() {
	  return this.hasAttribute('lazy');
	}

	set lazy(s) {
	  this.setAttribute('lazy', true);
	}

	constructor() {
		super();

		var shadow = this.attachShadow({mode: 'open'});

		// will be a bool type attribute, it will set if the doc is loaded immediately or later by a trigger
		// this.lazy = this.hasAttribute('lazy') || false;
		
		if(this.hasAttribute('src')) {
			this.src = this.getAttribute('src');
		}
		
		if( this.src !== null && this.src !== "" && !this.lazyLoad) {
			this.Load();
		}

		shadow.appendChild(this.wrapper);		
	}

	Load() {
		$.get(this.src)
			.done((response) => {
				var doc = this.parser.parseFromString(response, "text/html")
								.querySelector("body").childNodes;

				doc.forEach(element => {
					this.wrapper.appendChild(element);
				});

				this.loaded = true;
			})
			.fail((err) => {
				console.warn(err);
			});
	}
}

customElements.define('html-doc', HtmlDoc);