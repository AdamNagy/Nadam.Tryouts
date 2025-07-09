import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditorComponent, TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular';

@Component({
  selector: 'ndm-text-editor',
  standalone: true,
  imports: [EditorComponent, FormsModule, ReactiveFormsModule],
  templateUrl: './text-editor.html',
  styleUrl: './text-editor.scss',
  providers: [
    // this path is pointing to the core js file which will be treated as an asset, so will be copied as is from projects assets folder to the dist assets folder
    { provide: TINYMCE_SCRIPT_SRC, useValue: 'assets/tinymce/tinymce.min.js' },
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TextEditorComponent implements OnChanges {
  @ViewChild('editor') editorElementRef?: ElementRef<EditorComponent>;

  @Output() saving = new EventEmitter<string>();

  @Input() text?: string;

  public textContent = '';

  init: EditorComponent['init'] = {
    plugins: 'lists link image table code help wordcount',
    base_url: '/assets/tinymce', // Root for resources
    suffix: '.min', // Suffix to use when loading resources
  };

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['text'].currentValue) {
      this.textContent = changes['text'].currentValue;
    }
  }

  public save() {
    this.saving.emit(this.textContent);
  }
}
