// in this context array is not an object
declare function IsObject(variable: any);

// convert an object to an array using the values only
declare function ObjectToArray(obj: any): Array<any>

declare function CamelCaseToDashed(word: string): string;

declare function IsHTMLElementProperty(word): boolean

declare function Throttle(func, limit);

declare function IsElement(element: any): boolean;
