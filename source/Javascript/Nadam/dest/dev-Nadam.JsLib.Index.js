;
"use strict";

/// <summary>
/// 
/// </summary>
/// <returns></returns>
var GetById = function(id) {
	var sameIds = document.getElementById(id);

	if(sameIds.length !== undefined && sameIds.length >= 1) {
		sameIds[0];
	}

	return sameIds;
}

/// <summary>
/// 
/// </summary>
/// <returns></returns>
var GetByClass = function(className) {
    return document.getElementsByClassName(className);
}

var GetByTagName = function(tagName) {
    return document.getElementsByTagName(tagName);
}

var IsPrimitive = function(variable) {
	
    var type = typeof variable;

    if (type === "number" || type === "string" || type === "boolean") {
        return true;
    }

    return false;
}

var IsUndefined = function(variable) {

    return typeof variable === "undefined"
}

var IsFunction = function(variable) {

    return typeof variable === "function";
}

var IsObject = function(variable) {

    return typeof variable === "object";
}

var RemoveScripts = function() {

	var script = GetByTagName(HtmlTags.script)[0];
	while(script !== null){
		script.remove();
	}
}

var RemoveIFrames = function() {

	var iframe = GetByTagName(HtmlTags.iframe)[0];
	while(iframe !== null){
		iframe.remove();
	}
}
;
"use strict";

var HtmlTags = {
    div: "div",
    image: "img",
	link: "a",
	script: "script",
	iframe: "iframe"
}
;
"use strict";

(function() {

    Array.prototype.Where = function(predicate) {

        if (predicate == null || typeof predicate !== 'function') {
            throw "Predicate is not a function!";
        }

        var filteredArray = new Array();

        for (var i = 0; i < this.length; ++i) {
            if (predicate(this[i])) {
                filteredArray.push(this[i])
            }
        }

        return filteredArray;
    }

    Array.prototype.FirstOrDefault = function(predicate) {

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

    Array.prototype.ForEach = function(action) {

        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        var projected = new Array();

        for (var i = 0; i < this.length; ++i) {
            projected.push(action(this[i]));
        }

        return projected;
    }

    Array.prototype.ForEach = function(action, idx) {
        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        var projected = new Array();

        for (var i = 0; i < this.length; ++i) {
            projected.push(action(this[i], i));
        }

        return projected;
    }

    Array.prototype.Each = function(action) {

        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        for (var i = 0; i < this.length; ++i) {
            action(this[i]);
        }
    }

    Array.prototype.Any = function(predicate) {

        if (predicate == null || typeof predicate !== 'function') {
            throw "Predicate is not a function!";
        }

        var any = false;

        for (var i = 0; i < this.length; ++i) {
            if (predicate(i)) {
                any = true;
            }
        }

        return any;
    }

})();
;
"use strict";

(function() {

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Element.prototype.GetChildren = function() {
        var firstChild = this.firstElementChild;
        var directchildren = new Array();

        if (firstChild !== null) {
            directchildren.push(firstChild);
        }

        var sibling = firstChild.nextElementSibling;
        while (sibling != null) {
            directchildren.push(sibling);
            sibling = sibling.nextElementSibling;
        }

        return directchildren;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Element.prototype.Where = function(predicate) {

        if (predicate == null || typeof predicate !== 'function') {
            throw "Predicate is not a function!";
        }

        var filteredArray = new Array();

        var children = this.GetChildren();
        if (children.length < 1) {
            return filteredArray;
        }

        for (var i = 0; i < children.length; ++i) {
            if (predicate(children[i])) {
                filteredArray.push(children[i]);
            }
        }

        return filteredArray;
    }

    Element.prototype.IsDiv = function() {
        return this.tagName == HtmlTags.div;
    }

})();
;
"use strict";

(function() {

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Object.prototype.IsArray = function() {
        if (this != null && this.length !== undefined) {
            return true;
        }

        return false;
    }

    Object.prototype.HasProperty = function(name) {
        return this[name] !== undefined;
    }

    Object.prototype.AddProperty = function(name, value) {
        if (this[name] == undefined) {
            this[name] = value;
        }
    }

    /// <summary>
    /// Extends the object with properties coming from the extension object with thise values
    /// </summary>
    /// <returns>nothing</returns>
    Object.prototype.ExtendWith = function(extension) {

    }

})();