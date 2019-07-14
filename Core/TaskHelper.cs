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

        /// <summary>
        /// Lets you await any task with a timeout.
        /// </summary>
        /// <typeparam name="TResult">The type of the result from a given task.</typeparam>
        /// <param name="task">A given task.</param>
        /// <param name="timeout">A given timeout.</param>
        /// <returns>A task that is signaled upon completion.</returns>
        public static async Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            Task winner = await (Task.WhenAny(task, Task.Delay(timeout)));
            if (winner != task)
                throw new TimeoutException();
            return await task; // unwrap result/re-throw
        }

        /// <summary>
        /// Lets you abandon a task with a CancellationToken.
        /// </summary>
        /// <typeparam name="TResult">The type of the result from a given task.</typeparam>
        /// <param name="task">A given task.</param>
        /// <param name="ct">A given cancellation token.</param>
        /// <returns></returns>
        public static Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<TResult>();
            var reg = ct.Register(() => tcs.TrySetCanceled());
            task.ContinueWith(antecedentTask =>
            {
                reg.Dispose();
                if (antecedentTask.IsCanceled)
                    tcs.TrySetCanceled();
                else if (antecedentTask.IsFaulted)
                    tcs.TrySetException(antecedentTask.Exception.InnerException);
                else
                    tcs.TrySetResult(antecedentTask.Result);
            });

            return tcs.Task;
        }

        /// <summary>
        /// Works like WhenAll, except that if any of the tasks faultm
        /// the resultant task faults immediately.
        /// </summary>
        /// <typeparam name="TResult">The of the result of the given tasks.</typeparam>
        /// <param name="tasks">The given tasks.</param>
        /// <returns>A task that is signled upon completion or fault.</returns>
        public static async Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
        {
            var killJoy = new TaskCompletionSource<TResult[]>();
            foreach (var task in tasks)
                task.ContinueWith(antecedentTask =>
                {
                    if (antecedentTask.IsCanceled)
                        killJoy.TrySetCanceled();
                    else if (antecedentTask.IsFaulted)
                        killJoy.TrySetException(antecedentTask.Exception.InnerException);
                });
            return await await Task.WhenAny(killJoy.Task, Task.WhenAll(tasks));
        }
    }
}
