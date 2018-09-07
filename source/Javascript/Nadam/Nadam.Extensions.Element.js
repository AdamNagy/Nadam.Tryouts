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