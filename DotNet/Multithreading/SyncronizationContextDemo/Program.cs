Console.WriteLine("Hello, World!");

var uiSyncContext = SynchronizationContext.Current;
// uiSyncContext.

Enumerable.Range(0, 3).Select(p => Task.Run(Job));

async Task Job()
{
    var random = new Random();
    Enumerable.Range(0, 5).Select(p => {
        Task.Delay(random.Next(10, 400));
        return p;
    });
}
