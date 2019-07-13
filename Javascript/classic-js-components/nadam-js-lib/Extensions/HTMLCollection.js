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