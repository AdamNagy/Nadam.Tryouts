import { CssRule, ToCssElement } from '../../style-utils/css-rule';

export class SidePagerElement extends HTMLElement {
  pages: { id: number; element: HTMLElement }[] = [];
  domParser = new DOMParser();
  openPages = 0;
  nextPageId = -1;

  pageWidth = 720;
  spaceBetweenPages = 30;

  template = `
    <div class="side-page">
      <div class="side-page-header">
        <span>[Title]</span>
        <section>
          <button nid='btn-close'>Close</button>
          <button nid='btn-remove'>Remove</button>
        </section>
      </div>
        <div class="side-page-opener"></div>
    </div>`;

  connectedCallback() {
    this.#addStyle();

    this.querySelectorAll('div').forEach((page) => {
      page.remove();
      this.addPage.bind(this)(page as HTMLElement);
    });

    if (this.pages.length > 0) {
      this.closeAll();
    }
  }

  addPage(contentElement: HTMLElement) {
    const sidePage = this.#createPageSkeleton();
    sidePage?.append(contentElement);

    this.appendChild(sidePage);
    this.closeAll();
  }

  #createPageSkeleton() {
    const pageElement: HTMLElement | null = this.domParser
      .parseFromString(this.template, 'text/html')
      .querySelector('div:first-child');

    if (!pageElement) {
      throw 'template is malicios';
    }

    this.nextPageId++;
    pageElement.style.zIndex = `${101 + this.nextPageId}`;

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
    if (!this.pages || this.pages.length === 0) {
      return;
    }

    this.closePage(this.pages[0].id);
  }

  #addStyle() {
    const sidePage_StyleDef: CssRule = {
      selector: 'ndm-side-pager div.side-page',
      transition: 'right 0.4s',
      position: 'fixed',
      top: '0',
      height: '100%',
      background: 'rgba(100,100,100,0.5)',
      'border-left': '1px solid lightskyblue',
    };

    const sidePageClosed_StyleDef: CssRule = {
      selector: 'div.side-page-closed',
      background: 'rgb(100,100,100)',
      opacity: '0.6',
      'overflow-y': 'hidden',
    };

    const sidePageOpen_StyleDef: CssRule = {
      selector: 'div.side-page-open',
      'background-color': 'lightgray',
      opacity: '1',
    };

    const sidePageOpener_StyleDef: CssRule = {
      selector: '.side-page-opener',
      height: '100%',
      width: '30px',
      float: 'left',
    };

    const sidePageContent_StyleDef: CssRule = {
      selector: '.side-page-content',
      'overflow-y': 'scroll',
      height: '100%',
    };

    const sidePagerHeader_StyleDef: CssRule = {
      selector: '.side-page-header',
      display: 'flex',
      'justify-content': 'space-between',
      padding: '10px 10px 10px 40px',
      'background-color': 'lightgray',
    };

    const styleBase = ToCssElement([
      sidePage_StyleDef,
      sidePageClosed_StyleDef,
      sidePageOpen_StyleDef,
      sidePageOpener_StyleDef,
      sidePageContent_StyleDef,
      sidePagerHeader_StyleDef,
    ]);
    this.appendChild(styleBase);
    this.style.display = 'block';
    this.style.position = 'relative';
    this.style.top = '0';
  }
}

customElements.define('ndm-side-pager', SidePagerElement);
