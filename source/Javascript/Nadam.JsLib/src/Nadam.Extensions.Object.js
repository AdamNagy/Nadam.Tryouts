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