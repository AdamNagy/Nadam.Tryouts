import { CreateStyleElement } from '../../style-utils/css-rule.js';
// @ts-expect-error wer
import { jarallax } from './jarallax.esm.min.js';

export class ParallaxElement extends HTMLElement {
  static observedAttributes = ['height', 'src'];

  #container: HTMLDivElement = document.createElement('div');
  #image: HTMLImageElement = document.createElement('img');

  connectedCallback() {
    const root = this.attachShadow({ mode: 'open' });

    // @ts-expect-error wer
    this.#container.setAttribute('data-jarallax', true);
    this.#container.setAttribute('data-speed', '0.2');
    this.#container.classList.add('jarallax');
    this.#container.classList.add('ndm-jarallax');
    this.#container.style.width = '100%';

    console.log('jarallax inited');

    const style = document.createElement('style');
    style.textContent = `
      .jarallax {
        position: relative;
        z-index: 0;
      }

      .jarallax > .jarallax-img {
        position: absolute;
        object-fit: cover;
        /* support for plugin https://github.com/bfred-it/object-fit-images */
        font-family: 'object-fit: cover;';
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: -1;
      }
    `;

    const jarallaxStyle = CreateStyleElement(
      'https://cdn.jsdelivr.net/npm/jarallax@2/dist/jarallax.min.css'
    );

    this.#container.appendChild(this.#image);

    root.appendChild(jarallaxStyle);
    root.appendChild(style);
    root.appendChild(this.#container);

    jarallax(this.#container, {
      speed: 0.2,
    });
  }

  attributeChangedCallback(name: string, oldValue: string, newValue: string) {
    switch (name) {
      case 'height': {
        this.#container.style.height = `${newValue}px`;
        break;
      }

      case 'src': {
        this.#image.classList.add('jarallax-img');
        this.#image.setAttribute('src', newValue);
      }
    }
  }
}

customElements.define('ndm-parallax', ParallaxElement);
