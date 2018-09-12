"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var index_1 = require("./events/index");
var _404_component_1 = require("./errors/404.component");
exports.appRoutes = [
    //order matters
    { path: 'events/new', component: index_1.CreateEventComponent, canDeactivate: ['canDeactivateCreateEvent'] },
    { path: 'events', component: index_1.EventsListComponent, resolve: { events1: index_1.EventListResolver } },
    { path: 'events/:id', component: index_1.EventDetailComponent, resolve: { event: index_1.EventResolver } },
    { path: 'events/session/new', component: index_1.CreateSessionComponent },
    { path: '404', component: _404_component_1.Error404Component },
    { path: '', redirectTo: '/events', pathMatch: 'full' },
    // user prefix, localhost/user/x, will load module here: app/user/user.module and the module name is UserModule, concat '#'
    { path: 'user', loadChildren: 'app/user/user.module#UserModule' }
];
//# sourceMappingURL=routes.js.map