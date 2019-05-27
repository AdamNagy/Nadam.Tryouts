// type: Domain logic
// file name: nadam.dom-filter.js
// class name: DomFilter
// namespace: Nadam

/// <predicateSelector> is a dom selector, the returned elements will be passed to the predicate
/// <elementSelector> is a dom selector, the actual elements that the filter (hide or show) will apply
export class DomFilter {

	constructor (_predicateSelector, _elementSelector) {

		this.predicateSelector = _predicateSelector;
		this.elementSelector = _elementSelector || _predicateSelector;

		var template = `
			<div id=nadam-domfilter>
				<input type="text" data-local-id="filter-predicate-value" size="30" list="babes" class="ml-1 mr-1">
				<button data-local-id="apply-filter-btn" class="btn btn-primary ml-1 mr-1">Filter</button>
				<button data-local-id="reset-filter-btn" class="btn btn-danger ml-1 mr-1">Reset</button>			
			</div>`;
		
		var domParser = new DOMParser();
		this.view = domParser.parseFromString(template, "text/html")
			.querySelector("div#nadam-domfilter");
			
		this.view.querySelector("button[data-local-id='apply-filter-btn']")
			.addEventListener("click", (() => {
				return () => { this.Filter(this.FilterValue()); };
			})());

		this.view.querySelector("button[data-local-id='reset-filter-btn']")
			.addEventListener("click", (() => {
				return () => { this.ResetFilter(); };
			})());
	}
	
	FilterValue() {
		return this.view.querySelector("input[data-local-id='filter-predicate-value']").value;
	}

    FilterPredicate(element) {
		var filterPredicateValue = this.FilterValue();
		if (element) {
			filterPredicateValue = filterPredicateValue.replace(' ', '').toLowerCase();
			let actual = element.innerText.replace(' ', '').toLowerCase();
			if (filterPredicateValue.lastIndexOf('*') > 0)
				return actual.startsWith(filterPredicateValue.substr(0, filterPredicateValue.length - 1));
			else
				return actual === filterPredicateValue;
		}
		return false;
	}

	Filter() {
		var predicateElements = document.querySelectorAll(this.predicateSelector);
		var elements = document.querySelectorAll(this.elementSelector);

		for(var i = 0; i < elements.length; ++i) {
			if( this.FilterPredicate(predicateElements[i]) )
				continue;
			
			elements[i].style.display = "none";
		}
	};

	ResetFilter() {
		this.view.querySelector("input[data-local-id='filter-predicate-value']").value = "";
		var elements = document.querySelectorAll(this.elementSelector);
		for(var i = 0; i < elements.length; ++i)	
			elements[i].style.display = "block";			
	};
}