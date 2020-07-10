/*
Page type can be 
	template: template element id has to be provided and itt will be instantianted by:  (template).content.cloneNode(true)
	elementclass: means a prototype function which will ba called by reflection
*/

class Router {

	spaContainer = {};
	pageRegistry = [];

	queryString = "";
	queryStringDict = [];

	navigations = 0;
	// location inside the application in url its after the #-mark without the query string
	spaLocation = "";

	constructor() {
		this.spaContainer = document.getElementById("spa-container");
		var pagesTemplates = document.querySelectorAll("template.page");

		for(var pageTemplateElement of pagesTemplates) {
			this.pageRegistry.push({
				type: "template",
				ctor: null,
				route: pageTemplateElement.getAttribute("route"),
				componentInstance: undefined,
				defaultQueryString: pageTemplateElement.getAttribute("default-querystring") || "",
				persistedQueryString: ""
			});
		}
	}

	registerRoute(route, ctor, defaultQueryString) {
		this.pageRegistry.push({
			type: "elementclass",
			ctor: ctor,
			route: route,
			componentInstance: undefined,
			defaultQueryString,
			persistedQueryString: ""
		});
	}

	detectRoute() {
		const appRoute = document.location.hash;
		if ( appRoute === "") {
			history.replaceState({ navigation: ++this.navigations  }, "home page", "#home?page=1");
			this.activateRoute("home?page=1", true);
		} else {
			const page = appRoute.slice(1, appRoute.length);
			this.activateRoute(page, true);
		}
	}

	parseQueryString(queryString) {

		this.queryStringDict = [];		

		if( queryString === "" )
			return;

		var queryParams = queryString.split('&');

		for(var queryParam of queryParams) {
			var queryParamKeyValue = queryParam.split('=');
			if( queryParamKeyValue.length === 1 ) {
				queryStringDict.push({key: queryParamKeyValue[0], value: undefined})
			} else {
				this.queryStringDict.push({key: queryParamKeyValue[0], value: queryParamKeyValue[1]})
			}
		}
	}

	setQueryParam(key, value) {
		this.queryStringDict[key] = value;
		history.pushState({ navigation: ++this.navigations }, this.spaLocation, `${window.location.origin}/${window.app.root}#${this.spaLocation}?${key}=${value}`);
	}

	stringifyQueryParams() {

	}

	getCurrentQueryString() {
		return window.location.hash.split('?')[1] || "";
	}
		
	

	activateRoute(requestedLink, initialNavigation = false) {

		const splitted = requestedLink.split("?");
		
		const route = splitted[0];

		const registeredRoute = this.pageRegistry.find(item => item.route === route);
		if( !initialNavigation ) {
			const prevoiusRoute = this.pageRegistry.find(item => item.route === this.spaLocation);
			prevoiusRoute.persistedQueryString = this.getCurrentQueryString();
		}

		if(registeredRoute) {

			this.parseQueryString(splitted[1] || registeredRoute.persistedQueryString || registeredRoute.defaultQueryString );
			this.spaLocation = route;

			if( splitted.length === 1 && (registeredRoute.persistedQueryString || registeredRoute.defaultQueryString)) {
				requestedLink += `?${registeredRoute.persistedQueryString || registeredRoute.defaultQueryString}`
			}
			
			if (!initialNavigation) {
				history.pushState({ navigation: ++this.navigations }, requestedLink, `${window.location.origin}/${window.app.root}#${requestedLink}`);
			}

			let pageRootElement = { };
			if( registeredRoute.componentInstance ) {
				pageRootElement = registeredRoute.componentInstance;
			}
			else if( registeredRoute.type === "template" ) {
				const template = document.querySelector(`[route="${registeredRoute.route}"]`);
				pageRootElement = document.createElement("section");
				pageRootElement.WithChild( (template).content.cloneNode(true));
				registeredRoute.componentInstance = pageRootElement;
			}

			window.scrollTo(0, 0);
			this.spaContainer.WithoutChildren()
				.WithChild(pageRootElement);

		} else {
			console.error("invalid route");
		}
	}
}
