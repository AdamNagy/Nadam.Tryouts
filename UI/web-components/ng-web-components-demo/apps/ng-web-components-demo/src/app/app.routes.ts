import { Route } from '@angular/router';
import { DragScrollDemoComponent } from './components/drag-scroll-demo.component';
import { ParallaxDemoComponent } from './components/parallax-demo.component';
import { SidePagerDemoComponent } from './components/side-pager-demo.component';

export const appRoutes: Route[] = [
  {
    path: 'drag-scroll',
    pathMatch: 'full',
    component: DragScrollDemoComponent,
  },
  {
    path: 'parallax',
    pathMatch: 'full',
    component: ParallaxDemoComponent,
  },
  {
    path: 'side-pager',
    pathMatch: 'full',
    component: SidePagerDemoComponent,
  },
];
