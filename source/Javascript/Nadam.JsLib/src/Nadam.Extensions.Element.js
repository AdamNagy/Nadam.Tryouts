;
"use strict";

(function() {

    /// <summary> 
    /// Iterates throught the sub tree of the node, and flattens it into a list. 
    /// Optionally filters the list
    /// </summary> 
    Element.prototype.All = function(predicate) {

        // HINT
        // var nodeIterator = document.createNodeIterator(root, whatToShow, filter);
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

    Element.prototype.GetIterator = function(filter) {

        var iterator = document.createNodeIterator(this, NodeFilter.SHOW_ELEMENT, filter);
        iterator.ToList = ToList();
        return iterator;
    }

    Element.prototype.Find = function(predicate) {

        var found = this.GetIterator().ToList().find(predicate);

        return found[0];
    }

    Element.prototype.FindAll = function(predicate) {

        var found = this.GetIterator().ToList().find(predicate);

        return found[0];
    }

})();