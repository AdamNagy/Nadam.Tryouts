<Query Kind="Program" />

void Main()
{
	var isRich = GreaterThan(300);
	var numbers = new List<int>(){ 100,120,130,140,230,340,430,420,450 };
	
	var richies = numbers.Where(isRich);
	Console.Write(richies);
}

Func<int, bool> GreaterThan(int than)
{
	return (int e) => e > than;
}

// Define other methods and classes here
