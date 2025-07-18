import {
  AfterViewInit,
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { SidePagerElement } from '@ng-web-components-demo/web-components';

@Component({
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  template: `<section class="body">
    <button (click)="addPage()">Add</button>
    <button (click)="closeAll()">Close All</button>
    <ndm-side-pager #pager id="pager-1"></ndm-side-pager>
  </section> `,
  styles: `
        .body {
            padding-top: 60px;
        }
    `,
})
export class SidePagerDemoComponent implements AfterViewInit {
  @ViewChild('pager') pager?: ElementRef<SidePagerElement>;
  ngAfterViewInit(): void {
    if (this.pager) {
      this.pager.nativeElement.addPage(document.createElement('p'));
    }
  }

  public addPage() {
    this.pager?.nativeElement.addPage(document.createElement('p'));
  }

  public closeAll() {
    this.pager?.nativeElement.closeAll();
  }
}
