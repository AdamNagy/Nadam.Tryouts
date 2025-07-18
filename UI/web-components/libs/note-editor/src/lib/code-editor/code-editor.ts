import {
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';

import 'ace-builds/src-noconflict/ace';

@Component({
  standalone: true,
  selector: 'ndm-code-editor',
  templateUrl: './code-editor.html',
  styleUrl: './code-editor.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CodeEditor implements OnInit, AfterViewInit {
  @ViewChild('editor') private editor?: ElementRef<HTMLElement>;
  @Input() initialCode?: string;

  public value = '';

  ngOnInit(): void {
    if (this.initialCode) {
      this.value = this.initialCode;
    }
  }

  ngAfterViewInit(): void {
    // @ts-expect-error asd
    ace.config.set('basePath', 'https://ace.c9.io/build/src-noconflict/');

    // @ts-expect-error asd
    const aceEditor = ace.edit(this.editor?.nativeElement);
    aceEditor.setTheme('ace/theme/github');
    aceEditor.session.setValue('<h1>Ace Editor works great in Angular!</h1>');
    aceEditor.session.setMode('ace/mode/html');
  }
}
