using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using static System.Math;
using static System.Console;


using static Core.MatrixHelper;
using static Core.ConsoleHelper;
using static Core.NetworkHelper;
using static Core.DiagnosticHelper;

namespace ConcurrencyAsynchrony.AsynchronousProgramming.Patterns
{
    static class Demo
    {
        internal static async Task TestAsync()
        {
            // Cancellation asynchronous pattern

            // It is often important to be able to cancel a concurrent
            // operation after it's started, perhaps in response to
            // a user request.
            try
            {
                CustomCancellationToken cct = new CustomCancellationToken();
                var task = CancellableAsynchronousMethod(cct);
                //await Task.Delay(2000);

                // Calls `Cancel` on the cancellation
                // token that it passed to the method
                // to cancel the above operation. This
                // sets `IsCancellationRequested` to
                // true, which causes the operation
                // to fault a short time later with
                // an `OperationCanceledException`
                // a predefined exception designed
                // for this purpose).
                cct.Cancel();

                await task;
            }
            catch(OperationCanceledException ex)
            {
                WriteLine();
                DisplayWarning(ex.Message);
                WriteLine();
            }

            // The CLR provides a type called `CancellationToken` which
            // is very similar to what we have implemented. However, it
            // lacks a `Cancel` method which is exposed on another type
            // called `CancellationTokenSource`. This separation provides
            // some security : a method that has access only to a
            // `CancellationToken` object can check for but not *initiate*
            // cancellation.
            try
            {
                CancellationTokenSource cs = new CancellationTokenSource();
                var task = CancellableAsynchronousMethod(cs.Token);

                // The source of the Token, is the
                // only one that can cancel it.
                cs.Cancel();

                await task;
            }
            catch (OperationCanceledException ex)
            {
                WriteLine();
                DisplayWarning(ex.Message);
                WriteLine();
            }

            try
            {
                CancellationTokenSource cs = new CancellationTokenSource();
                var task = CancellableDelayMethod(cs.Token);

                // The source of the Token, is the
                // only one that can cancel it.
                cs.Cancel();

                await task;
            }
            catch (OperationCanceledException ex)
            {
                WriteLine();
                DisplayWarning(ex.Message);
                WriteLine();
            }

            try
            {
                // Implementing timeout

                // From Framework 4.5, you can specify a time interval when
                // constructing CancellationTokenSource to initiate
                // cancellation after a set period of time.
                CancellationTokenSource cs = new CancellationTokenSource(5000);
                var task = CancellableAsynchronousMethod(cs.Token);
                WriteLine("Started");

                // The source of the Token, is the
                // only one that can cancel it.
                cs.Cancel();

                await task;
            }
            catch (OperationCanceledException ex)
            {
                WriteLine();
                DisplayWarning(ex.Message);
                WriteLine();
            }

            // Progress reporting pattern.

            // Simple progress reporting with an `Action<T>` delegate
            Action<int> simpleProgress = i => DisplayInfo(i + " %");
            await SimpleProgressReporting(simpleProgress);
            WriteLine();

            // Progress reporting with `IProgress<T>` interface and
            // `Progress<T>` class that implements `IProgress<T>`.
            // Their purpose is to wrap a delegate, so that UI app
            // can report progress safely through the synchronization
            // context.

            // `Progress<T>` has a constructor that accepts a delegate
            // of type Action<T> that it wraps. When `ProgressReporting`
            // calls `Report`, the delegate is invoked through that
            // context.
            Progress<int> progress = new Progress<int>(i => DisplayInfo(i + " %"));
            await ProgressReporting(progress);
            

            // Task combinators

            // A nice consequence of there being a consistent protocol
            // for asynchronous functions (whereby they consistenly
            // return tasks) is that it's possible to use and write
            // task combinators - functions that usefully combine tasks,
            // without regard for what those specific tasks do.

            // The CLR includes two task combinators : `Task.WhenAny` and
            // `Task.WhenAll`

            // WhenAny

            // `Task.WhenAny` returns a task that completes when any one
            // of a set of tasks complete. Because `Task.WhenAny` itself
            // returns a task we can await it, which returns the task
            // finishes first. This is entirely nonblocking - including
            // the last line when we access the `Result` property because
            // `winningTask` will already have finished. Nonethelss,
            // it's usually better to await the `winningTask`, because any
            // exception are then re-thrown without an `AggregateException`
            // wrapping.
            Task<int> winningTask = await Task.WhenAny(Delay1(), Delay2(), Delay3());
            WriteLine();
            WriteLine("Done");
            WriteLine("Winner is Delay" + winningTask.Result);
            WriteLine("Winner is Delay" + await winningTask);

            // We can perform both awaits in one step.
            WriteLine("Winner is Delay" + await await Task.WhenAny(Delay1(), Delay2(), Delay3()));
            WriteLine();

            // If a nonwinning task subsequently faults, the exception
            // will go unobserved unless you subsequently await the task
            // or query its `Exception` property.

            // When calling `WhenAny` with differently typed tasks, the
            // winner is reported as a plain `Task` (rather than a Task

            // `WhenAny` is useful for applying timeouts or cancellation
            // to operations that don't otherwise support it.
            
            try
            {
                CancellationTokenSource cts2 = new CancellationTokenSource();
                var cprTask = CancellableProgressReporting(progress, cts2.Token);
                Task winner = await Task.WhenAny(cprTask, Task.Delay(5000));
                if (winner != cprTask)
                {
                    cts2.Cancel();
                    throw new TimeoutException("Timeout - Progress reporting being cancelled ...");
                }
                await cprTask; // unwrap result or resume execution or re-throw.
            }
            catch (TimeoutException ex)
            {
                WriteLine();
                DisplayWarning(ex.Message);
                WriteLine();
            }

            // WhenAll

            // `Task.WhenAll` returns a task that completes when all the tasks
            // that you pass to it complete.

            // Completes after three seconds and demonstrates
            // the "fork/join" pattern.
            await Task.WhenAll(Delay1(), Delay2(), Delay3());

            // You could get a smilar result by awaiting `task1`, `task2` and `task3`
            // in turn rather than using `WhenAll`. The difference (apart from it
            // being less efficient by virtue  of requiring three awaits rather than
            // one), is that should `task1` fault, we will never get await `task2/task3`
            // and their exception will go unobserved. In fact, this is why they
            // relaxed the unobserved task exception behavior fron CLR 4.5 : it would
            // be confusing if, despite an exception handling block around the entire
            // following block, an exception from `task2` or `task3 could crash your
            // application sometime later when garbage collected in CLR 4.0.
            Task task1 = Delay1(), task2 = Delay2(), task3 = Delay3();
            await task1;
            await task2;
            await task3;

            // In contrast, `Task.WhenAll` doesn't complete until all the tasks have
            // completed - even when there's a fault. And if there are multiple
            // faults, their exceptions are combined into the combined task's 
            // `AggregateException` (this is when `AggregateException` actually 
            // becomes useful - should you be interested in all the exceptions, 
            // that is). Awaiting the combined task, however, throws only the first
            // exception, so to see all the exceptions you need to do the following.
            Task exceptionTask1 = Task.Run(() => { throw null; });
            Task exceptionTask2 = Task.Run(() => { throw null; });
            Task all = Task.WhenAll(exceptionTask1, exceptionTask2);
            try
            {
                await all;
            }
            catch
            {
                DisplayInfo("The total number of thrown exception(s) : " + all.Exception.InnerExceptions.Count);
            }

            Task<int> taskWithResult1 = Task.Run(() => 1);
            Task<int> taskWithResult2 = Task.Run(() => 2);
            int[] results = await Task.WhenAll(taskWithResult1, taskWithResult2);
            Display1DMatrix(results);
            WriteLine();

            // Performance optimizations
            try
            {
                string[] domainNames = "www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com".Split();
                string[] uris = UrisFromDomainNames(domainNames, HttpsUriFromDomainNameConverter);
                WriteLine("The total size of the downloaded pages is " + await GetTotalSizeAsync(uris) + " bytes");
                WriteLine("The total size of the downloaded pages is " + await GetTotalSizeWithCombinatorAsync(uris) + " bytes");
                WriteLine("The total size of the downloaded pages is " + await GetTotalSizeWithCombinatorEfficientAsync(uris) + " bytes");
                WriteLine("The total size of the downloaded pages is " + await GetTotalSizeWithCombinatorEfficientWithCacheAsync(uris) + " bytes");

                WriteLine();
            }
            catch (Exception ex)
            {
                DisplayError(ex.ToString());
            }
            
        }

