export type CssRules = 'background' | 'transition';

export type Css = {
  selector: string;
  [key: string]: string;
};

export function ToCssElement(css: Css[]): HTMLStyleElement {
  const styleElement = document.createElement('style');

  let cssString = '';
  for (const section of css) {
    cssString += `${section.selector} {\n`;
    for (const rule of Object.keys(section).filter((p) => p !== 'selector')) {
      cssString += `\t${rule}: ${section[rule]};\n`;
    }

    cssString += '} \n';
  }

  styleElement.textContent = cssString;
  return styleElement;
}

export class SidePagerElement extends HTMLElement {
  pages: { id: number; element: HTMLElement }[] = [];
  domParser = new DOMParser();
  openPages = 0;
  nextPageId = -1;

  pageWidth = 720;
  spaceBetweenPages = 30;

  template = `
    <div class="side-page">
        <button nid='btn-close'>Close</button>
        <button nid='btn-remove'>Remove</button>
        <div class="side-page-opener"></div>
        <div class="side-page-content"></div>
    </div>`;

  #root?: ShadowRoot;

  // runs each time the element is added to the DOM
  connectedCallback() {
    this.#root = this.attachShadow({ mode: 'open' });

    // const pages = this.querySelectorAll('.side-page');
    // for (const page of this.pages) {
    //   const sidePage = this.createPageSkeleton();

    //   while (page.children.length > 0) {
    //     sidePage.querySelector('.side-page-content').append(page.children[0]);
    //   }
    //   page.remove();
    //   this.appendChild(sidePage);
    // }

    const styleDef: Css = {
      selector: '.side-page',
      transition: 'right 0.4s',
      position: 'fixed',
      height: '100%',
      background: 'rgba(100,100,100,0.5)',
      'border-left': '1px solid lightskyblue',
    };
    const styleBase = ToCssElement([styleDef]);
    this.#root.appendChild(styleBase);

    const style = document.createElement('style');
    style.textContent = `
        side-page-closed {
            background: rgb(100,100,100);
            opacity: 0.6;
            overflow-y: hidden;
        }

        .side-page-open {
            background-color: lightgray;
            opacity: 1;
        }

        .side-page-opener {
            height: 100%;
            width: 30px;
            float: left;
        }

        .side-page-content {
            overflow-y: scroll;
            height: 100%;
        }
    `;

    this.#root.appendChild(style);

    this.style.display = 'block';
    this.closeAll();
  }

  addPage(contentElement: HTMLElement) {
    const sidePage = this.createPageSkeleton();
    sidePage
      ?.querySelector('div[class=side-page-content]')
      ?.append(contentElement);

    this.#root?.appendChild(sidePage);
    this.closeAll();
  }

  createPageSkeleton() {
    const pageElement: HTMLElement | null = this.domParser
      .parseFromString(this.template, 'text/html')
      .querySelector('div:first-child');

    if (!pageElement) {
      throw 'template is malicios';
    }

    this.nextPageId++;
    pageElement.style.zIndex = `${11 + this.nextPageId}`;

    pageElement
      .querySelector("div[class='side-page-opener']")
      ?.addEventListener(
        'click',
        ((pageId) => () => {
          this.openPage(pageId);
        })(this.nextPageId)
      );

    pageElement.querySelector("button[nid='btn-close']")?.addEventListener(
      'click',
      ((pageId) => () => {
        this.closePage(pageId);
      })(this.nextPageId)
    );

    pageElement.querySelector("button[nid='btn-remove']")?.addEventListener(
      'click',
      ((pageId) => () => {
        this.removePage(pageId);
      })(this.nextPageId)
    );

    this.pages.push({ id: this.nextPageId, element: pageElement });

    return pageElement;
  }

  openPage(pageId: number) {
    const index = this.#getIndexByPageId(pageId);
    if (index < 0) return;

    const closedPagesWidth =
      (this.pages.length - 1 - index) * this.spaceBetweenPages;
    const openPages_buffer = index * this.spaceBetweenPages;

    let pageRightPosition = closedPagesWidth + openPages_buffer;

    for (let i = 0; i <= index; ++i) {
      const currentElement = this.pages[i];
      currentElement.element.style.right = pageRightPosition + 'px';
      currentElement.element.classList.add('side-page-open');
      currentElement.element.classList.remove('side-page-closed');
      pageRightPosition -= this.spaceBetweenPages;
    }

    document.body.style.overflowY = 'hidden';
    this.openPages++;
  }

  closePage(pageId: number) {
    const index = this.#getIndexByPageId(pageId);
    if (index < 0) return;

    console.log(`${pageId}:${index}`);

    const buffer = this.pages.length - index - 1;
    let pageRightPosition =
      buffer * this.spaceBetweenPages + this.spaceBetweenPages;
    for (let i = index; i < this.pages.length; ++i) {
      const actual = this.pages[i];
      actual.element.style.width = this.pageWidth + 'px';
      actual.element.style.right =
        '-' + (this.pageWidth - pageRightPosition) + 'px';

      actual.element.classList.remove('side-page-open');
      actual.element.classList.add('side-page-closed');
      pageRightPosition -= this.spaceBetweenPages;
    }

    this.openPages--;

    if (this.openPages <= 0) {
      document.body.style.overflowY = 'auto';
      this.openPages = 0;
    }
  }

  #getIndexByPageId(pageId: number): number {
    const actual = this.pages.find((item) => item.id === pageId);

    if (!actual) return -1;

    return this.pages.indexOf(actual);
  }

  removePage(pageId: number) {
    let index = this.#getIndexByPageId(pageId);
    if (index < 0) return;

    const toRemove = this.pages[index];
    toRemove.element.remove();
    this.pages.splice(index, 1);

    if (index > 0) this.openPage(--index);
  }

  closeAll() {
    this.closePage(this.pages[0].id);
  }
}

customElements.define('ndm-side-pager', SidePagerElement);
