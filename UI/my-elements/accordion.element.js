
/*
<div class="card">
	<div class="card-header" id="headingOne">
		<h5 class="mb-0">
			<button class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
			</button>
		</h5>
	</div>

	<div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
		<div class="card-body">			
		</div>
	</div>
</div>
*/

class AccordionElement extends HTMLElement {
	
	config = {};
	
	template = `
		<div class="card">
			<div class="card-header" >
				<h5 class="mb-0" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
						
				</h5>
			</div>

			<div id="collapseOne" class="collapse" data-parent="#accordion">
				<div class="card-body">
				</div>
			</div>
		</div>
		`;	

	constructor(_config) {
		super();
		this.config = _config;
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		var accordionItems = this.querySelectorAll(".accordion-item");

		var domParser = new DOMParser();
		var accordionItemIdx = 1;
		// using predefined html tags
		for(var accordionItem of accordionItems) {
			var accordionItemElement = domParser.parseFromString(this.template, "text/html")
											.querySelector("div:first-child");
			accordionItemElement.querySelector("h5").innerText = accordionItem.getAttribute("title");
			
			while( accordionItem.children.length > 0 ) {

				accordionItemElement.querySelector(".card-body").append(accordionItem.children[0]);
			}

			accordionItemElement.querySelector(".card-header").setAttribute("id", `heading-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("data-target", `#collapse-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("aria-controls", `collapse-${accordionItemIdx}`);

			accordionItemElement.querySelector(".collapse").setAttribute("aria-labelledby", `heading-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("id", `collapse-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("data-parent",  `#${this.getAttribute("id")}`);

			// set the first one to be shown
			if( accordionItemIdx === 1 ) {
				accordionItemElement.querySelector(".card-header h5").setAttribute("aria-expanded", "true");
				accordionItemElement.querySelector(".collapse").classList.add("show");
			} else {
				accordionItemElement.querySelector("h5").classList.add("collapsed");
			}

			accordionItem.remove();
			this.appendChild(accordionItemElement);

			++accordionItemIdx;
		}

		// using config
		for(var accordionItem of this.config) {
			var accordionItemElement = domParser.parseFromString(this.template, "text/html")
				.querySelector("div:first-child");

			accordionItemElement.querySelector("h5").innerText = accordionItem.title;			
			accordionItemElement.querySelector(".card-body").append(accordionItem.contentElement);			

			accordionItemElement.querySelector(".card-header").setAttribute("id", `heading-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("data-target", `#collapse-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("aria-controls", `collapse-${accordionItemIdx}`);

			accordionItemElement.querySelector(".collapse").setAttribute("aria-labelledby", `heading-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("id", `collapse-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("data-parent",  `#${this.config.id}`);

			// set the first one to be shown
			if( accordionItemIdx === 1 ) {
				accordionItemElement.querySelector(".card-header h5").setAttribute("aria-expanded", "true");
				accordionItemElement.querySelector(".collapse").classList.add("show");
			} else {
				accordionItemElement.querySelector("h5").classList.add("collapsed");
			}

			this.appendChild(accordionItemElement);
			++accordionItemIdx;
		}

		
		if( this.getAttribute("id") === undefined || this.getAttribute("id") === null  )
			this.setAttribute("id", this.config.id);
	}
}

customElements.define('ndm-accordion', AccordionElement);
