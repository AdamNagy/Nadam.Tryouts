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
