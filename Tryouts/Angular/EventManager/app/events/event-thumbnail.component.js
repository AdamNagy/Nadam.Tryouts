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
var EventThumbnailComponent = /** @class */ (function () {
    function EventThumbnailComponent() {
    }
    //ngClass can accept object, string, array
    EventThumbnailComponent.prototype.getClasses = function () {
        // const isEarly = this.event && this.event.time === '8:00 am';
        // return { yellow: isEarly, bold: isEarly };
        // if (this.event && this.event.time === '8:00 am')
        //     return 'yellow bold';
        // return '';
        if (this.event && this.event.time === '8:00 am')
            return ['yellow', 'bold'];
        return [];
    };
    EventThumbnailComponent.prototype.getStyles = function () {
        if (this.event && this.event.time === '8:00 am')
            return { 'text-decoration': 'underline', 'font-style': 'italic' };
        return {};
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], EventThumbnailComponent.prototype, "event", void 0);
    EventThumbnailComponent = __decorate([
        core_1.Component({
            selector: 'event-thumbnail',
            template: "\n        <div [routerLink]=\"['/events', event.id]\" class=\"well hoverwell thumbnail\">\n            <h2>Event: {{event?.name | uppercase}}</h2>\n            <div>Price: {{event?.price | currency:'USD':true}}</div>\n            <div>Date: {{event?.date | date:'shortDate'}}</div>\n            <!--<div [class.yellow]=\"event?.time === '8:00 am'\" [ngSwitch]=\"event?.time\">--><!--will add yellow class if true-->\n            <!--<div [ngClass]=\"{yellow:event?.time === '8:00 am',bold:event?.time === '8:00 am'}\" [ngSwitch]=\"event?.time\">-->\n            <!--<div class=\"notaffected\" [ngClass]=\"getClasses()\" [style.text-decoration]=\"event?.time === '8:00 am'?'underline':'normal'\" [ngSwitch]=\"event?.time\">-->\n            <!--<div class=\"notaffected\" [ngClass]=\"getClasses()\" [ngStyle]=\"{'text-decoration':event?.time === '8:00 am'?'underline':'normal','font-style':'italic'}\" [ngSwitch]=\"event?.time\">-->\n            <div class=\"notaffected\" [ngClass]=\"getClasses()\" [ngStyle]=\"getStyles()\" [ngSwitch]=\"event?.time\">\n                Time: {{event?.time}}\n                <span *ngSwitchCase=\"'8:00 am'\">(Early Start)</span>\n                <span *ngSwitchCase=\"'10:00 am'\">(Late Start)</span>\n                <span *ngSwitchDefault>(Normal Start)</span>\n            </div>\n            <div *ngIf=\"event?.location\">Address: {{event?.location?.address}}, {{event?.location?.city}}, {{event?.location?.country}}</div>\n            <div *ngIf=\"event?.onlineUrl\">Online Url: {{event?.onlineUrl}}</div>\n        </div>\n    ",
            styles: ["\n        .yellow{color:yellow !important;}\n        .bold{font-weight:800;}\n        .notaffected{font-size:18px;}\n        .thumbnail{min-height:210px;}\n        .margin-left{margin-left:10px;}\n        .well div{color:#bbb;}\n    "]
        })
    ], EventThumbnailComponent);
    return EventThumbnailComponent;
}());
exports.EventThumbnailComponent = EventThumbnailComponent;
//# sourceMappingURL=event-thumbnail.component.js.map