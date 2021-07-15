import { Observable } from "rxjs-compat";
import { Subject } from "rxjs/Subject";
import { map, tap } from "rxjs/operators";
import { xorWith } from "lodash";

function $id(id: string): HTMLElement {
	return document.getElementById(id);
}

function $class(className: string): HTMLCollection {
	return document.getElementsByClassName(className);
}

const buttonClicked = Observable.fromEvent($id("the-button"), "click").pipe(
	tap(() => console.log("btn click observable pipe")),
	map(() => ($id("input") as HTMLInputElement).value)
);

buttonClicked.subscribe((value: string) => console.log(`observable subscriber 1 ${value}`));
buttonClicked.subscribe((value) => console.log(`observable subscriber 2 ${value}`));

const subject = new Subject<any>();
subject.pipe(
	tap(() => console.log("subject 1 pipe"))
).subscribe((value: string) => console.log(`subject 1 subscription ${value}`));

subject.subscribe((value: string) => console.log(`subject 2 subscriber ${value}`));

buttonClicked.subscribe(subject);

let index = 2;
Observable.fromEvent($id("the-button-2"), "click").subscribe(() => {
	subject.subscribe((value: string) => console.log(`subject subscription ${++index} ${value}`));
});

/******************************************************/

var arr: number[] = [1,2,3];
var promise: Promise<number[]>;
promise = Promise.resolve(arr);


$id("late-button").addEventListener("click", () => {
	arr = [1,2,3];
	promise = Promise.resolve(arr);
	promise.then((val: number[]) => console.log(`from first ${val}`));
});

var idx = 1;
$id("add-listener").addEventListener("click", () => {
	promise.then((val: number[]) => console.log(`from ${++idx} ${val}`));	
});

$id("add-number").addEventListener("click", () => {
	arr.push(++idx);
});