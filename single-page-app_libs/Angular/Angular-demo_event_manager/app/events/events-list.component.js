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
//OnInit is like interface which class must implements ngOnInit method
var core_1 = require("@angular/core");
var event_service_1 = require("./shared/event.service");
var router_1 = require("@angular/router");
var EventsListComponent = /** @class */ (function () {
    function EventsListComponent(eventService, route) {
        this.eventService = eventService;
        this.route = route;
    }
    EventsListComponent.prototype.ngOnInit = function () {
        // // this.events = this.eventService.getEvents()
        // this.eventService.getEvents().subscribe(events => { this.events = events })  //since we are using resolver
        this.events = this.route.snapshot.data['events1'];
    };
    EventsListComponent = __decorate([
        core_1.Component({
            //selector: 'events-list',  //not needed since we are using route
            templateUrl: 'app/events/events-list.component.html' //relative to index.html
        }),
        __metadata("design:paramtypes", [event_service_1.EventService, router_1.ActivatedRoute])
    ], EventsListComponent);
    return EventsListComponent;
}());
exports.EventsListComponent = EventsListComponent;
//# sourceMappingURL=events-list.component.js.map