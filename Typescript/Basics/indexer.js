var MyClass = /** @class */ (function () {
    function MyClass() {
        this.prop1 = "Hello world";
        this.prop2 = 10;
        this.prop3 = { a: 1, b: 2 };
    }
    return MyClass;
}());
(function testingIndexer() {
    var myObj = new MyClass();
    for (var key in myObj) {
        var value = myObj[key];
        console.log(value);
    }
})();
