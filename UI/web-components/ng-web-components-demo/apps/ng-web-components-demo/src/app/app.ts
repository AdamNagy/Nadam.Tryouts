import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  Inject,
  ViewChild,
} from '@angular/core';
import { RouterModule } from '@angular/router';
import {
  VIEW_STATE,
  ViewState,
} from './components/auto-height/auto-height.directive';

@Component({
  imports: [RouterModule],
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements AfterViewInit {
  protected title = 'ng-web-components-demo';

  @ViewChild('menubar') menubar?: ElementRef<HTMLScriptElement>;

  public screenHeight = 0;
  public menubarHeight = 0;
  public availableHeight = 0;

  private readonly viewState?: ViewState = inject(ViewState);

  ngAfterViewInit(): void {
    this.#adjustHeight();
    window.addEventListener('resize', this.#adjustHeight.bind(this));
  }

  #adjustHeight() {
    this.screenHeight = document.documentElement.clientHeight;
    this.menubarHeight = this.menubar?.nativeElement.offsetHeight || 0;
    this.availableHeight = this.screenHeight - this.menubarHeight;

    if (!this.viewState) {
      return;
    }
    this.viewState.screenHeight = this.screenHeight;
    this.viewState.menubarHeight = this.menubarHeight;
    this.viewState.availableHeight = this.availableHeight - 18;
    console.log(
      `Screen Height: ${this.screenHeight}, Menubar Height: ${this.menubarHeight}, Available Height: ${this.availableHeight}`
    );
  }
}
