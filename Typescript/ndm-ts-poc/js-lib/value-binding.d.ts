// this will return a function which able to update the 'value' with given parameter
// or just simply returns it
// val: the initial value of the property (string, number, object, array)
// boundElement: and HTMLElement which will be updated when value changes
// attrName: this is the name of the attribute of the previous HTMLElement that will hold the value as string
//   default is 'innerText'
declare function Property(val, boundElement, attrName): any;

// this returns a function as well, calling it will update the DOM wit the given values
// element: the HTMLElement to bound value to
// attrName: the attribute name to put the value into
declare function Binding(element, attrName): any;

// this could go to html-element.extension but logically belongs to this file
declare interface HTMLElement {

	BindValue(event, action): HTMLElement;
}
