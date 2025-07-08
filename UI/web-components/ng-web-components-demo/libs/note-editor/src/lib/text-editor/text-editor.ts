import { ChangeDetectionStrategy, Component } from '@angular/core';
import { EditorComponent, TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular';

@Component({
  selector: 'ndm-text-editor',
  standalone: true,
  imports: [EditorComponent],
  templateUrl: './text-editor.html',
  styleUrl: './text-editor.scss',
  providers: [
    { provide: TINYMCE_SCRIPT_SRC, useValue: 'assets/tinymce/tinymce.min.js' },
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TextEditorComponent {
  init: EditorComponent['init'] = {
    plugins: 'lists link image table code help wordcount',
    base_url: '/assets/tinymce', // Root for resources
    suffix: '.min', // Suffix to use when loading resources
  };
}
