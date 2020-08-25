/*
Page type can be 
	template: template element id has to be provided and itt will be instantianted by:  (template).content.cloneNode(true)
	elementclass: means a prototype function which will ba called by reflection
*/

declare class Router {

	registerRoute(route: string, ctor: any, defaultQueryString: any): void;

	detectRoute(): void;

	parseQueryString(queryString: string): void;

	setQueryParam(key: string, value: string): void;

	stringifyQueryParams(queryParams: Array<any>): string;

	getCurrentQueryString(): string;

	activateRoute(requestedLink: string, initialNavigation: boolean): void;
}
