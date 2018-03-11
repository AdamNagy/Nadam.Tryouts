import {Routes} from "@angular/router";

import { AppComponent } from "./../app/app.component";
import { CollapsableDemoComponent } from "./collapsable-demo/collapsable-demo.component";
import { FocusLayerDemoComponent } from "./focus-layer-demo/focus-layer-demo.component";
import { TabControlDemoComponent } from "./tab-control-demo/tab-control-demo.component";
import { SliderDemoComponent } from "./slider-demo/slider-demo.component";
import { GridDemoComponent } from "./grid-demo/grid-demo.component";

export const appRoutes: Routes = [
	{ path: "slider", component: SliderDemoComponent },
	{ path: "index", component: CollapsableDemoComponent },
	{ path: "tab-control", component: TabControlDemoComponent },
	{ path: "focuslayer", component: FocusLayerDemoComponent },
	{ path: "grid", component: GridDemoComponent }
];