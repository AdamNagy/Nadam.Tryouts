import { Injectable } from "@angular/core";

@Injectable()
export class MathOperations {

	add(a: number, b: number): number {
		return a + b;
	}

	square(a: number): number {
		return a * a;
	}

	cube(a: number): number {
		return this.square(a) * a;
	}
}