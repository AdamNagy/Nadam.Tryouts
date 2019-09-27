"use strict";
window.nadam = window.nadam || {};

function StaticSpa() {

	var parser = new DOMParser();
	var content = document.getElementById("content");
	var pageContainers = new Array();
	var navigations = 0;
	// var routes = ["home", "gallery", "footer"];

	function Navigate(destination, isFirstNavigation) {
		var pageContainer = GetPageContainer(destination);
		if( pageContainer === undefined ) {
			pageContainer = CreatePageContainer(destination);
	
			$.get("/" + destination + ".page.html", function( response ) {
				var doc = parser.parseFromString(response, "text/html")
								.querySelector("body").firstChild;
	
				pageContainer.appendChild(doc);
			}).fail(function() {
				console.warn(  );
			});
		} 
	
		for(var i = 0; i < pageContainers.length; ++i) {
			$( pageContainers[i]).hide();
		}
	
		$(pageContainer).show();		
		if( isFirstNavigation == undefined || !isFirstNavigation )
			history.pushState({ navigation: ++navigations }, destination, "#" + destination);		
	}
	
	function GetPageContainer(name) {
		for(var i = 0; i < pageContainers.length; ++i) {
			if( pageContainers[i].getAttribute("n-name") === name)
			return pageContainers[i];
		}
	}
	
	function CreatePageContainer(name) {
		var pageContainer = document.createElement("section");
		pageContainer.classList.add("n-page-container");
		pageContainer.setAttribute("n-name", name);
	
		content.appendChild(pageContainer);
		pageContainers.push(pageContainer);
		return pageContainer;
	}	

	var navs = document.getElementsByClassName("n-nav");
	for(var i = 0; i < navs.length; ++i) {
		$(navs[i]).click(
			function(idx) {
				var route = this.getAttribute("data-link")
				Navigate(route);
			})
	}

	var appRoute = document.location.hash;
	if( appRoute == "") {
		history.replaceState({ navigation: ++nadam.navigation  }, "home page", "#home");
		Navigate("home", true);
	} else {
		var page = appRoute.slice(1, appRoute.length);
		Navigate(page);
	}
}

window.nadam.staticSpa = new StaticSpa();