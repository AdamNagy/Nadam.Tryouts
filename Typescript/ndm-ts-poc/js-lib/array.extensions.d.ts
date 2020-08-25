declare interface Array {
	First(predicate: any): any;

	Last(predicate: any): any;

	Skip(amount: number): Array;
	
	Take(amount: number): Array;

	Where(predicate: any): Array;

	Select(action: any): any;
}