        /// <summary>
        /// A cancellable asynchronous method.
        /// </summary>
        /// <param name="cct">A cancellation token used to cancel this method.</param>
        /// <returns>A task to signal upon completion or fault.</returns>
        static async Task CancellableAsynchronousMethod(CustomCancellationToken cct)
        {
            for (int i = 0; i < 10; i++)
            {
                DisplaySpaceVal(i);
                await Task.Delay(1000);
                cct.ThrowIfCancellationRequested();
            }
        }

        /// <summary>
        /// A cancellable asynchronous method.
        /// </summary>
        /// <param name="ct">A cancellation token used to cancel this method.</param>
        /// <returns>A task to signal upon completion or fault.</returns>
        static async Task CancellableAsynchronousMethod(CancellationToken ct)
        {
            for (int i = 0; i < 10; i++)
            {
                DisplaySpaceVal(i);
                await Task.Delay(1000);
                ct.ThrowIfCancellationRequested();
            }
        }

        /// <summary>
        /// A cancellable asynchronous method.
        /// </summary>
        /// <param name="ct">A cancellation token used to cancel this method.</param>
        /// <returns>A task to signal upon completion or fault.</returns>
        static async Task CancellableDelayMethod(CancellationToken ct)
        {
            for (int i = 0; i < 10; i++)
            {
                DisplaySpaceVal(i);

                // Most asynchronous in the CLR support cancellation tokens,
                // including `Delay`. By passing the cancellation token to
                // `Delay`, the task will end immediately upon request rather
                // than up to a second later. Notice that we no longer call
                // `ThrowIfCancelledRequested` because `Task.Delay` is doing
                // that for us. 
                await Task.Delay(1000, ct);
            }
        }

