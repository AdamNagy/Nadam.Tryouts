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