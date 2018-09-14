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