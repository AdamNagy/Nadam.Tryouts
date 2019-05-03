"use strict"

window.Nadam = window.Nadam || {}
Nadam.Bootstrap = Nadam.Bootstrap || {}
Nadam.Bootstrap.Pagination = function() {

	var baseTemplate = 
		`<nav aria-label="Page navigation example">
		<ul class="pagination">
		</ul>
		</nav>
		`
	
	var pageTemplate = 
	`<li class="page-item"><a class="page-link">Previous</a></li>
	`
	/* items 
	<li class="page-item"><a class="page-link" href="#">Previous</a></li>
	<li class="page-item"><a class="page-link" href="#">1</a></li>
	...
	<li class="page-item"><a class="page-link" href="#">2</a></li>
	<li class="page-item"><a class="page-link" href="#">Next</a></li>
	*/ 

	// constructor
	(function() {
		var rootElement = domParser.parseFromString(baseTemplate, "text/html")
									.querySelector("nav:first-child");
	})();
}