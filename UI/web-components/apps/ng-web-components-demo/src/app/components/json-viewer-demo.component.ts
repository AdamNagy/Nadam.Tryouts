import { Component } from '@angular/core';
import { JsonViewerComponent } from '@ndm-note-editor';

@Component({
  standalone: true,
  imports: [JsonViewerComponent],
  template: `<ndm-json-viewer
    [model]="testModel"
    [expanded]="true"
  ></ndm-json-viewer>`,
})
export class JsonViewerDemoComponent {
  public testModel = {
    prop1: 1,
    prop2: 'Hello',
    prop3: [1, 2, 3],
    prop4: ['qwe', 'asd', 'yxc'],
  };
}
