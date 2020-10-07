Array.prototype.First = function(predicate) {

	if (predicate == null || typeof predicate !== 'function') {
		throw "Predicate is not a function!";
	}

	var found;

	for (var i = 0; i < this.length; ++i) {
		if (predicate(this[i])) {
			found = this[i];
		}
	}

	return found;
}

Array.prototype.Last = function(predicate) {

	if (predicate !== null && predicate !== undefined) {
		var filtered = this.filter(predicate);
		return filtered[filtered.length - 1];
	}

	return this[this.length - 1];
}

Array.prototype.Skip = function(amount) {

	return this.splice(amount, this.length-1);
}

Array.prototype.Take = function(amount) {

	return this.splice(0, amount);
}

Array.prototype.Where = function(predicate) {

	return this.filter(predicate);
}

Array.prototype.Select = function(action) {

	var list = new Array();
	if (action == null || typeof action !== 'function') {
		throw "Action is not a function!";
	}

	for (var i = 0; i < this.length; ++i) {
		list.push(action(this[i]));
	}

	return list;
}

/*
usage:
ndm("default:div.container") returns <div class="container"></div>
	. -> class
	# -> id
	* -> nid
	(space) -> child
	[attribute name = attrbiute value] -> attribute
	{key} -> adds second patameters (config) 'key' named property as object to ctor as parameter
ndm(custom:parallax#my-parallax.some-class, {...}) returns <ndm-parallax class="some-class" id="my-parallax">...</ndm-parallax>
ndm("<div><p></p><img><img><section></section></div>...") returns <div><p></p><img><img><section></section></div>
ndm("template:some-template-id") returns instantiated html document
*/

function ndm(param, config) {

}
HTMLElement.prototype.Value = function() {
	if( this.value !== undefined )
		return this.value;
	else
		return this.innerText;
}

HTMLElement.prototype.Height = function() {
	return this.height;
}

HTMLElement.prototype.Width = function() {
	return this.width;
}
// attribute
HTMLElement.prototype.WithAttribute = function(key, value) {
	this.setAttribute(key, value);
	return this;
}

HTMLElement.prototype.WithoutAttribute = function(key, value) {
	this.removeAttribute(key);
	return this;
}

// style
HTMLElement.prototype.WithStyle = function(key, value) {
	this.style[key] = value;
	return this;
}

HTMLElement.prototype.WithCss = function(cssObj) {
	for(var propName in cssObj) {
		this.style[propName] = cssObj[propName];
	}
	return this;
}

// class
HTMLElement.prototype.WithClass = function(name) {
	this.classList.add(name);
	return this;
}


HTMLElement.prototype.WithClasses = function(classNames) {

	classNames.forEach(
		item => item.split(" ").forEach(
			className => this.classList.add(className)
		)
	);
	
	return this;
}

HTMLElement.prototype.WithoutClass = function(className) {

	if(this.classList.contains(className))
		this.classList.remove(className);
	
	return this;
}

// inner text/value
HTMLElement.prototype.WithInnerText = function(text) {
	this.innerText = text;
	return this;
}

HTMLInputElement.prototype.WithValue = function(val) {
	if( this.value !== undefined )
		this.value = val;
	else
		this.innerText = val;

	return this;
}

// id
HTMLElement.prototype.WithId = function(id) {
	this.setAttribute("id", id);
	return this;
}

// child element
HTMLElement.prototype.WithChild = function(child){
	this.append(child);
	return this;
}

HTMLElement.prototype.WithChildren = function(children) {
	for(var i = 0; i < children.length; ++i) {
		this.append(children[i]);
	}
	return this;
}

HTMLElement.prototype.WithoutChildren = function() {

	var children = this.children;
	while (children !== undefined && children.length > 0) {
		for (var i = 0; i < children.length; ++i) {
			this.children[i].remove();
		}
		children = this.children;
	}

	return this;
}

// event listener
HTMLElement.prototype.WithEventListener = function(event, func) {
	this.addEventListener(event, func);
	return this;
}

HTMLElement.prototype.WithoutEventListener = function(event) {
	this.removeEventListener(event);
	return this;
}

