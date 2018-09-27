;
"use strict";

/// <DOM_related_function>
var GetById = function(id) {

    var sameIds = document.getElementById(id);

    if (sameIds !== null && sameIds.length !== undefined && sameIds.length >= 1) {
        return sameIds[0].AddProperty("Type", sameIds[0].nodeName.toLowerCase());
    }

    return sameIds.AddProperty("Type", sameIds.nodeName.toLowerCase());
}

var GetByClass = function(className) {

    var sameClasses = document.getElementsByClassName(className);

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
var Children = {

    isImage: function(element) {
        if (element == null || element.firstElementChild == null) {
            return false;
        }
        return element.firstElementChild.tagName.toLocaleLowerCase() === HtmlTags.image;
    },

    isDiv: function(element) {
        if (element == null || element.firstElementChild == null) {
            return false;
        }
        return element.firstElementChild.tagName.toLocaleLowerCase() === HtmlTags.div;
    },

    // teszting shit
    isThumbnail: function(element) {
        if (element == null || element.firstElementChild == null) {
            return false;
        }

        var firstChild = element.firstElementChild;
        var secondLevelChild = firstChild.firstElementChild;

        if (firstChild == null || secondLevelChild == null) {
            return false;
        }

        return firstChild.tagName.toLocaleLowerCase() === HtmlTags.div &&
            secondLevelChild.tagName.toLocaleLowerCase() === HtmlTags.image;
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

    Array.prototype.FirstOrNull = function(predicate) {

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

    Array.prototype.Each = function(action) {

        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        for (var i = 0; i < this.length; ++i) {
            this[i] = action(this[i]);
        }

        return this;
    }

    Array.prototype.Where = function(predicate) {
        return this.filter(predicate);
    }

})();
;
"use strict";

(function() {

    /// <summary> 
    /// Iterates throught the sub tree of the node, and flattens it into a list. 
    /// Optionally filters the list
    /// </summary> 
    Element.prototype.All = function(predicate) {

        if (predicate == null || typeof predicate !== 'function') {
            throw "Predicate is not a function!";
        }

        var nodeIterator = this.GetIterator(predicate);
        var nodeList = nodeIterator.ToList();

        return nodeList;
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

        return directchildren;
    }

    Element.prototype.Each = function(action) {

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

    Element.prototype.EachChildren = function(action) {

    }

    // Element.prototype.GetIterator = function(filter) {

    //     var iterator = document.createNodeIterator(this, NodeFilter.SHOW_ELEMENT, filter);
    //     return iterator;
    // }

    // Element.prototype.Find = function(predicate) {

    //     var found = this.GetIterator().ToList().find(predicate);

    //     return found[0];
    // }

    // Element.prototype.FindAll = function(predicate) {

    //     var found = this.GetIterator().ToList().filter(predicate);

    //     return found;
    // }

})();
;
"use strict";

(function() {

    NodeIterator.prototype.ToList = function() {

        var list = new Array();
        var node = this.nextNode();
        while (node != null) {
            list.push(node);
            node = this.nextNode();
        }

        return list;
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
;
"use strict";

(function() {

    HTMLCollection.prototype.ToList = function() {

        var list = new Array();

        for (var i = 0; i < this.length; ++i) {
            list.push(this.item(i));
        }

        return list;
    }

})();