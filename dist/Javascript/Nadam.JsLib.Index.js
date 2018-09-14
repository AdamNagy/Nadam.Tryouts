;
"use strict";

/// <DOM_related_function>
var GetById = function(id) {

    var sameIds = document.getElementById(id);

    if (sameIds !== null && sameIds.length !== undefined && sameIds.length >= 1) {
        return sameIds[0].AddProperty("Type", sameIds[0].nodeName.toLowerCase());
    }
    [1, 2, 3, 4, 5].filter((item) => item / 2 === 0);
    return sameIds.AddProperty("Type", sameIds.nodeName.toLowerCase());
}

var GetByClass = function(className) {

    var sameClasses = document.getElementsByClassName(className);
    // sameClasses.Each((item) => item.AddProperty("Type", item.nodeName.toLowerCase()));

    return sameClasses;
}

var GetByTagName = function(tagName) {

    return document.getElementsByTagName(tagName);
}

var RemoveAllFromDom = function(tagName) {

    var element = GetByTagName(tagName)[0];
    while (element !== null) {
        element.remove();
        element = GetByTagName(tagName)[0];
    }
}

/// </DOM_related_function>

/// <Type_related_function>
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

/// </Type_related_function>
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

    /// Use Array.proptotype.filter
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

    // Array.prototype.ForEach = function(action) {

    //     if (action == null || typeof action !== 'function') {
    //         throw "Action is not a function!";
    //     }

    //     var projected = new Array();

    //     for (var i = 0; i < this.length; ++i) {
    //         projected.push(action(this[i]));
    //     }

    //     return projected;
    // }

    // Array.prototype.ForEach = function(action, idx) {
    //     if (action == null || typeof action !== 'function') {
    //         throw "Action is not a function!";
    //     }

    //     var projected = new Array();

    //     for (var i = 0; i < this.length; ++i) {
    //         projected.push(action(this[i], i));
    //     }

    //     return projected;
    // }

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
    /// Puts a DOM element all direct children in an array, and returns it. Only the direct children, so depth is 1
    /// </summary>
    Element.prototype.GetChildren = function() {
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

        return directchildren;
    }

    /// <summary> 
    /// Gets the direct children of the given element, and filter them with the given predicate
    /// </summary> 
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

    // TODO: finish
    Element.prototype.WhereMany = function(predicate, depth) {

        if (depth === undefined || depth <= 1) {
            return this.Where(predicate);
        }

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

        for (var i = 0; i < children.length; ++i) {
            var subFilteredChildren = children[i].Where(predicate);
            filteredArray = filteredArray.concat(subFilteredChildren);
        }

        return filteredArray;
    }

    Element.prototype.Each = function(action, depth) {

        if (action == null || typeof action !== 'function' && typeof depth !== "number") {
            throw "Action is not a function!";
        }

        var children = this.GetChildren(),
            currentDepth = 1;
        if (children === null || children == undefined) {
            return;
        }

        for (var i = 0; i < children.length; ++i) {
            action(children[i]);
            if (currentDepth < depth) {
                children[i].Each(action);
            }
        }
    }

})();
;
"use strict";

(function() {

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
        if (!this.HasProperty(name)) {
            this[name] = value;
        }
        return this;
    }

})();