HTMLElement.prototype.WithOnClick = function(func) {
	this.addEventListener("click", func);
	return this;
}

HTMLElement.prototype.WithOnLoad = function(func) {
	this.addEventListener("load", func);
	return this;
}

// other
HTMLElement.prototype.ToParent = function(parent) {
	parent.append(this);
	return this;
}
HTMLElement.prototype.$nid = function(nid) {
	if (nid === "")
		return this;
	return this.querySelector('[nid="' + nid + '"]');
}

HTMLElement.prototype.$class = function(selector) {
	if (selector === "")
		return this;
	return this.querySelectorAll('[class*="' + selector + '"]');
}

HTMLElement.prototype.$ = function(selector) {
	if (selector === "")
		return this;
	return this.querySelectorAll(selector);
}


/// <summary> 
/// Iterates throught the direct childrend of the node, and flattens it into a list
/// Optionally filters the list
/// </summary>
Element.prototype.AllChildren = function(predicate) {

	var firstChild = this.firstElementChild;
	var directchildren = new Array();

	if (firstChild !== null) {
		directchildren.push(firstChild);
	} else {
		return;
	}

	var sibling = firstChild.nextElementSibling;
	while (sibling != null) {
		directchildren.push(sibling);
		sibling = sibling.nextElementSibling;
	}

	if(predicate !== null && predicate !== undefined ) {
		return directchildren.filter(predicate);
	}

	return directchildren;
}

