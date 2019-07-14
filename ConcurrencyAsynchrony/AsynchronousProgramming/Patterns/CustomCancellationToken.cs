using System;
namespace ConcurrencyAsynchrony.AsynchronousProgramming.Patterns
{
    /// <summary>
    /// Encapsulates a cancellation flag used to cancel a concurrent
    /// operation after it's started, perhaps in response to a user
    /// request.
    /// </summary>
    class CustomCancellationToken
    {
        internal bool IsCancellationRequested
        {
            get;
            private set;
        }

        internal void Cancel()
        {
            IsCancellationRequested = true;
        }

        internal void ThrowIfCancellationRequested()
        {
            if (IsCancellationRequested)
                throw new OperationCanceledException();
        }
    }
}
