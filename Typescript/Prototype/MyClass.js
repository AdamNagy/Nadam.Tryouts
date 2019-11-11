var MyClass = /** @class */ (function () {
    function MyClass() {
    }
    MyClass.prototype.WithName = function (name) {
        this.Name = name;
        return this;
    };
    MyClass.prototype.WithAge = function (age) {
        this.Age = age;
        return this;
    };
    MyClass.prototype.WithText = function (text) {
        this.Text = text;
        return this;
    };
    return MyClass;
}());
function TestShit() {
    var obj = new MyClass();
    obj.WithName("Halika")
        .WithAge(31)
        .WithText("Some test text to seem this work");
}
