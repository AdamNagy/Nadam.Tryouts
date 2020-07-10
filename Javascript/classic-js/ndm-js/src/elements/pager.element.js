class PagerElement extends HTMLElement {
	
	config = {};	

	constructor(_config) {
		super();

		this.config = _config || {};
	}

	setActive(activePage) {		

		activePage = parseInt(activePage);
		var pagerRow = this.$nid("buttons-row").WithoutChildren();

		var startPage = activePage > 2 ? activePage - 2: 1
		var endPage = startPage + 5 > this.config.pages ? this.config.pages : startPage + 5

		// prev button
		if( activePage > 1 ) {
			var prevButton = document.createElement("button").WithClasses(["btn", "btn-sm", "mx-1"]).WithInnerText("Előző")
				.WithOnClick(
					((pageIdx) => (event) => {
						this.dispatchEvent(new CustomEvent('paging', { bubbles: true, detail: { page: pageIdx } }))
					})(activePage - 1)
				).WithClass("btn-success");

			pagerRow.WithChild(prevButton);
		}

		// numbered buttons
		for(var i = startPage ; i < endPage; ++i) {
			var button = document.createElement("button").WithClasses(["btn", "btn-sm", "mx-1"]).WithInnerText(i)
				.WithOnClick(
					((pageIdx) => (event) => {
						this.dispatchEvent(new CustomEvent('paging', { bubbles: true, detail: { page: pageIdx } }))
					})(i)
				);

			if( i === activePage )
				button.WithClass("btn-success");
			else 
				button.WithClass("btn-primary");

			pagerRow.WithChild(button);
		}

		// next button
		if( activePage < this.config.pages ) {
			var nextButton = document.createElement("button").WithClasses(["btn", "btn-sm", "mx-1"]).WithInnerText("Következő")
				.WithOnClick(
					((pageIdx) => (event) => {
						this.dispatchEvent(new CustomEvent('paging', { bubbles: true, detail: { page: pageIdx } }))
					})(activePage + 1)
				).WithClass("btn-success");

			pagerRow.WithChild(nextButton);
		}
	}

	init(pages, activePage) {
		this.config.pages = parseInt(pages);
		
		this.append(
			document.createElement("div")
				.WithClasses(["row justify-content-center sticky-top py-2 bg-dark"])
				.WithAttribute("nid", "buttons-row"));

		this.setActive(activePage);
	}	

	// runs each time the element is added to the DOM
	connectedCallback() {
		
	}
}

customElements.define('ndm-pager', PagerElement);
