var burger: string = 'hamburger',     // String 
    calories: number = 300,           // Numeric
    tasty: Boolean = true;            // Boolean

// Alternatively, you can omit the type declaration:
// var burger = 'hamburger';

// The function expects a string and an integer.
// It doesn't return anything so the type of the function itself is void.

function say(food: string, energy: number): void {
  console.log("Our " + food + " has " + energy + " calories.");
}

// speak(burger, calories);

console.log("Hello world");

var tastyer: boolean = true;

var arr: Array<Number> = new Array<Number>();
arr.push(1);
arr.push(2);
arr.push(3);

let fullName: string = 'Bob Bobbington';
let age: number = 37;
let sentence: string = 'Hello, my name is ${ fullName }.';

// Array
let list: number[] = [1, 2, 3];
let list2: Array<number> = [1, 2, 3];

// Tuple
// Declare a tuple type
let x: [string, number];

// Initialize it
x = ["hello", 10];

// Initialize it incorrectly
// x = [10, "hello"]; // Error