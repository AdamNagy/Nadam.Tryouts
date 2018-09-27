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