var DIContainer = /** @class */ (function () {
    function DIContainer() {
        this.container = [];
    }
    DIContainer.prototype.RegisterType = function (key, type) {
        this.container[key] = type;
    };
    DIContainer.prototype.CreateInstance = function (className, params) {
        // @ts-ignore
        return Reflect.construct(this.container[className], params);
    };
    return DIContainer;
}());

window.AppDIContainer = new DIContainer();
