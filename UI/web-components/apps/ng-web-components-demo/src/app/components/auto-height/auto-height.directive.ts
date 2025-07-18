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

  #nature = '';
  @Input('appAutoHeight') public set nature(value: string) {
    this.#nature = value;
  }

  constructor(private readonly elementRef?: ElementRef) {}

  ngAfterViewInit(): void {
    this.viewState?.availableHeight$.subscribe((height) => {
      if (this.elementRef?.nativeElement) {
        // Set the height of the element to the available height.
        this.elementRef.nativeElement.style.height = `${height}px`;
      }
    });
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
