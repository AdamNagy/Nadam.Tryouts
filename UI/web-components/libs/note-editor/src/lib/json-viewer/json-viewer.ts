import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { NgxJsonViewerModule } from 'ngx-json-viewer';

@Component({
  standalone: true,
  selector: 'ndm-json-viewer',
  imports: [NgxJsonViewerModule],
  templateUrl: './json-viewer.html',
  styleUrl: './json-viewer.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JsonViewerComponent {
  @Input() model: object = {};
  @Input() expanded = false;
  @Input() depth = 3;
}