HTMLElement.prototype.ToParent = function(parent) {
	parent.append(this);
	return this;
}

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
		var queryParam = this.queryStringDict.find((item) => item.key === key);

		if( !queryParam ) {
			this.queryStringDict.push({key: key, value: value})
		} else {
			queryParam.value = value;
		}

		history.pushState(
			{ navigation: ++this.navigations },
			this.spaLocation,
			`${window.location.origin}/${window.app.root}#${this.spaLocation}${this.stringifyQueryParams(this.queryStringDict)}`);
	}

	stringifyQueryParams(queryParams) {
		var queryString = "?";
		for(var queryKey of queryParams) {
			queryString += `${queryKey.key}=${queryKey.value}&`
		}

		return queryString.slice(0, -1);
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
			else if( registeredRoute.type === "elementclass" ) {

				pageRootElement = Reflect.construct(registeredRoute.ctor, []);
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

// in this context array is not an object
var IsObject = function(variable) {
	return( typeof variable === "object" && variable.length === undefined )
}

// convert an object to an array using the values only
var ObjectToArray = function(obj) {
	var arr = [];
	for(var propname in obj) { 
		arr.push(obj[propname]); 
	}

	return arr;
}

var CamelCaseToDashed = function(word) {
	var dashCased = word[0].toLowerCase();

	for(var i = 1; i < word.length; ++i) {
		if( word[i] === word[i].toUpperCase() && word[i] !== "-")
			dashCased += "-";
		 
		dashCased += word[i].toLowerCase();
	}
	
	// trim '-' from start
	while( dashCased[0] === '-' ) {
		dashCased = dashCased.slice(1);
	}

	// trim '-' from end
	while( dashCased[dashCased.length - 1] === '-' ) {
		dashCased = dashCased.slice(0, dashCased.length - 1);
	}

	// replaces all multiple dasheses next to each other with one dash only
	return dashCased.replace(new RegExp('[-]+', 'g'), '-');
}

var IsHTMLElementProperty = function(word) {
	// need to list all HTMLElement property
	if( word === "innerText" )
		return true;

	return false;
}

var Throttle = function(func, limit) {
	var lastFunc;
	var lastRan;

	return function() {
		var context = this;
		var args = arguments;

		if ( !lastRan ) {
			func.apply(context, args);
			lastRan = Date.now();
		} else {
			clearTimeout(lastFunc);
			lastFunc = setTimeout(function() {
				if ((Date.now() - lastRan) >= limit) {
					func.apply(context, args)
					lastRan = Date.now()
				}
			}, limit - (Date.now() - lastRan))
		}
	}
}

var IsElement = function(element) {
    return element instanceof Element || element instanceof HTMLDocument;  
}

// this will return a function which able to update the 'value' with given parameter
// or just simply returns it
// val: the initial value of the property (string, number, object, array)
// boundElement: and HTMLElement which will be updated when value changes
// attrName: this is the name of the attribute of the previous HTMLElement that will hold the value as string
//   default is 'innerText'
var Property = function(val, boundElement, attrName) {
	var _val = val;

	if( boundElement !== undefined ) {
		var update = Binding(boundElement, attrName);
		update(_val);
	}

	// the new value if the property if given, so acts as a setter
	// if undefined the fuction acts as a getter
	return function (val, operation) {
		
		if( val === undefined ) {
			if( typeof _val === "function" )
			return _val();
			return _val;
		}
		
		operation = operation === undefined ? "assignment" : operation;
		switch(operation) {
			case "assignment": _val = val;
				break;
			case "add": _val.push(val);
				break;
			case "remove": 
				var idx = _val.indexOf(val);
				if( idx > -1 ) _val.splice(idx, 1);
				break;
		}

		
		if( update !== undefined ) {
			if( typeof _val === "function" ) {
				update(_val());
				return _val();
			}
			else {
				update(_val);
				return _val; 
			}
		}
	}			
}

// this returns a function as well, calling it will update the DOM wit the given values
// element: the HTMLElement to bound value to
// attrName: the attribute name to put the value into
var Binding = function(element, attrName) {

	var _element = element;
	var _attrName = attrName === undefined ? "innerText" : attrName;	// default attribute is innerText

	// the value to update the DOM HTMLElement with
	return function (newValue) {
		
		// <_attrname> is string like 'innerTEXT' or 'id' or 'propsToAttrs' which is special
		if ( typeof _attrName === "string") {

			// special case <newValue> is an object and its properties needs to be mapped to <_element> attributes
			if ( _attrName === "propsToAttrs" && IsObject(newValue) ) {
			
				for(var propName in newValue) {
					_element.setAttribute(CamelCaseToDashed(propName), newValue[propName]);
				}

			// <_attrMame> in this case is a HTMLElement attribute name like 'innerText' or 'id', 'class' etc..
			// <_element> is array multiple HTMLElement need to be updated
			} else if ( IsArray(_element) &&  IsObject(newValue)) {

				var valAsArray = ObjectToArray(newValue);
				for(var i = 0; i < _element.length; ++i) {
					_element[i][_attrName] = valAsArray[i];
				}

			// basic case:
			// <_attrMame> is a attribute name
			// <_element> is a single HTMLElement
			// <newValue> is string
			} else {
				// there is difference between property and attribute.
				// _element[_attrName] = newValue; will work only if _attrName is already exist on HTMLElement
				if ( IsHTMLElementProperty(_attrName) )
					_element[_attrName] = newValue;
				else
					_element.setAttribute(CamelCaseToDashed(_attrName), newValue);
			}
		}
		// <_attrName> is a function that generates HTMLElement (children) and will be append to <_element> (parent)
		else if(typeof _attrName === "function") {

			// purge the parent element inside to make sure no duplicity takes place
			_element.innerHTML = "";

			// 2 cases here:
			// A: <newValue> is any array of strings
			if ( IsArray(newValue) ) {
				for(var i = 0; i < newValue.length; ++i) {
					_element.append(
						_attrName(newValue[i])
					)
				}
			// B: <newValue> is string
			} else {
				_element.append(_attrName(newValue));
			}
		}
	}
}

// this could go to html-element.extension but logically belongs to this file
HTMLElement.prototype.BindValue = function(event, action) {
	this.addEventListener(event, Throttle(action, 400));
	return this;
}


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

		if( accordionItems.length > 0 )
			this.initByHtml(accordionItems);
		else if ( this.config !== undefined )
			this.initByConfig(this.config)			
	}

	initByHtml(accordionItems) {

		var domParser = new DOMParser();
		var accordionItemIdx = 1;

		var parentId = this.getAttribute("id");

		for(var accordionItem of accordionItems) {
			var accordionItemElement = domParser.parseFromString(this.template, "text/html")
											.querySelector("div:first-child");
			accordionItemElement.querySelector("h5").innerText = accordionItem.getAttribute("title");
			
			while( accordionItem.children.length > 0 ) {

				accordionItemElement.querySelector(".card-body").append(accordionItem.children[0]);
			}

			accordionItemElement.querySelector(".card-header").setAttribute("id", `heading-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("data-target", `#collapse-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("aria-controls", `collapse-${parentId}-${accordionItemIdx}`);

			accordionItemElement.querySelector(".collapse").setAttribute("aria-labelledby", `heading-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("id", `collapse-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("data-parent",  `#${parentId}`);

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
	}

	initByConfig(config) {
		
		var domParser = new DOMParser();
		var accordionItemIdx = 1;
		var parentId = config.id;

		for(var accordionItem of config.items) {
			var accordionItemElement = domParser.parseFromString(this.template, "text/html")
											.querySelector("div:first-child");

			accordionItemElement.querySelector("h5").innerText = accordionItem.title;			
			accordionItemElement.querySelector(".card-body").append(accordionItem.contentElement);

			accordionItemElement.querySelector(".card-header").setAttribute("id", `heading-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("data-target", `#collapse-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".card-header h5").setAttribute("aria-controls", `collapse-${parentId}-${accordionItemIdx}`);

			accordionItemElement.querySelector(".collapse").setAttribute("aria-labelledby", `heading-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("id", `collapse-${parentId}-${accordionItemIdx}`);
			accordionItemElement.querySelector(".collapse").setAttribute("data-parent",  `#${parentId}`);

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

		this.setAttribute("id", config.id);
	}
}

customElements.define('ndm-accordion', AccordionElement);

// Single slides carousel config
var SingleSlideCarouselConfig = {
	dots : true,
	arrows : false,
	infinite : true,
	speed : 300,
	slidesToShow : 1, 
	slidesToScroll : 1,
	autoplay : false,
	autoplaySpeed : 2000,
	lazyLoad : "ondemand",
	responsive : [
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
			}
		},
		{
			breakpoint: 992,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
			}
		},
		{
			breakpoint: 768,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 576,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
				dots: true
			}
		}
	]
}

// Multiple slides carousel config
var MultiSlideCarouselConfig = {
	dots : true,
	arrows : false,
	infinite : true,
	speed : 300,
	slidesToShow : 4, 
	slidesToScroll : 3,
	autoplay : false,
	autoplaySpeed : 3000,
	variableWidth : true,
	lazyLoad : "ondemand",
	responsive : [
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 4,
				slidesToScroll: 3,
			}
		},
		{
			breakpoint: 992,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 2,
			}
		},
		{
			breakpoint: 768,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 2,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 576,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
				dots: true
			}
		}
	]
}

