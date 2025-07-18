import {
  AfterViewInit,
  Directive,
  ElementRef,
  inject,
  Injectable,
  InjectionToken,
  Input,
  Provider,
} from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

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
    // this.viewState?.availableHeight$.subscribe((height) => {
    // });
    if (this.elementRef?.nativeElement) {
      const siblings = Array.from(
        this.elementRef?.nativeElement.parentElement
          ?.children as HTMLCollectionOf<HTMLElement>
      );
      let availHeight = this.viewState?.availableHeight || 0;
      for (const sibling of siblings) {
        // clientHeight includes padding but not margin, so we need to subtract margin
        availHeight -= sibling.clientHeight;
      }
      //this.viewState!.availableHeight = availHeight;
      // Set the height of the element to the available height.
      // if there is any padding, then need to subtract additional 3 from availHeight
      this.elementRef.nativeElement.style.height = `${availHeight - 3}px`;
    }
  }
}

@Injectable({
  providedIn: 'root',
})
export class ViewState {
  #availableHeight = 0;

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
}
