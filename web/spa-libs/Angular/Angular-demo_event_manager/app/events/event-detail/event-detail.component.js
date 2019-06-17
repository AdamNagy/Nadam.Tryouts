"use strict";
//http://localhost:8808/events/3
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
var event_service_1 = require("../shared/event.service");
var router_1 = require("@angular/router");
var EventDetailComponent = /** @class */ (function () {
    function EventDetailComponent(eventService, route) {
        this.eventService = eventService;
        this.route = route;
        this.addMode = false;
        this.filterBy = 'all';
        this.sortBy = 'votes'; //default sortBy votes
    }
    EventDetailComponent.prototype.ngOnInit = function () {
        // //+ convert string to number
        // this.event = this.eventService.getEvent(+this.route.snapshot.params['id'])
        var _this = this;
        // whenever route params changes, reset all the states
        this.route.data.forEach(function (data) {
            // this.event = this.eventService.getEvent(+params['id']);  //sync
            // //async, removed because we use EventResolver
            // this.eventService.getEvent(+params['id']).subscribe((e: IEvent) => {
            //     this.event = e;
            //     this.addMode = false;
            //     this.filterBy = 'all';
            //     this.sortBy = 'votes';
            // });
            //async and use EventResolver
            _this.event = data['event'];
            _this.addMode = false;
            _this.filterBy = 'all';
            _this.sortBy = 'votes';
        });
    };
    EventDetailComponent.prototype.addSession = function () {
        this.addMode = true;
    };
    EventDetailComponent.prototype.saveNewSession = function (session) {
        //find max id, and newSession id should = id + 1
        // const maxId = Math.max.apply(null, this.event.sessions.map(s => s.id))
        var maxId = Math.max.apply(Math, this.event.sessions.map(function (s) { return s.id; }));
        session.id = maxId + 1;
        this.event.sessions.push(session);
        this.eventService.saveEvent(this.event).subscribe();
        this.addMode = false;
    };
    EventDetailComponent.prototype.cancelAddSession = function () {
        this.addMode = false;
    };
    EventDetailComponent = __decorate([
        core_1.Component({
            templateUrl: '/app/events/event-detail/event-detail.component.html',
            styles: ["\n        .container{padding:0 20px;}\n        .event-img{height:100px;}\n        a {cursor:pointer;}\n    "]
        }),
        __metadata("design:paramtypes", [event_service_1.EventService, router_1.ActivatedRoute])
    ], EventDetailComponent);
    return EventDetailComponent;
}());
exports.EventDetailComponent = EventDetailComponent;
//# sourceMappingURL=event-detail.component.js.map