class CarouselElement extends HTMLElement {
	
	config = {};
	bodyElement = {};
	slickConfig = {};

	constructor(_config) {
		super();
  
		this.classList.add("position-relative");
		this.classList.add("d-block");

		this.bodyElement = document.createElement("div");

		this.config = _config || {};
		this.config.height = this.getAttribute("height") || _config.height;
		this.config.configVariableName = this.getAttribute("configVariableName") || _config.configVariableName;
		this.config.imageSources = _config && _config.imageSources;

		var imageElements;
		 if( this.querySelector("img") ) {
			imageElements = this.querySelectorAll("img");
		} else if( this.querySelector("ndm-img-wall") ) {
			imageElements = this.querySelectorAll("ndm-img-wall");
		} else if( this.config.imageSources && this.config.imageSources.length && this.config.imageSources.length > 0 ) {
			imageElements = [];
			for(var imgSource of this.config.imageSources ) {
				var imgElement = new Image();
				imgElement.src = imgSource;
				imageElements.push(imgElement);
			}
		}

		for(var imgElement of imageElements ) {
			this.bodyElement.append(imgElement);
		}

		var frontLayer = this.querySelector(".front-layer");
		if(frontLayer) {
			frontLayer.style.zIndex = '1';
		}

		this.slickConfig = window[this.config.configVariableName];
		if( !this.slickConfig )
			this.slickConfig = SingleSlideCarouselConfig;

		if( this.hasAttribute("customProperties") ) {
			var customProperties = JSON.parse(this.getAttribute("customProperties"));
			this.slickConfig = Object.assign({}, this.slickConfig, customProperties);
		}

		this.append(this.bodyElement);
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		$(this.bodyElement).slick(this.slickConfig);
	}
}

