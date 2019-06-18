// type: Domain logic
// file name: nadam.dom-filter.js
// class name: DomFilter
// namespace: Nadam

/// <predicateSelector> is a dom selector, the returned elements will be passed to the predicate
/// <elementSelector> is a dom selector, the actual elements that the filter (hide or show) will apply
export class DomFilter {

	PredicateSelector: string;
	ElementSelector: string;
	View: HTMLElement;
	Template = `
		<div id=nadam-domfilter>
			<input type="text" data-local-id="filter-predicate-value" size="30" list="babes" class="ml-1 mr-1">
			<button data-local-id="apply-filter-btn" class="btn btn-primary ml-1 mr-1">Filter</button>
			<button data-local-id="reset-filter-btn" class="btn btn-danger ml-1 mr-1">Reset</button>
		</div>`;

	constructor (_predicateSelector: string, _elementSelector: string) {

		this.PredicateSelector = _predicateSelector;
		this.ElementSelector = _elementSelector || _predicateSelector;
		let domParser: DOMParser = new DOMParser();

		this.View = domParser.parseFromString(this.Template, "text/html")
			.querySelector("div#nadam-domfilter");

		this.View.querySelector("button[data-local-id='apply-filter-btn']")
			.addEventListener("click", (() => {
				return () => { this.Filter(); };
			})());

		this.View.querySelector("button[data-local-id='reset-filter-btn']")
			.addEventListener("click", (() => {
				return () => { this.ResetFilter(); };
			})());
	}

	FilterValue(): string {
		return (this.View.querySelector("input[data-local-id='filter-predicate-value']") as HTMLInputElement).value;
	}

    FilterPredicate(element: HTMLElement): boolean {
		var filterPredicateValue: string = this.FilterValue();
		if (element) {
			filterPredicateValue = filterPredicateValue.replace(" ", "").toLowerCase();
			let actual: string = element.innerText.replace(" ", "").toLowerCase();
			if (filterPredicateValue.lastIndexOf("*") > 0) {
				return actual.startsWith(filterPredicateValue.substr(0, filterPredicateValue.length - 1));
			} else {
				return actual === filterPredicateValue;
			}
		}
		return false;
	}

	Filter(): void {
		var predicateElements: NodeListOf<any> = document.querySelectorAll(this.PredicateSelector);
		var elements: NodeListOf<any> = document.querySelectorAll(this.ElementSelector);

		for(let i: number = 0; i < elements.length; ++i) {
			if( this.FilterPredicate(predicateElements[i]) ) {
				continue;
			}

			elements[i].style.display = "none";
		}
	}

	ResetFilter(): void {
		(this.View.querySelector("input[data-local-id='filter-predicate-value']") as HTMLInputElement).value = "";
		var elements: NodeListOf<any> = document.querySelectorAll(this.ElementSelector);
		for(let i: number = 0; i < elements.length; ++i) {
			elements[i].style.display = "block";
		}
	}
}