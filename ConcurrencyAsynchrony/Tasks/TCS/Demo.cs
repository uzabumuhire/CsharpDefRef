using System.Threading;
using System.Threading.Tasks;

using static System.Console;

using static Core.TaskHelper;

namespace ConcurrencyAsynchrony.Tasks.TCS
{
    public class Demo
    {
        /// <summary>
        /// Demonstrates usage of a <see cref="TaskCompletionSource{TResult}"/>.
        /// </summary>
        internal static void Test()
        {
            UseCreatedTask();

            UseCustomRun();

            UseTaskWithTimer();

            UseCustomDelay(5000);

            //UseIntensivelyCustomDelay(5000, 10_000);
        }

        /// <summary>
        /// Demonstrates usages of a<see cref="Task{int}"/> created from a <see cref="TaskCompletionSource{int}"/>.
        /// </summary>
        static void UseCreatedTask()
        {
            // By attaching a continuation to the task,
            // we can write its result without blocking
            // *any* thread.
            var awaiter = CreateTask().GetAwaiter();
            awaiter.OnCompleted(() => WriteLine(awaiter.GetResult()));
        }

        /// <summary>
        /// Create a <see cref="Task{int}"/> from a <see cref="TaskCompletionSource{int}"/>.
        /// </summary>
        /// <returns>A created <see cref="Task{int}"/> slave.</returns>
        static Task<int> CreateTask()
        {
            // `TaskCompletionSource` is another way to create a task.
            // `TaskCompletionSource` let you create a task out of any
            // operation that starts and finishes some time later. It
            // works by giving you a slave task that you manually drive
            // by indicating when the operation finishes or faults or
            // cancels.  This is ideal for I/O-bound work, an operation
            // that spends mostof its time *waiting* for something to
            // happen (input/output or Threed.Sleep). You get all the
            // benefits of tasks (with their ability to propagate
            // return values, exceptions and continuations) without
            // blocking a thread for the duration of the operation.

            // To use `TaskCompletionSource` simply instantiate the class.
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

            new Thread(() =>
            {
                // Represents an I/O-bound work.
                Thread.Sleep(5000);

                // Signals the task to put it
                // into a completed state.
                tcs.SetResult(42);
            })
            {
                // Sets the thread as background.
                IsBackground = true
            }
            .Start();

            // `TaskCompletionSource` exposes a `Task` property
            // that returns a task upon which you can await and
            // attach continuations. The returned task is
            // controlled entirely by the `TaskCompletionSource`
            // object via its SetXXX and TrySetXXX methods which
            // signals the task when called.
            Task<int> task = tcs.Task; // slave task

            return task;
        }

        /// <summary>
        /// Demonstrates usage of a custom <see cref="Task{TResult}"/> factory
        /// <see cref="Run{TResult}(System.Func{TResult})"/>.
        /// </summary>
        static void UseCustomRun()
        {
            Task<int> task = Run(() =>
            {
                Thread.Sleep(5000);
                return 42;
            });
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() => WriteLine(awaiter.GetResult()));
        }

        /// <summary>
        /// Demonstrates continuation of a task created with a timer.
        /// </summary>
        static void UseTaskWithTimer()
        {
            // By attaching continuation to the task, we can write
            // its result without blocking *any* thread.
            var awaiter = TaskWithTimer().GetAwaiter();
            awaiter.OnCompleted(() => WriteLine(awaiter.GetResult()));
        }

        /// <summary>
        /// Creates a task that waits without using threads but timers.
        /// </summary>
        /// <returns>A <see cref="Task{int}"/> from the <see cref="TaskCompletionSource{int}"/>. </returns>
        static Task<int> TaskWithTimer()
        {
            // The real power of `TaskCompletionSource`
            // is in creating task that don't tie up
            // threads. 
            var tcs = new TaskCompletionSource<int>();

            // Creates a task that waits for five seconds
            // and then returns the number 42 without using
            // a thread but a `Timer` class, which with the
            // help of the CLR (and in turn, the operating
            // system) fires an event in x milleseconds.

            // Create a timer that fires once in 5000 ms.
            var timer = new System.Timers.Timer(5000) { AutoReset = false};
            timer.Elapsed += delegate
            {
                timer.Dispose();
                tcs.SetResult(42);
            };
            timer.Start();

            return tcs.Task;
        }

        static void UseCustomDelay(int milliseconds)
        {
            Delay(milliseconds).GetAwaiter().OnCompleted(() => WriteLine(42));
        }

        static void UseIntensivelyCustomDelay(int milliseconds, int totalRepeat)
        {
            // Use of `TaskCompletionSource` without thread means that
            // thread is engaged only when continuation starts.
            // We can start many `Delay` operations at once
            // without error or excessive ressource consumption.

            // Timers fire their callbacks on pooled threads, so after
            // the specified interval of `milliseconds`, the thread pool
            // will receive a number of `totalRepeat` requests to call
            // `SetResult(null)` on a `TaskCompletionSource`. If the
            // requests arrive faster than they can be processed, the
            // thread pool will respond by enqueuing and then processing
            // them at the optimum level of parallelism for the CPU. This
            // is ideal if the thread-bound job is merely the call to
            // `SetResult` plus either the action of posting the
            // continuation to the synchronization context (in a UI app)
            // or otherwise the continuation itself.
            for (int i = 0; i < totalRepeat; i++)
                Delay(milliseconds).GetAwaiter().OnCompleted(() => WriteLine(42));
        }
    }
}