customElements.define('ndm-carousel', CarouselElement);

class ColumnLaneElement extends HTMLElement {

	optionsMenuTemplate = `
		<nav class="navbar navbar-expand-lg navbar-dark sticky-top bg-dark" id="mainNav">
			<div class="container">
				
				<button class="navbar-toggler navbar-toggler-right"
					type="button"
					data-toggle="collapse"
					data-target="#navbarResponsive"
					aria-controls="navbarResponsive"
					aria-expanded="false"
					aria-label="Toggle Navigation">
						<i class="fa fa-bars"></i>
				</button>

				<div class="collapse navbar-collapse row justify-content-center" id="navbarResponsive">
					<div>
						<ul class="navbar-nav text-uppercase ml-auto">
							<li class="nav-item nav-link js-scroll-trigger text-center" nid="btn-toggle-column-margin">Oszlop margó</li>
							<li class="nav-item nav-link js-scroll-trigger text-center" nid="btn-toggle-item-margin">Kép margó</li>

							<li class="nav-item dropdown">
								<a class="nav-link dropdown-toggle"
									id="navbarDropdown"
									role="button"
									data-toggle="dropdown"
									aria-haspopup="true"
									aria-expanded="false">
										Oszlopok száma
								</a>

								<div nid="column-options-container" class="dropdown-menu" aria-labelledby="navbarDropdown">
									<a class="dropdown-item" role="button">1</a>
									<a class="dropdown-item" role="button">2</a>
									<a class="dropdown-item" role="button">3</a>
									<a class="dropdown-item" role="button">4</a>
									
									<a class="dropdown-item" role="button">5</a>
									<a class="dropdown-item" role="button">6</a>
									<a class="dropdown-item" role="button">7</a>
									<a class="dropdown-item" role="button">8</a>
								</div>
							</li>
						</ul>
					</div>
				</div>
			</div>
		</nav>
		`;

	config = {};
	items = [];
	observer;
	isFirstInit = true;

	portraitImageWidth = 0;
	landscapeImageWidth = 0;
	numOfPortraits = 0;
	numOfLandscapes = 0;

	constructor(config, items) {
		super();

		this.items = items || [];
		this.config = config || {};

		this.WithStyle("display", "block");

		var domParser = new DOMParser();
		var optionsMenu = domParser.parseFromString(this.optionsMenuTemplate, "text/html")
			.querySelector("nav");

		optionsMenu.$nid("btn-toggle-column-margin")
			.WithOnClick(() => {
				config.columnGap = config.columnGap === 10 ? 0 : 10;
				this.Init(config);
			});

		optionsMenu.$nid("btn-toggle-item-margin")
			.WithOnClick(() => {
				config.rowGap = config.rowGap === 10 ? 0 : 10;
				this.Init(config);
			});
		
		var columnNumber = 0;
		for(var columnNumberOption of optionsMenu.$nid("column-options-container").$("a.dropdown-item") ) {
			columnNumberOption.WithOnClick( ((_columnNumber) => () => this.Init(Object.assign(config, { numOfColumns: _columnNumber } )))(++columnNumber) );
		}

		this.WithChildren([
			optionsMenu,
			document.createElement("div")
				.WithAttribute("nid", "gallery-row")
				.WithClass("mx-auto")
		]);

		
		// this.Init(config, true);
	}

