;
"use strict";

(function() {

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
        if (this.HasProperty(name)) {
            this[name] = value;
        }
        return this;
    }

    /// <summary>
    /// Extends the object with properties coming from the extension object with thise values
    /// </summary>
    /// <returns>nothing</returns>
    Object.prototype.ExtendWith = function(extension) {
        return this;
    }

})();