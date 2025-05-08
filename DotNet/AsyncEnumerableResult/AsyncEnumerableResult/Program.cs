// See https://aka.ms/new-console-template for more information

using System.Reflection.Metadata;

var consleRead = Console.ReadLine();

var result = new AsyncEnumerableResult.AsyncEnumerableResult<string>();

Task.Factory.StartNew(async () =>
{
    while (true)
    {
        var item = await result.Read();
        Console.WriteLine(item);

    }
    
}).ConfigureAwait(false);

Console.WriteLine("start tiping");
while(consleRead.ToLower() != "exit")
{
    await result.Add(Task.FromResult(consleRead));

    consleRead = Console.ReadLine();
}

