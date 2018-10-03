window.TestNamespace = window.TestNamespace || {}
TestNamespace.StaticClass1 = new function() {
	
	// private functions
	var printHelo = function() {
		console.log("helika");
	}
	
	// this is private as well
	this.PublicFunc = function() {
		console.log("PublicFunc")
	}
	
	// public object with public functions and props
	return { PrintHelo: printHelo,
		SayHello: "Sayin halika"}
}

/********************************************************/
window.TestNamespace = window.TestNamespace || {}
TestNamespace.StaticClass2 = new function() {

	var privateFunc = function() {
		console.log("private")
	}

	this.PublicFunc = function(){
		console.log("halika from ctor")
	}
};

TestNamespace.StaticClass2.PublicFunc();

/********************************************************/
window.TestNamespace = window.TestNamespace || {}
TestNamespace.PublicClass = function(arg1, arg2) {
	
	var self = this;

	var prettyPrint = function() {
		return "?" + self.Arg1 + "  " + self.Arg2 + "?";
	}
	
	this.Arg1 = arg1;
	this.Arg2 = arg2;

	this.PrintHelo = function() {
		console.log(prettyPrint());
	}	
}

var testObject = new TestNamespace.PublicClass("Halika", 10);
testObject.PrintHelo();

