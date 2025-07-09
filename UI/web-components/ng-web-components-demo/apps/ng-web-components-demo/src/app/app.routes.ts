import { Route } from '@angular/router';
import { DragScrollDemoComponent } from './components/drag-scroll-demo.component';
import { ParallaxDemoComponent } from './components/parallax-demo.component';
import { SidePagerDemoComponent } from './components/side-pager-demo.component';
import { TextEditorDemoComponent } from './components/text-editor-demo.component';
import { JsonViewerDemoComponent } from './components/json-viewer-demo.component';
import { CodeEditorDemoComponent } from './components/code-editor-demo.component';

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
  {
    path: 'text-editor',
    pathMatch: 'full',
    component: TextEditorDemoComponent,
  },
  {
    path: 'json-viewer',
    pathMatch: 'full',
    component: JsonViewerDemoComponent,
  },
  {
    path: 'code-editor',
    pathMatch: 'full',
    component: CodeEditorDemoComponent,
  },
];
