;
"use strict";

(function() {

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

        if (predicate !== null) {
            var filtered = this.filter(predicate);
            return filtered[filtered.length - 1];
        }

        return this[this.length - 1];
    }

    Array.prototype.Where = function(predicate) {
        return this.filter(predicate);
    }

    Array.prototype.Select = function(action, args) {

        var list = new Array();
        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        for (var i = 0; i < this.length; ++i) {
            list.push(action(this[i], args));
        }

        return list;
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
;
"use strict";

var HtmlTags = {
    div: "div",
    image: "img",
	link: "a",
	script: "script",
	iframe: "iframe"
}
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
var Thumbail = {

    GetImageSrc: function(thumbnail) {
        var div = thumbnail.firstElementChild;
        var image = div.firstElementChild;

        if (div == null || image == null) {
            return false;
        }

        return image.getAttribute("src");
    },

    ToDownloadable: function(thumbnail, generator) {
        thumbnail.removeAttribute("onclick");

        var imageData;
        if (generator === null) {
            imageData = Thumbail.GetDefaultImageData(thumbnail);
        } else {
            imageData = generator(Thumbail.GetImageSrc(thumbnail));
        }

        thumbnail.setAttribute("download", imageData.Title);
        thumbnail.setAttribute("src", imageData.NewSrc);

        return thumbnail;
    },

    GetDefaultImageData: function(thumbail) {

        var oldImageSrc = Thumbail.GetImageSrc(thumbail);
        var title = oldImageSrc.split("/").Last().split(".").Last();
        return {
            Title: title,
            NewSrc: oldImageSrc
        };
    }
}