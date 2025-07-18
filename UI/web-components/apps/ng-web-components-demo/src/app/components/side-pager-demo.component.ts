import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { JsonViewerComponent, TextEditorComponent } from '@ndm-note-editor';
import { SidePagerElement } from '@ng-web-components-demo/web-components';

@Component({
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [TextEditorComponent, JsonViewerComponent],
  template: `<section class="body">
    <button (click)="addPage()">Add</button>
    <button (click)="closeAll()">Close All</button>
    <ndm-side-pager #pager id="pager-1">
      <div class="side-page-content">
        <ndm-text-editor />
      </div>

      <div class="side-page-content">
        <ndm-json-viewer
          [model]="testModel"
          [expanded]="true"
        ></ndm-json-viewer>
      </div>
    </ndm-side-pager>
  </section> `,
  styles: `
      .body {
          position: relative;
      }
    `,
})
export class SidePagerDemoComponent {
  public testModel = {
    prop1: 1,
    prop2: 'Hello',
    prop3: [1, 2, 3],
    prop4: ['qwe', 'asd', 'yxc'],
  };

  @ViewChild('pager') pager?: ElementRef<SidePagerElement>;

  public addPage() {
    this.pager?.nativeElement.addPage(document.createElement('p'));
  }

  public closeAll() {
    this.pager?.nativeElement.closeAll();
  }
}
