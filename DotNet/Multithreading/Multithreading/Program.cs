// See https://aka.ms/new-console-template for more information
using Multithreading;

var testDataGenerator = new TestDataGenerator(
    @"C:\Users\Adam_Nagy1\Documents",
    "test-urls.txt",
    "test-data");

// testDataGenerator.GenerateTestFile();


var tcs = Get();
Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
tcs.ContinueWith(p => Console.WriteLine($"Continuation: ({Thread.CurrentThread.ManagedThreadId}){p.Result}"));

string input = Console.ReadLine();
while(input.ToLower() != "exit")
{
    Console.WriteLine($"Wrote: {input} // {Thread.CurrentThread.ManagedThreadId}");
    input = Console.ReadLine();
}

static Task<int> Get()
{
    var tcs = new TaskCompletionSource<int>();

    Task.Factory.StartNew(() => { Thread.Sleep(3000); tcs.SetResult(42); });

    return tcs.Task;
}