	Init(config, isFirstInit = false) {

		if( isFirstInit ) {
			// lazy loading observation
			this.observer = new IntersectionObserver((entries, self) => {
				entries.forEach((entry) => {
					if (!entry.isIntersecting)
						return true;
	
					this.LoadImage(entry.target);
					self.unobserve(entry.target);
					
				});
			});
		}

		this.$nid("gallery-row").WithoutChildren();
		for (let i = 0; i < config.numOfColumns; ++i) {
			this.$nid("gallery-row").WithChild(
				document.createElement("div").WithClass("column")
					.WithAttribute("nid", `column-${i}`)
					.WithStyle("padding", "0px")
					.WithStyle("max-width", `${100 / config.numOfColumns}%`),
			);
		}

		// put images into the columns
		for (let i = 0; i < this.items.length; ++i) {
			this.$nid("gallery-row").$nid(`column-${i % config.numOfColumns}`).WithChild(
				document.createElement("div") //.WithAttribute("href", this.items[i].getAttribute("data-real-src"))
				.WithChild(
					this.items[i].WithStyle("opacity", `${isFirstInit ? 0 : 1}`)
						.WithStyle("transition", "opacity .4s")
						.WithStyle("margin-bottom", `${config.rowGap}px`)
						.WithStyle("min-height", "70px")
						.WithOnLoad((event) => this.ClassifyImage(event))
						.WithEventListener("error", () => {
							this.items[i].WithStyle("display", "none")
						}),
				)
			);

			if( isFirstInit ) {
				this.observer.observe(this.items[i]);
			}
		}

		// additional styles as per the configuration
		if ( config.columnGap > 0 ) {
			let containerWidth = config.containerWidth;

			for (let i = 1; i < config.numOfColumns; ++i) {
				this.$nid("gallery-row").$nid(`column-${i}`)
					.WithStyle("margin-left", `${config.columnGap}px`);

				containerWidth += config.columnGap;
			}
			// this.WithStyle("width", `${containerWidth}px`);
		}
	}

	LoadImage(image) {
		image.WithAttribute("src", image.getAttribute("data-src"));
	}

	ClassifyImage(event) {

		const image = event.target;
		image.WithAttribute("original-width", image.Width())
			.WithAttribute("original-height", image.Height());

		// landscape
		if ( image.Width() > image.Height() ) {
			image.WithClass("landscape-image");
			++this.numOfLandscapes;
			if (this.portraitImageWidth > 0)
				image.WithStyle("width", `${this.portraitImageWidth}px`);

		// portrait
		} else if ( image.Width() < image.Height() ) {
			image.WithClass("portrait-image");
			++this.numOfPortraits;
			if (this.portraitImageWidth === 0)
				this.portraitImageWidth = parseInt(image.Width(), 0);

		} else
			image.WithClass("rectangle-image");
			if (this.portraitImageWidth > 0)
				image.WithStyle("width", `${this.portraitImageWidth}px`);

		image.WithStyle("opacity", "1");
	}

	connectedCallback() {
		if(this.isFirstInit) {
			this.Init(this.config, true);
			this.isFirstInit = false;
		}
	}
}

customElements.define("ndm-column-lane", ColumnLaneElement);

const DefaultConfig = {
	containerWidth: 1200,
	itemFirst: false,
	numOfColumns: 4,
	columnGap: 0,
	rowGap: 0,
	includeControlPanel: true,
};

class ImageWallElement extends HTMLElement {
	
	constructor(config) {
		super();
  
		config = config || {};

		var self = this;		
		config.src = this.getAttribute("src") || config.src;
		config.height = this.getAttribute("height") || config.height;
		config.width = this.getAttribute("width") || config.width;
		config.maxWidth = this.getAttribute("max-width") || config.width;

		var img = new Image();
		img.src = config.src;
		img.addEventListener("load", (event) => {

			var widthHeightRatio = event.target.width / event.target.height;

			self.style.height = `${config.height || event.target.height}px`;
			var calculatedWidth = (config.height || event.target.height) * widthHeightRatio;
			self.style.width = `${config.width || calculatedWidth}px`;

			self.style.backgroundImage = `url(${config.src})`;
			self.style.backgroundPosition = "center"; /* Center the image */
			self.style.backgroundRepeat = "no-repeat"; /* Do not repeat the image */
			self.style.backgroundSize = (config.height || event.target.height) > calculatedWidth ? "contain" : "cover"; /* Resize the*/
			self.style.backgroundAttachment = "local";
			self.style.display = "block";
		});
	}
}

