;
"use strict";

(function() {

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

		if(predicate !== null && predicate !== undefined ) {
			return directchildren.filter(predicate);
		}

        return directchildren;
    }

	Element.prototype.OnHover = function(options)  {

		if( options === null || options.length === undefined )
			return

		var currentValues = new Array();
		
		for( var i = 0; i < options.length; ++i ) {
			currentValues.push(this.style[options[i].cssProp]);	
		}  
	
		this.addEventListener("mouseover", function( event ) {   
			for( var i = 0; i < options.length; ++i ) {
				event.target.style[options[i].cssProp] = options[i].newVal;
			}	   	 
		}, false);
	 
		this.addEventListener("mouseleave", function( event ) {
			for( var i = 0; i < options.length; ++i ) {
				event.target.style[options[i].cssProp] = currentValues[i];
			}
		}, false);
	}	

})();