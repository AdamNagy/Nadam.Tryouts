interface IMyFood {
    name: string;
    calories: number;

    GetName(): string;
}

class MyFood implements IMyFood {
    name: string;
    calories: number;

    GetName(): string {
        return "Hello, my name is ${ fullName }. I'll be ${ age + 1 } years old next month.";
    }
}
// ---------------------------------------------
interface Food {
    name: string;
    calories: number;
}

// We tell our function to expect an object that fulfills the Food interface. 
// This way we know that the properties we need will always be available.
function speak(food: Food): void{
  console.log("Our " + food.name + " has " + food.calories + " calories.");
}

// We define an object that has all of the properties the Food interface expects.
// Notice that types will be inferred automatically.
var ice_cream = {
  name: "ice cream", 
  calories: 200
}

speak(ice_cream);