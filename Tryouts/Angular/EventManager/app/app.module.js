"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var platform_browser_1 = require("@angular/platform-browser");
var router_1 = require("@angular/router");
var routes_1 = require("./routes");
var app_component_1 = require("./app.component");
var _404_component_1 = require("./errors/404.component");
var nav_component_1 = require("./nav/nav.component");
var index_1 = require("./events/index");
var index_2 = require("./common/index");
var auth_service_1 = require("./user/auth.service");
var forms_1 = require("@angular/forms");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                router_1.RouterModule.forRoot(routes_1.appRoutes),
                forms_1.FormsModule,
                forms_1.ReactiveFormsModule,
                http_1.HttpModule,
            ],
            declarations: [
                app_component_1.AppComponent,
                nav_component_1.NavBarComponent,
                index_1.EventsListComponent, index_1.EventThumbnailComponent, index_1.EventDetailComponent, index_1.CreateEventComponent,
                index_1.CreateSessionComponent, index_1.SessionListComponent,
                _404_component_1.Error404Component,
                index_2.CollapsibleWellComponent,
                index_2.SimpleModal, index_2.ModalTriggerDirective,
                index_1.DurationPipe,
                index_1.UpvoteComponent,
                index_1.LocationValidator,
            ],
            providers: [
                index_1.EventService,
                // ToastrService,
                { provide: index_2.TOASTR_TOKEN, useValue: toastr },
                { provide: index_2.JQ_TOKEN, useValue: jQuery },
                {
                    provide: 'canDeactivateCreateEvent',
                    useValue: checkDirtyState,
                },
                index_1.EventResolver,
                index_1.EventListResolver,
                auth_service_1.AuthService,
                index_1.VoterService,
            ],
            bootstrap: [app_component_1.AppComponent],
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
// this function can be defined in another file
function checkDirtyState(component) {
    if (component.isDirty) {
        return confirm("you haven't save the event. Are you sure to cancel?");
    }
    return true;
}
//# sourceMappingURL=app.module.js.map