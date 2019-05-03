// Immediately Invoked Function Expression (IIFE)

(function(){
    console.log("this will be a IIEF");
})()
// or
(() => {
    console.log("this will be a IIEF");
})();

// Function declaration
function func1(){  
    console.log('test');
}

// Function expression
(function(){
    console.log('test');
})

// 1) encapsulate code complexity inside IIFE so we don't have to understand what the IIFE code does
// 2) define variables inside the IIFE so they don't pollute the global scope
// (var statements inside the IIFE remain within the IIFE's closure)


/************************************************************************************************ */
// Revealing Module pattern
// Expose module as global variable
var singleton = function() {

    // Inner logic
    function sayHello(){
        console.log('Hello');
    }
  
    // Expose API
    return {
        sayHello: sayHello
    }
}()

// Access module functionality
singleton.sayHello();