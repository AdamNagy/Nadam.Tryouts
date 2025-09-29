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
import { BehaviorSubject, Observable, of } from 'rxjs';

export const VIEW_STATE = new InjectionToken<ViewState>('VIEW_STATE');

export function provideViewState(): Provider {
  return { provide: VIEW_STATE, useClass: ViewState };
}

@Directive({
  selector: '[appAutoHeight]',
  standalone: true,
})
export class AutoHeightDirective implements AfterViewInit {
  private readonly viewState: ViewState = inject(ViewState);
  private readonly elementRef: ElementRef<HTMLElement> = inject(ElementRef);

  @Input('appAutoHeight') public hostId?: string | undefined;

  ngAfterViewInit(): void {
    if (this.elementRef?.nativeElement) {
      const availHeight$ = this.viewState?.getAvailableHeight(this.hostId);
      availHeight$?.subscribe((availHeight) => {
        const siblings = Array.from(
          this.elementRef?.nativeElement.parentElement
            ?.children as HTMLCollectionOf<HTMLElement>
        );
        for (const sibling of siblings) {
          if (sibling === this.elementRef.nativeElement) {
            continue; // Skip the current element
          }
          // clientHeight includes padding but not margin, so we need to subtract margin
          availHeight -= sibling.clientHeight;
        }

        const id = this.elementRef?.nativeElement.getAttribute('id');
        if (id) {
          this.viewState?.addChildHostHeight(id, availHeight);
        }
        //this.viewState!.availableHeight = availHeight;

        // Set the height of the element to the available height.
        // if there is any padding, then need to subtract additional 3 from availHeight
        this.elementRef.nativeElement.style.height = `${availHeight - 3}px`;
      });
    }
  }
}

@Injectable({
  providedIn: 'root',
})
export class ViewState {
  #availableHeight = 0;
  #childHostHeights: Map<string, BehaviorSubject<number>> = new Map();

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

  public getAvailableHeight(id?: string): Observable<number> {
    if (!id) {
      return this.#availableHeight$.asObservable();
    }

    const exist = this.#childHostHeights.get(id);
    if (exist) {
      return exist.asObservable();
    }

    const heightSubject = new BehaviorSubject<number>(this.#availableHeight);
    this.#childHostHeights.set(id, heightSubject);
    return heightSubject.asObservable();
  }

  public addChildHostHeight(id: string, height: number): void {
    const existing = this.#childHostHeights.get(id);

    if (existing) {
      existing.next(height);
    } else {
      this.#childHostHeights.set(id, new BehaviorSubject(height));
    }
  }
}
