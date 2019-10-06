"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var AuthService = /** @class */ (function () {
    function AuthService(http) {
        this.http = http;
    }
    AuthService.prototype.loginUser = function (userName, password) {
        // let headers = new Headers({ 'Content-Type': 'applicaton/json' });
        // let options = new RequestOptions({ headers: headers });
        // let loginInfo = { username: userName, password: password };
        // //put is exactly same code with post
        // return this.http.post('/api/login', JSON.stringify(loginInfo), options)
        //     .do((res: Response) => {
        //         if (res) {
        //             this.currentUser = <IUser>res.json().user;
        //         }
        //     }).catch(err => {
        //         return Observable.of(false);
        //     });
    };
    AuthService.prototype.isAuthenticated = function () {
        return !!this.currentUser;
    };
    AuthService.prototype.updateCurrentUser = function (firstName, lastName) {
        this.currentUser.firstName = firstName;
        this.currentUser.lastName = lastName;
        var headers = new http_1.Headers({ 'Content-Type': 'applicaton/json' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.put("/api/users/" + this.currentUser.id, JSON.stringify(this.currentUser), options);
    };
    //persist user authentication status, call this is app.component.ts
    AuthService.prototype.checkAuthenticationStatus = function () {
        var _this = this;
        return this.http.get('/api/currentIdentity').map(function (res) {
            if (res._body) {
                return res.json();
            }
            else {
                return {};
            }
        }).do(function (currentUser) {
            if (!!currentUser.userName) {
                _this.currentUser = currentUser;
            }
        }).subscribe();
    };
    AuthService.prototype.logout = function () {
        this.currentUser = undefined;
        var headers = new http_1.Headers({ 'Content-Type': 'applicaton/json' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.post('api/logout', JSON.stringify({}), options);
    };
    AuthService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map