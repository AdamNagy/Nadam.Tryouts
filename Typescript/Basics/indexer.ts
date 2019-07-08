class MyClass {
	prop1 = "Hello world";
	prop2 = 10;
	prop3 = {a: 1, b: 2};
}

(function testingIndexer(): void {

	var myObj = new MyClass();
	for (const key in myObj) {
		let value = myObj[key];
		console.log(value);
	}		
})();