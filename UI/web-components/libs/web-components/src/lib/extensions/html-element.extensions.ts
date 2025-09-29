// @ts-expect-error know what iam doing
HTMLElement.prototype.withAttribute = function (
  attrName: string,
  value: string
) {
  this.setAttribute(attrName, value);
  return this;
};
