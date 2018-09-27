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

    // Array.prototype.Each = function(action) {

    //     if (action == null || typeof action !== 'function') {
    //         throw "Action is not a function!";
    //     }

    //     for (var i = 0; i < this.length; ++i) {
    //         this[i] = action(this[i]);
    //     }

    //     return this;
    // }

    Array.prototype.Where = function(predicate) {
        return this.filter(predicate);
    }

    Array.prototype.Select = function(action) {

        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        for (var i = 0; i < this.length; ++i) {
            this[i] = action(this[i]);
        }

        return this;
    }

})();