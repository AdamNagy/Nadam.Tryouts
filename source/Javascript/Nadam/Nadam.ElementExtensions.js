;
"use strict";

(function() {

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Element.prototype.GetDirectChildren = function() {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Element.prototype.Where = function(predicate, depth) {

        if (predicate == null || typeof predicate !== 'function') {
            throw "Predicate is not a function!";
        }

        var filteredArray = new Array();

        // TODO: graph functions for element (get first lined children)
    }

})();