customElements.define('ndm-img-wall', ImageWallElement);
/*
	<div class="modal" id="modal-1">
		<p class="modal-closer h1" id="close-portrait">X</p>
		<div class="modal-container d-flex justify-content-center align-items-center">
			<img class="modal-img" src="../test-images/big/portraits/space_1.jpg">
		</div>
	</div>
*/

class ModalElement extends HTMLElement {

	config = {};
	bodyElement = {};
	isFirstAttach = true;

	constructor(_config) {
		super();

		this.config = _config || {};
		this.config.id = this.getAttribute("id") || _config && _config.id;
		this.config.content = this.children || _config && _config.content;

		var template = `
			<p class="modal-closer h1">X</p>
			<div class="modal-container d-flex justify-content-center align-items-center" nid="modal-container">
				<img class="modal-img" nid="modal-image"></img>
			</div>
		`;

		let domparser = new DOMParser();
		var bodyElementDocument = domparser.parseFromString(template, "text/html");
		this.bodyElement = bodyElementDocument.body.children;
	}

	withContent(content) {
		this.$nid("modal-image").WithAttribute("src", content);
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		if(!this.isFirstAttach) {
			return;
		}

		this.classList.add("modal");

		if( this.config.content ) {
			if( this.config.content.length !== undefined ) {

				while( this.config.content.length > 0 ) {

					this.bodyElement[1].append(this.config.content[0]);
				}
			} else {
				this.bodyElement[1].appendChild(this.config.content);
			}
		}
	
		this.append(this.bodyElement[0]);
		this.append(this.bodyElement[0]);
		

		this.querySelector(".modal-closer").addEventListener("click", (clickEcent) => {
			$(this).hide();
		});

		this.isFirstAttach = false;
	}
}

