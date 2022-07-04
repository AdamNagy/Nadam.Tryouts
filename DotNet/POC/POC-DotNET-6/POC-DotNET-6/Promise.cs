namespace POC_DotNET_6
{
    public class Promise<TResult>
    {
        private readonly Action<Action<TResult>, Action<Exception>> _generator;

        public Promise(Action<Action<TResult>, Action<Exception>> generator)
        {
            _generator = generator;
        }

        public Task<TResult> Execute()
        {
            var tcs = new TaskCompletionSource<TResult>();

            _generator(tcs.SetResult, tcs.SetException);

            return tcs.Task;
        }
    }
}
