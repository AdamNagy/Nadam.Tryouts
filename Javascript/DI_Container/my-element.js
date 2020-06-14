var MyElement = /** @class */ (function () {
    function MyElement() {
        this.body = document.createElement("div");
        this.body.innerText = "Hello word of DI in JS";
    }
    return MyElement;
}());

window.AppDIContainer.RegisterType("MyElement", MyElement);
