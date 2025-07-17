import {
  AfterViewInit,
  Directive,
  ElementRef,
  inject,
  Inject,
  Injectable,
  InjectionToken,
  Provider,
} from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

export const VIEW_STATE = new InjectionToken<ViewState>('VIEW_STATE');

export function provideViewState(): Provider {
  return { provide: VIEW_STATE, useClass: ViewState };
}

@Directive({
  selector: '[appAutoHeight]',
  standalone: true,
})
export class AutoHeightDirective implements AfterViewInit {
  private readonly viewState?: ViewState = inject(ViewState);

  constructor(private readonly elementRef?: ElementRef) {}

  ngAfterViewInit(): void {
    this.viewState?.availableHeight$.subscribe((height) => {
      if (this.elementRef?.nativeElement) {
        const pt = this.elementRef.nativeElement.style.borderTop;
        this.elementRef.nativeElement.style.height = `${height}px`;
      }
    });
  }
}

@Injectable({
  providedIn: 'root',
})
export class ViewState {
  #screenHeight = 0;
  #menubarHeight = 0;
  #availableHeight = 0;

  public get screenHeight(): number {
    return this.#screenHeight;
  }

  public set screenHeight(value) {
    this.#screenHeight = value;
  }

  public get menubarHeight(): number {
    return this.#menubarHeight;
  }

  public set menubarHeight(value) {
    this.#menubarHeight = value;
  }

  // <available-height>
  public get availableHeight(): number {
    return this.#availableHeight;
  }

  public set availableHeight(value) {
    this.#availableHeight = value;
    this.#availableHeight$.next(value);
  }
  #availableHeight$ = new BehaviorSubject<number>(0);
  public get availableHeight$(): Observable<number> {
    return this.#availableHeight$.asObservable();
  }
  // </available-height>
}
