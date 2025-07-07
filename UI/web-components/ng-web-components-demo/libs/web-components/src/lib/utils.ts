export function CreateStyleElement(href: string): HTMLStyleElement {
  const style = document.createElement('style');
  style.setAttribute('rel', 'stylesheet');
  style.setAttribute('href', href);

  return style;
}

export function CreateScriptElement(src: string): HTMLScriptElement {
  const script = document.createElement('script');
  script.setAttribute('src', src);

  return script;
}
