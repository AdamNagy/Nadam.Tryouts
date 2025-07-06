export class DragScrollImageElement extends HTMLElement {
  static observedAttributes = ['src'];

  #root?: ShadowRoot;
  #image: HTMLImageElement = document.createElement('img');

  #xCorrection = 0;
  #yCorrection = 0;

  #scrollAmount = 90;

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
    this.#image.addEventListener('drag', this.#setImagePosition.bind(this));
    this.#image.addEventListener('wheel', this.#scrollImage.bind(this));

    this.#image.setAttribute('id', 'the-image');

    const style = document.createElement('style');
    style.textContent = `
      #the-image {
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

  #setImagePosition(event: any) {
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

  #checkScrollDirection(event: any) {
    if (this.#checkScrollDirectionIsUp(event)) {
      return 1;
    } else {
      return -1;
    }
  }

  #checkScrollDirectionIsUp(event: any) {
    if (event.wheelDelta) {
      return event.wheelDelta > 0;
    }
    return event.deltaY < 0;
  }

  #scrollImage(event: Event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    const imageX = this.#image.clientWidth;

    const scrollDirection = this.#checkScrollDirection(event);

    if (scrollDirection == -1) {
      this.#image.style.width = `${imageX - this.#scrollAmount}px`;
    } else {
      this.#image.style.width = `${imageX + this.#scrollAmount}px`;
    }
  }
}

customElements.define('ndm-image', DragScrollImageElement);
