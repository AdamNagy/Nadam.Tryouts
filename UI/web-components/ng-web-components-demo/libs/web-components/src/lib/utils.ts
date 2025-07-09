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

export function GetNumberValue(value: string): number {
  const regexp = /\d+/;
  const matches = value.match(regexp);

  if (matches?.length == 0) {
    return 0;
  }

  const number = matches?.[0] ?? '';

  if (!number) {
    return 0;
  }

  return parseInt(number, 10);
}
