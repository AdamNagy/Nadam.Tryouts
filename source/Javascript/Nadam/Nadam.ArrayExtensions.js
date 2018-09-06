;
"use strict";

(function() {

    Array.prototype.Where = function(predicate) {

        if (predicate == null || typeof predicate !== 'function') {
            throw "Predicate is not a function!";
        }

        var filteredArray = new Array();

        for (var i = 0; i < this.length; ++i) {
            if (predicate(this[i])) {
                filteredArray.push(this[i])
            }
        }

        return filteredArray;
    }

    Array.prototype.FirstOrDefault = function(predicate) {

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

    Array.prototype.ForEach = function(action) {

        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        var projected = new Array();

        for (var i = 0; i < this.length; ++i) {
            projected.push(action(this[i]));
        }

        return projected;
    }

    Array.prototype.ForEach = function(action, idx) {
        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        var projected = new Array();

        for (var i = 0; i < this.length; ++i) {
            projected.push(action(this[i], i));
        }

        return projected;
    }

    Array.prototype.Each = function(action) {

        if (action == null || typeof action !== 'function') {
            throw "Action is not a function!";
        }

        for (var i = 0; i < this.length; ++i) {
            action(this[i]);
        }
    }

    Array.prototype.Any = function(predicate) {

        if (predicate == null || typeof predicate !== 'function') {
            throw "Predicate is not a function!";
        }

        var any = false;

        for (var i = 0; i < this.length; ++i) {
            if (predicate(i)) {
                any = true;
            }
        }

        return any;
    }

})();