using System.Threading.Channels;

namespace AsyncEnumerableResult;

public class AsyncEnumerableResult<T>
{
    private readonly Channel<Task<T>> _tasks;
    public bool HasMore { get; set; }
    public CancellationToken CancellationToken { get; set; }

    public AsyncEnumerableResult()
    {
        var options = new BoundedChannelOptions(10000)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _tasks = Channel.CreateBounded<Task<T>>(options);

        var cts = new CancellationTokenSource();
        CancellationToken = cts.Token;
    }

    public AsyncEnumerableResult(IEnumerable<Task<T>> tasks)
    {
        var options = new BoundedChannelOptions(10000)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _tasks = Channel.CreateBounded<Task<T>>(options);

        foreach (var task in tasks)
        {
            _tasks.Writer.TryWrite(task);
        }

        var cts = new CancellationTokenSource();
        CancellationToken = cts.Token;
    }

    public async ValueTask Add(Task<T> task, bool hasMore = true)
    {
        await _tasks.Writer.WriteAsync(task);

        HasMore = hasMore;
    }

    public void SignalEnd()
    {
        HasMore = false;
    }

    public async Task<T> Read()
    {
        var workItem = await _tasks.Reader.ReadAsync(CancellationToken);
        return workItem.Result;        
    }
}
