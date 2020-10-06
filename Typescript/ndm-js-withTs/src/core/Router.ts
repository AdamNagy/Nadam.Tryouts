/*
Page type can be
	template: template element id has to be provided and itt will be instantianted by:  (template).content.cloneNode(true)
	elementclass: means a prototype function which will ba called by reflection
*/

class TsRouter {

	public spaContainer: HTMLElement;
	public pageRegistry: any[] = [];

	public queryString = "";
	public queryStringDict: any[] = [];

	public navigations = 0;
	// location inside the application in url its after the #-mark without the query string
	public spaLocation = "";

	constructor() {
		this.spaContainer = document.getElementById("spa-container")!;
		const pagesTemplates = document.querySelectorAll("template.page");

		for (const pageTemplateElement of pagesTemplates) {
			this.pageRegistry.push({
				type: "template",
				ctor: null,
				route: pageTemplateElement.getAttribute("route"),
				componentInstance: undefined,
				defaultQueryString: pageTemplateElement.getAttribute("default-querystring") || "",
				persistedQueryString: "",
			});
		}
	}

	public registerRoute(route: string, ctor: any, defaultQueryString: string) {
		this.pageRegistry.push({
			type: "elementclass",
			ctor,
			route,
			componentInstance: undefined,
			defaultQueryString,
			persistedQueryString: "",
		});
	}

	public detectRoute() {
		const appRoute = document.location.hash;
		if ( appRoute === "") {
			history.replaceState({ navigation: ++this.navigations  }, "home page", "#home?page=1");
			this.activateRoute("home?page=1", true);
		} else {
			const page = appRoute.slice(1, appRoute.length);
			this.activateRoute(page, true);
		}
	}

	public parseQueryString(queryString: string) {

		this.queryStringDict = [];

		if ( queryString === "" )
			return;

		const queryParams = queryString.split('&');

		for (const queryParam of queryParams) {
			const queryParamKeyValue = queryParam.split('=');
			if ( queryParamKeyValue.length === 1 ) {
				this.queryStringDict.push({key: queryParamKeyValue[0], value: undefined});
			} else {
				this.queryStringDict.push({key: queryParamKeyValue[0], value: queryParamKeyValue[1]});
			}
		}
	}

	public setQueryParam(key: string, value: string) {
		const queryParam = this.queryStringDict.find((item) => item.key === key);

		if ( !queryParam ) {
			this.queryStringDict.push({key, value});
		} else {
			queryParam.value = value;
		}

		history.pushState(
			{ navigation: ++this.navigations },
			this.spaLocation,
			`${window.location.origin}/${(window as any).app.root}#${this.spaLocation}${this.stringifyQueryParams(this.queryStringDict)}`);
	}

	public stringifyQueryParams(queryParams: any[]) {
		let queryString = "?";
		for (const queryKey of queryParams) {
			queryString += `${queryKey.key}=${queryKey.value}&`;
		}

		return queryString.slice(0, -1);
	}

	public getCurrentQueryString() {
		return window.location.hash.split('?')[1] || "";
	}

	public activateRoute(requestedLink: string, initialNavigation = false) {

		const splitted = requestedLink.split("?");
		const route = splitted[0];

		const registeredRoute = this.pageRegistry.find(item => item.route === route);
		if ( !initialNavigation ) {
			const prevoiusRoute = this.pageRegistry.find(item => item.route === this.spaLocation);
			prevoiusRoute.persistedQueryString = this.getCurrentQueryString();
		}

		if (registeredRoute) {

			this.parseQueryString(splitted[1] || registeredRoute.persistedQueryString || registeredRoute.defaultQueryString );
			this.spaLocation = route;

			if ( splitted.length === 1 && (registeredRoute.persistedQueryString || registeredRoute.defaultQueryString)) {
				requestedLink += `?${registeredRoute.persistedQueryString || registeredRoute.defaultQueryString}`;
			}

			if (!initialNavigation) {
				history.pushState({ navigation: ++this.navigations }, requestedLink, `${window.location.origin}/${(window as any).app.root}#${requestedLink}`);
			}

			let pageRootElement: HTMLElement;
			if ( registeredRoute.componentInstance ) {
				pageRootElement = registeredRoute.componentInstance;
			} else if ( registeredRoute.type === "template" ) {

				const template = document.querySelector(`[route="${registeredRoute.route}"]`)!;
				pageRootElement = document.createElement("section");
				pageRootElement.WithChild( (template as any).content.cloneNode(true));
				registeredRoute.componentInstance = pageRootElement;
			} else if ( registeredRoute.type === "elementclass" ) {

				pageRootElement = Reflect.construct(registeredRoute.ctor, []);
				registeredRoute.componentInstance = pageRootElement;
			}

			window.scrollTo(0, 0);
			this.spaContainer.WithoutChildren()
				.WithChild(pageRootElement!);

		} else {
			console.error("invalid route");
		}
	}
}
