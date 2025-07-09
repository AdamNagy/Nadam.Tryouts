import { Component } from '@angular/core';
import { CodeEditor } from '@ndm-note-editor';

@Component({
  standalone: true,
  selector: 'app-code-editor-demo',
  imports: [CodeEditor],
  template: `<ndm-code-editor></ndm-code-editor>`,
})
export class CodeEditorDemoComponent {}
