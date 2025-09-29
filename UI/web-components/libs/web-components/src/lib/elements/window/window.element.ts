import { ToCssElement } from '../../style-utils/css-rule';

export class WindowElement extends HTMLElement {
  static observedAttributes = ['with', 'height', 'title', 'visible'];

  #root?: ShadowRoot;
  #windowElement?: HTMLElement;

  #domParser = new DOMParser();

  #head$?: HTMLElement;
  #body$?: HTMLElement;
  #title$?: HTMLElement;
  #resizer$?: HTMLElement;

  #template = `
    <div class="window">
      <div class="window-head" nid="head" draggable="true">
        <div nid="title"></div>
        <div nid="operations">
         O _ X
        </div>
      </div>
      <div class="window-body" nid="body">
      </div>
      <div class="window-foot" nid="footer">
        <label>Num of items</label>
        <span draggable="true" nid="resizer">X</span>
      </div>
    </div>`;

  #attributes: {
    [key: string]: string;
  } = {};

  #xCorrection = 0;
  #yCorrection = 0;

  connectedCallback() {
    this.#windowElement = this.#domParser
      .parseFromString(this.#template, 'text/html')
      .querySelector('div.window')!;

    const innerContent = this.querySelector('div');

    this.#head$ = this.#windowElement.querySelector('[nid="head"]')!;
    this.#title$ = this.#windowElement.querySelector('[nid="title"]')!;
    this.#body$ = this.#windowElement.querySelector('[nid="body"]')!;
    this.#resizer$ = this.#windowElement.querySelector('[nid="resizer"]')!;

    this.#updateElementWitAttribues();

    if (innerContent) {
      this.#body$.appendChild(innerContent);
    }

    const style = ToCssElement([
      {
        selector: '.window',
        position: 'absolute',
        top: '120px',
        left: '30px',
        width: '640px',
        height: '480px',
        'border-radius': '5px',
      },
      {
        selector: '.window-head',
        padding: '10px',
        display: 'flex',
        'justify-content': 'space-between',
        cursor: 'pointer',
        'background-color': 'grey',
        'user-select': 'none',
        'border-top': '1px solid black',
        'border-left': '1px solid black',
        'border-right': '1px solid black',
      },
      {
        selector: '.window-body',
        height: '100%',
        'background-color': 'lightgrey',
        color: 'black',
        padding: '10px',
        'border-left': '1px solid black',
        'border-right': '1px solid black',
      },
      {
        selector: '.window-foot',
        padding: '5px',
        display: 'flex',
        'justify-content': 'space-between',
        'background-color': 'grey',
        'user-select': 'none',
        border: '1px solid black',
      },
      {
        selector: '[nid="resizer"]',
        cursor: 'nwse-resize',
      },
    ]);

    this.#initDragDrop();
    this.#initResizer();

    this.appendChild(style);
    this.appendChild(this.#windowElement);
  }

  attributeChangedCallback(name: string, oldValue: string, newValue: string) {
    this.#attributes[name] = newValue;
  }

  #updateElementWitAttribues() {
    for (const name of Object.keys(this.#attributes)) {
      switch (name) {
        case 'title':
          this.#title$!.innerText = this.#attributes[name];
          break;

        case 'height':
          this.#windowElement!.style.height = `${this.#attributes[name]}px`;
          break;

        case 'width':
          this.#windowElement!.style.width = `${this.#attributes[name]}px`;
          break;

        case 'visible':
          this.style.display = this.#attributes[name] ? 'block' : 'none';
      }
    }
  }

  #initDragDrop() {
    this.#head$!.addEventListener('dragstart', (event) => {
      const x = event.clientX;
      const y = event.clientY;

      const imageX = this.#windowElement!.offsetLeft;
      const imageY = this.#windowElement!.offsetTop;

      this.#xCorrection = x - imageX;
      this.#yCorrection = y - imageY;
    });

    this.#head$!.addEventListener('drag', this.#setWindowPosition.bind(this));
  }

  #setWindowPosition(event: DragEvent) {
    event.stopImmediatePropagation();
    event.preventDefault();

    const x = event.clientX;
    const y = event.clientY;

    if (x <= 0 || y <= 0) {
      return;
    }

    const imageX = x - this.#xCorrection;
    const imageY = y - this.#yCorrection;

    this.#windowElement!.style.left = `${imageX}px`;
    this.#windowElement!.style.top = `${imageY}px`;
  }

  #initResizer() {
    this.#resizer$!.addEventListener('dragstart', (event) => {
      const x = event.clientX;
      const y = event.clientY;

      const imageX = this.#windowElement!.offsetLeft;
      const imageY = this.#windowElement!.offsetTop;

      this.#xCorrection = x - imageX;
      this.#yCorrection = y - imageY;
    });

    this.#resizer$!.addEventListener('drag', this.#setWindowSize.bind(this));
  }

  #setWindowSize(event: DragEvent) {
    event.stopImmediatePropagation();
    event.preventDefault();

    const x = event.clientX;
    const y = event.clientY;

    if (x <= 0 || y <= 0) {
      return;
    }

    // width
    const leftEdge = this.#windowElement?.offsetLeft;
    const currentWidth = this.#windowElement?.clientWidth;
    const rightEdge = leftEdge! + currentWidth!;
    const deltaX = x - rightEdge;

    this.#windowElement!.style.width = `${
      this.#windowElement!.clientWidth + deltaX
    }px`;

    // height
    const topEdge = this.#windowElement?.offsetTop;
    const currentHeight = this.#windowElement?.clientHeight;
    const bottomEdge = topEdge! + currentHeight!;
    const deltaY = y - bottomEdge;

    this.#windowElement!.style.height = `${
      this.#windowElement!.clientHeight + deltaY
    }px`;
  }
}

customElements.define('ndm-window', WindowElement);
