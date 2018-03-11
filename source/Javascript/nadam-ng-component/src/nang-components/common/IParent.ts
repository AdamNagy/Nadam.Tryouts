import { Injectable } from "@angular/core";

Injectable();
export class Parent {

	child: any;
	addChild(_child: any): void {
		this.child = _child;
	}
}