        /// <summary>
        /// Reports back progress as it's running.
        /// </summary>
        /// <param name="onProgressPercentChanged">A delegate that fires
        /// whenever progress changes.</param>
        /// <returns>A task to signal upon completion or fault.</returns>
        static Task SimpleProgressReporting(Action<int> onProgressPercentChanged)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Do something compute-bound (counting prime numbers).
                    ParallelEnumerable.Range(i * 1_000_000 + 2, 1_000_000).Count(p =>
                        Enumerable.Range(2, (int)Sqrt(p) - 1).All(n => p % n > 0));

                    // Report progress.
                    onProgressPercentChanged((i + 1) * 10);
                }
            });
        }

        /// <summary>
        /// Reports back progress as it's running.
        /// </summary>
        /// <param name="onProgressPercentChanged">An object to use to report
        /// back whenever progress changes.</param>
        /// <returns>A task to signal upon completion or fault.</returns>
        static Task ProgressReporting(IProgress<int> onProgressPercentChanged)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Do something compute-bound (counting prime numbers).
                    ParallelEnumerable.Range(i * 1_000_000 + 2, 1_000_000).Count(p =>
                        Enumerable.Range(2, (int)Sqrt(p) - 1).All(n => p % n > 0));

                    // Report progress.
                    onProgressPercentChanged.Report((i + 1) * 10);
                }
            });
        }

        /// <summary>
        /// Reports back progress as it's running.
        /// </summary>
        /// <param name="onProgressPercentChanged">An object to use to report
        /// back whenever progress changes.</param>
        /// <param name="ct">A cancellation token used to cancel this method.</param>
        /// <returns>A task to signal upon completion or fault.</returns>
        static Task CancellableProgressReporting(IProgress<int> onProgressPercentChanged, CancellationToken ct)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Cancel on demand.
                    ct.ThrowIfCancellationRequested();

                    // Do something compute-bound (counting prime numbers).
                    ParallelEnumerable.Range(i * 1_000_000 + 2, 1_000_000).Count(p =>
                        Enumerable.Range(2, (int)Sqrt(p) - 1).All(n => p % n > 0));

                    // Report progress.
                    onProgressPercentChanged.Report((i + 1) * 10);
                }
            });
        }

        static async Task<int> Delay1()
        {
            await Task.Delay(1000);
            return 1;
        }

        static async Task<int> Delay2()
        {
            await Task.Delay(2000);
            return 2;
        }

        static async Task<int> Delay3()
        {
            await Task.Delay(3000);
            return 3;
        }

        /// <summary>
        /// Gets the total size, in bytes, of the contents downloaded from the given uris.
        /// </summary>
        /// <param name="uris">The given uris</param>
        /// <returns>Returns a task that is signaled upon completion or fault.</returns>
        static async Task<int> GetTotalSizeAsync(string[] uris)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            IEnumerable<Task<byte[]>> downloadTasks = uris.Select(
                uri => new WebClient().DownloadDataTaskAsync(uri));

            int totalSize = 0;
            foreach (Task<byte[]> dt in downloadTasks)
            {
                byte[] content = await dt;
                totalSize += content.Length;
            }

            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);

            return totalSize;
        }

        /// <summary>
        /// Gets the total size, in bytes, of the contents downloaded from the given uris.
        /// </summary>
        /// <param name="uris">The given uris</param>
        /// <returns>Returns a task that is signaled upon completion or fault.</returns>
        static async Task<int> GetTotalSizeWithCombinatorAsync(string[] uris)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            IEnumerable<Task<byte[]>> downloadTasks = uris.Select(
                uri => new WebClient().DownloadDataTaskAsync(uri));
            byte[][] contents = await Task.WhenAll(downloadTasks);
            int totalSize = contents.Sum(c => c.Length);

            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);

            return totalSize;
        }

        /// <summary>
        /// Gets the total size, in bytes, of the contents downloaded from the given uris.
        /// </summary>
        /// <param name="uris">The given uris</param>
        /// <returns>Returns a task that is signaled upon completion or fault.</returns>
        static async Task<int> GetTotalSizeWithCombinatorEfficientAsync(string[] uris)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            IEnumerable<Task<int>> downloadTasks = uris.Select(async uri =>
                (await new WebClient().DownloadDataTaskAsync(uri)).Length);
            int[] contentLengths = await Task.WhenAll(downloadTasks);
            int totalSize = contentLengths.Sum();

            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);

            return totalSize;
        }

        /// <summary>
        /// Gets the total size, in bytes, of the contents downloaded from the given uris.
        /// </summary>
        /// <param name="uris">The given uris</param>
        /// <returns>Returns a task that is signaled upon completion or fault.</returns>
        static async Task<int> GetTotalSizeWithCombinatorEfficientWithCacheAsync(string[] uris)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            IEnumerable<Task<int>> downloadTasks = uris.Select(uri => GetWebPageSizeAsync(uri));
            int[] contentLengths = await Task.WhenAll(downloadTasks);
            int totalSize = contentLengths.Sum();

            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);

            return totalSize;
        }

        // The cache of "futures" (`Task<int>`) that caches the tasks of getting the size of page.
        static Dictionary<string, Task<int>> _sizeCache = new Dictionary<string, Task<int>>();

        /// <summary>
        /// Fetches a task used to get the  web page identified by a given URI.
        /// </summary>
        /// <param name="uri">A URI of the web page to fetch.</param>
        /// <returns>A task that is signaled upon completion of
        /// calculating the size the downloaded page.</returns>
        static Task<int> GetWebPageSizeAsync(string uri)
        {
            // Add thread-safety if there is no protection of
            // synchronization context. This does not hurt
            // concurrency because we are not locking for the
            // duration of downloading  and calculating the
            // size of a page, we are locking for the small 
            // duration of checking the cache, starting a new 
            // task if necessary and updating the cache
            // with that task.
            lock (_sizeCache)
            {
                // Should a completed task associated to a URI exist in 
                // the cache, the method returns an *already-signaled* 
                // task. This is referred to as *synchronous completion*.
                // Awaiting this task is cheap, thanks to the compiler
                // optimization through synchronous completion.

                // If we call this method repeatedly with the same URI
                // (Uniform Ressource Identifier), we are now guaranteed
                // to get the same `Task<int>` object and no multiple
                // redundant downloads. Subsequent calls to the same URI
                // asynchronously wait upon the result of the in-progress
                // request. This has the additional benefit of minimizing 
                // GC load.
                Task<int> downloadTask;
                if (_sizeCache.TryGetValue(uri, out downloadTask))
                {
                    return downloadTask;
                }

                // This method is not marked as `async` since we are directly
                // returning the task we obtain from calling
                // `GetWebPageSizeFromBytesAsync` method.
                _sizeCache[uri] = GetWebPageSizeFromBytesAsync(uri);

                return _sizeCache[uri];
            }
        }

        /// <summary>
        /// Calculates the size in bytes of a web page identified by a given URI.
        /// </summary>
        /// <param name="uri">A URI of the web page to fetch.</param>
        /// <returns>A task that is signaled upon completion of
        /// calculating the size the downloaded page.</returns>
        static async Task<int> GetWebPageSizeFromBytesAsync(string uri)
        {
            return (await new WebClient().DownloadDataTaskAsync(uri)).Length;
        }
    }
}
