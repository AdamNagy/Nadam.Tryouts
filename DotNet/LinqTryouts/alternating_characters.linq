<Query Kind="Statements" />

Func<int, string> alternator = (int n) => String.Join("", Enumerable.Range(1, n).Select(p => p % 2 == 0 ? '-' : '+'));

Console.WriteLine(alternator(5));
Console.WriteLine(alternator(4));