import { Component } from '@angular/core';
import { TextEditorComponent } from '@ndm-note-editor';

@Component({
  selector: 'app-text-editor-demo',
  standalone: true,
  imports: [TextEditorComponent],
  template: ` <ndm-text-editor /> `,
})
export class TextEditorDemoComponent {}