customElements.define('ndm-modal', ModalElement);
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
		for(var i = startPage ; i <= endPage; ++i) {
			var button = document.createElement("button").WithClasses(["btn", "btn-sm", "mx-1"]).WithInnerText(i)
				.WithOnClick(
					((pageIdx) => (event) => {
						this.dispatchEvent(new CustomEvent('paging', { bubbles: true, detail: { page: pageIdx } }))
					})(i)
				);

			if( i === activePage )
				button.WithClass("btn-success").WithAttribute("disabled", "true");
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
		
		this.WithoutChildren().append(
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


class ParallaxElement extends HTMLElement {
	
	constructor(config) {
		super();
  
		this.classList.add("position-relative");
		this.classList.add("d-block");
		config = config || {};

		var bodyElement = document.createElement("div");
			
		config.imgSrc = this.getAttribute("img-src") || config.imgSrc;
		config.height = this.getAttribute("height") || config.height;
		
		var imageElement;
		if( config.imgSrc ) {
			imageElement = new Image();
			imageElement.src = config.imgSrc;
		} else if( this.querySelector("picture") ) {
			imageElement = this.querySelector("picture");
		} else if( this.querySelector("img") ) {
			imageElement = this.querySelector("img");
		}

		if(imageElement) {
			imageElement.classList.add("jarallax-img");
			bodyElement.appendChild(imageElement);
		}
		
		bodyElement.setAttribute("data-jarallax", true);
		bodyElement.setAttribute("data-speed", "0.2");
		bodyElement.classList.add("jarallax");
		bodyElement.classList.add("ndm-jarallax");
		bodyElement.style.width = "100%";
		bodyElement.style.height = `${config.height}px`;		
		
		jarallax(bodyElement, {
			speed: 0.2
		});

		var frontLayer = this.querySelector(".front-layer");
		if(frontLayer) {
			frontLayer.style.zIndex = '1';
		}

		this.append(bodyElement);
	}
}

customElements.define('ndm-parallax', ParallaxElement);
// v.1.0.1

"use strict";

/// usage:
/// var pageManager = new SidePager();
///	pageManager.CreatePage(document.createElement("div").innerText = "Hello world");

class SidePagerElement extends HTMLElement {

	pages = new Array();
	domParser = new DOMParser();
	openPages = 0;
	nextPageId = 1;

	pageWidth = 720;
	spaceBetweenPages = 30;

	template = 
		`<div class="side-page">
			<button nid='btn-close'>Close</button>
			<button nid='btn-remove'>Remove</button>
			<div class="side-page-opener"></div>
			<div class="side-page-content"></div>
		</div>`;

	constructor(_config) {
		
		super();	
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		var pages = this.querySelectorAll(".side-page");
		for(var page of pages) {
			var sidePage = this.createPageSkeleton();

			while( page.children.length > 0 ) {

				sidePage.querySelector(".side-page-content").append(page.children[0]);
			}
			page.remove();
			this.appendChild(sidePage);
		}

		this.style.display = "block";
		this.closeAll();
	}

	createPageSkeleton() {
			
		var pageElement = this.domParser.parseFromString(this.template, "text/html")
									.querySelector("div:first-child");

		pageElement.style.zIndex = 11 + this.nextPageId;		

		pageElement.querySelector("div[class='side-page-opener']")
			.addEventListener("click", ((pageId) => () => { this.openPage(pageId) })(this.nextPageId) );
		
		pageElement.querySelector("button[nid='btn-close']")
			.addEventListener("click", ((pageId) => () => { this.closePage(pageId) })(this.nextPageId) );
		
		pageElement.querySelector("button[nid='btn-remove']")
			.addEventListener("click", ((pageId) => () => { this.removePage(pageId) })(this.nextPageId) );

		this.pages.push({ id: this.nextPageId, element: pageElement });
		this.nextPageId += 1;
		return pageElement;
	};

	openPage(pageId) {

		var index = this.getIndexByPageId(pageId);
		if( index < 0 )
			return;

		var closedPagesWidth = (this.pages.length - 1 - index) * this.spaceBetweenPages;
		var openPages_buffer = index * this.spaceBetweenPages;

        var pageRightPosition = closedPagesWidth + openPages_buffer;
        for (var i = 0; i <= index; ++i) {

            var currentElement = this.pages[i];
            currentElement.element.style.right = pageRightPosition + "px";
            currentElement.element.classList.add("side-page-open");
            currentElement.element.classList.remove("side-page-closed");
            pageRightPosition -= this.spaceBetweenPages;
        }

        document.body.style.overflowY = "hidden";
        this.openPages++;
    };

    closePage(pageId) {

		var index = this.getIndexByPageId(pageId);
		if( index < 0 )
			return;

		var buffer = this.pages.length - index - 1; 
        var pageRightPosition = (buffer * this.spaceBetweenPages) + this.spaceBetweenPages;
        for (var i = index; i < this.pages.length; ++i) {
            var actual = this.pages[i];
            actual.element.style.left = null;
            actual.element.style.width = this.pageWidth + "px";
			actual.element.style.right = "-" + (this.pageWidth - pageRightPosition) + "px";
			
            actual.element.classList.remove("side-page-open");
            actual.element.classList.add("side-page-closed");
            pageRightPosition -= this.spaceBetweenPages;
        }

        this.openPages--;

        if (this.openPages <= 0) {
            document.body.style.overflowY = "auto";
            this.openPages = 0;
        }
    };

	getIndexByPageId(pageId) {

		var actual = this.pages.find(item => item.id === pageId);

		if( !actual )
			return;

		return this.pages.indexOf(actual);
	};

    removePage(pageId) {

		var index = this.getIndexByPageId(pageId);
		if( index < 0 )
			return;

        var toRemove = this.pages[index];
        this.pages.splice(index, 1);
		toRemove.element.remove();

		if( index > 0 )
        	openPage(--index);
    };

    closeAll() {
		this.closePage(1);
    };

	addPage(contentElement) {

        var sidePage = this.createPageSkeleton();
		sidePage.querySelector("div[class=side-page-content]").append(contentElement);
		this.appendChild(sidePage);
		this.closeAll();
	};
}

customElements.define('ndm-side-pager', SidePagerElement);
