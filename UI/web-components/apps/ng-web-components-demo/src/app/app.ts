import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  ViewChild,
} from '@angular/core';
import { RouterModule } from '@angular/router';
import { ViewState } from './components/auto-height/auto-height.directive';

@Component({
  imports: [RouterModule],
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements AfterViewInit {
  protected title = 'ng-web-components-demo';

  @ViewChild('menubar') menubar?: ElementRef<HTMLElement>;

  @ViewChild('body') body?: ElementRef<HTMLElement>;

  public availableHeight = 0;

  private readonly viewState?: ViewState = inject(ViewState);

  ngAfterViewInit(): void {
    this.#adjustHeight();
    window.addEventListener('resize', this.#adjustHeight.bind(this));
  }

  #adjustHeight() {
    const screenHeight = document.documentElement.clientHeight;
    const menubarHeight = this.menubar?.nativeElement.offsetHeight || 0;
    this.availableHeight = screenHeight - menubarHeight;

    if (!this.viewState) {
      return;
    }
    this.viewState.availableHeight = this.availableHeight - 18;
    this.body!.nativeElement.style.top = `${menubarHeight}px`;
  }
}
