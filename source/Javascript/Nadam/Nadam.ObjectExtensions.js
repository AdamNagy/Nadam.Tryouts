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

})();