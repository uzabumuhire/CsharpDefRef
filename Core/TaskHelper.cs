using System;
using System.Threading;
using System.Threading.Tasks;

using static Core.ConsoleHelper;

namespace Core
{
    public static class TaskHelper
    {
        public static void DisplayCurrentTaskInfo(string message)
        {
            DisplayTaskInfo(message + " " + Task.CurrentId);
        }

        public static void DisplayCurrentTaskInfo(string message, int? taskId)
        {
            DisplayTaskInfo(message + " " + taskId);
        }

        /// <summary>
        /// Run the given <paramref name="function"/> on a nonpooled thread.
        /// Calling this method is equivalent to calling <c>Task.Factory.StartNew</c>
        /// with the <c>TaskCreationOptions.LongRunning</c> option to request
        /// a nonpooled thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the returned task's result.</typeparam>
        /// <param name="function">The function that resturns a <typeparamref name="TResult"/>.</param>
        /// <returns>A task slave to use for continuation.</returns>
        public static Task<TResult> Run<TResult> (Func<TResult> function)
        {
            TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();

            new Thread(() =>
            {
                try
                {
                    // Signals the task to put it
                    // into a completed state.
                    tcs.SetResult(function());
                }
                catch (Exception ex)
                {
                    // Signals the task to put it
                    // into a faulted state.
                    tcs.SetException(ex);
                }
            })
            .Start();

            return tcs.Task;
        }

        public static Task Delay(int milliseconds)
        {
            // To get rid of the task return value
            // the method returns a `Task`instead of
            // a `Task<int>`.

            // However, there is no nongeneric version
            // of `TaskCompletionSource`, which means
            // that we can't directly create
            // a non generic version of `Task`.

            // Since `Task<TResult>` derives from `Task`
            // we create a `TaskCompletionSource<anything>`
            // and then implicitly convert the Task<anything>
            // that it gives us into a `Task`. By returning
            // a `Task<int>` we convert it into the `Task`.
            var tcs = new TaskCompletionSource<object>();

            var timer = new System.Timers.Timer(milliseconds) { AutoReset = false };
            timer.Elapsed += delegate
            {
                timer.Dispose();
                tcs.SetResult(null);
            };
            timer.Start();

            
            return tcs.Task;

        }
    }
}
