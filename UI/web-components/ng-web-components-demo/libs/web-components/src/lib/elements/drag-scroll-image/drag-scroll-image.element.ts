import { checkScrollDirection, ScrollEvent } from '../../scroll-event';
import { GetNumberValue } from '../../utils';

export class DragScrollImageElement extends HTMLElement {
  static observedAttributes = ['src'];

  #root?: ShadowRoot;
  #image: HTMLImageElement = document.createElement('img');

  #xCorrection = 0;
  #yCorrection = 0;

  #scrollAmount = 90;

  #originalWidth?: number;
  #originalHeight?: number;

  get ratio() {
    if (!this.#originalWidth || !this.#originalHeight) {
      return 0;
    }

    return this.#originalWidth / this.#originalHeight;
  }

  connectedCallback() {
    this.#root = this.attachShadow({ mode: 'open' });

    this.#image.addEventListener('mousedown', (event) => {
      const x = event.clientX;
      const y = event.clientY;

      const imageX = this.#image.offsetLeft;
      const imageY = this.#image.offsetTop;

      this.#xCorrection = x - imageX;
      this.#yCorrection = y - imageY;
    });

    this.#image.classList.add('scrollable-dragable-image');

    this.#image.addEventListener('drag', this.#setImagePosition.bind(this));
    this.#image.addEventListener('wheel', (event) => {
      this.#scrollImage.bind(this)(event as ScrollEvent);
    });

    this.#image.addEventListener('load', (event) => {
      this.#originalWidth = (event.target as HTMLImageElement).naturalWidth;
      this.#originalHeight = (event.target as HTMLImageElement).naturalHeight;
    });

    const style = document.createElement('style');
    style.textContent = `
      .scrollable-dragable-image {
        position: relative;
        top: 0;
        left: 0;
        z-index: 1;
        width: 480px;
      }
    `;

    this.#root.appendChild(style);
    this.#root.appendChild(this.#image);
  }

  attributeChangedCallback(name: string, oldValue: string, newValue: string) {
    switch (name) {
      case 'src': {
        this.#image.setAttribute('src', newValue);
        break;
      }
    }
  }

  #setImagePosition(event: DragEvent) {
    const x = event.clientX;
    const y = event.clientY;

    if (x <= 0 || y <= 0) {
      return;
    }

    const imageX = x - this.#xCorrection;
    const imageY = y - this.#yCorrection;

    this.#image.style.left = `${imageX}px`;
    this.#image.style.top = `${imageY}px`;
  }

  #scrollImage(event: ScrollEvent) {
    event.stopImmediatePropagation();
    event.preventDefault();

    const imageWith = this.#image.clientWidth;

    const scrollDirection = checkScrollDirection(event);

    const left = GetNumberValue(this.#image.style.left);
    const top = GetNumberValue(this.#image.style.top);

    if (scrollDirection == -1) {
      const newWith = imageWith - this.#scrollAmount;

      if (newWith < 120) {
        return;
      }

      this.#image.style.width = `${newWith}px`;
      this.#image.style.left = `${left + this.#scrollAmount / 2}px`;
      this.#image.style.top = `${top + this.#scrollAmount / 3}px`;
    } else {
      if (imageWith === this.#image.naturalWidth) {
        return;
      }

      const newWith = Math.min(
        imageWith + this.#scrollAmount,
        this.#image.naturalWidth
      );

      this.#image.style.width = `${newWith}px`;
      this.#image.style.left = `${left - this.#scrollAmount / 2}px`;
      this.#image.style.top = `${top - this.#scrollAmount / 3}px`;
    }
  }
}

customElements.define('ndm-image', DragScrollImageElement);
