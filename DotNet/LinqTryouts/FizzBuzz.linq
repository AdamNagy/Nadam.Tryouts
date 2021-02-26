<Query Kind="Program" />

void Main()
{
	for(var i = 1; i < 20; ++i)
	{
		var result = "";
		
		if(i % 3 == 0 || i % 5 == 0) {
			if(i % 3 == 0)
				result = $"Fizz ({i}) ";
			
			if(i % 5 == 0)
				result += $"Buzz ({i}) ";
		} else {
			result = $"{i}";
		}
		Console.Write($"{result} ");
	}
}

// Define other methods and classes here
