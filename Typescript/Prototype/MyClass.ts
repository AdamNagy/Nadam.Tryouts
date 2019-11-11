class MyClass {

	Name: string;
	Age: Number;
	Text: string;

	constructor() {

	}

	public WithName(name: string): MyClass {
		this.Name = name;
		return this;
	} 

	public WithAge(age: number): MyClass {
		this.Age = age;
		return this;
	} 

	public WithText(text: string): MyClass {
		this.Text = text;
		return this;
	} 
}

function TestShit() {

	let obj = new MyClass();

	obj.WithName("Halika")
		.WithAge(31)
		.WithText("Some test text to seem this work");
}