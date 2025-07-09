export type CssRule = {
  selector: string;
  [key: string]: string;
};

export function ToCssElement(css: CssRule[]): HTMLStyleElement {
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

export function CreateStyleElement(href: string): HTMLStyleElement {
  const style = document.createElement('style');
  style.setAttribute('rel', 'stylesheet');
  style.setAttribute('href', href);

  return style;
}
