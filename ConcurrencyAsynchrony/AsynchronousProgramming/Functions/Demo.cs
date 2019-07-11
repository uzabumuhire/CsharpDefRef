using System;
using System.Net;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using static System.Math;
using static System.Console;

using static Core.DiagnosticHelper;

namespace ConcurrencyAsynchrony.AsynchronousProgramming.Functions
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of asynchronous functions.
        /// </summary>
        internal static async Task TestAsync()
        {
            int trackingId = TrackingId++;

            // C#'s *asynchronous functions*  with `async` and `await` 
            // keywords are essential for implementing asynchrony
            // without excessive complexity.

            // The `async` modifier tells the compiler to treat `await` as a
            // keyword rather than an identifier should an ambiguity arise
            // within that method (this ensures that code written prior
            // C# 5 that might use `await` as an identifier will still compile
            // without error). The `async` modifier can be applied only to
            // methods (and lambda expressions) that return void or a `Task` or
            // `Task<TResult>`

            // The `async` modifier is similar to the `unsafe` modifier in that
            // it has no effect on a method's signature or public metadata.
            // It affects only what happens *inside* the method. For this
            // reason, it makes no sense to use `async` in a interface.
            // However it is legal, for instance, to introduce `async` when
            // overriding virtual method, as long as you keep the signature
            // the same.

            // With any asynchronous function, you can replace the `void`
            // return type with a `Task` to make the method itself *usefully*
            // asynchronous (and *awaitable*). No further changes are required.

            // Notice that we don't explicitly return a task in the method
            // body. The compiler manufactures the task, which it signals
            // upon completion of the method (or upon an unhandled exception).
            // This makes it easy to create asynchronous call chains.

            // The compiler expands asynchronous functions that return tasks into 
            // code that leverages `TaskCompletionSource` to create a task that
            // it then signals or faults.

            // Whenever a task-running asynchronous method finishes, execution
            // jumps back to whoever awaited it (by virtue of a continuation).

            // Awaiting

            // The `await` keyword simplifies the attaching of
            // continuations as the compiler eliminates the
            // "plumbing" of asynchronous programming for us.
            //await DisplayPrimesCountAsync(GetPrimesCountAsync);
            //WriteLine();

            // Methods with the `async` modifier are called *asynchronous functions*
            // because they themselves are typically asynchronous. Because upon
            // encountering an `await` expression, execution (normally) returns to
            // the caller. But before returning, the runtime attaches a continuation
            // to the awaited task, ensuring that when a task completes, execution
            // jumps back into the method and continues where it left off. If the
            // task faults, its exception is rethrown, otherwise its return value is
            // assigned to the `await` expression.
            //await DisplayPrimesCountsAsync(GetPrimesCountAsync, 1_000_000, 10);
            //WriteLine();

            // Awaiting a non generic task is legal
            // and generates a void expression.
            await Task.Delay(5000);
            WriteLine("Five seconds have passed.");
            WriteLine();


            // Imperative looping constructs (for, foreach and so on),
            // do not mix well with continuations, because they rely
            // on the *current local state* of the method. The `async`
            // anf `await` keywords offer a solution that let you write
            // asynchronous code that has the same structure and
            // simplicity as synchronous code as well as eliminating
            // the "plumbing" of asynchronous programming. The real
            // power of `await` expressions is that they can appear
            // almost anywhere in code. Specifically, an `await`
            // expression can appear in place of any expression
            // (within an asynchronous function) except for inside
            // a `lock` expression, `unsafe` context or an
            // executable's entry point (main method, for C# version
            // up to 7.0).

            // Capturing local state

            // Upon first executing `GetPrimesCountAsync`, execution returns
            // to the caller (`TestAsync`) by virtue of the `await` expression.
            // When the method completes (or faults), execution resumes where
            // it left off, with the values of local variables and loop counters
            // preserved.

            // When we follow a pattern where we *await* every asynchronous
            // method right after calling it, we create a sequential flow with
            // no parallelism or overlapping execution within the call graph.
            // Each `await` expression creates a gap in execution, after which
            // the program resumes where it left off. Execution flow matches
            // the synchronous call graph.
            Stopwatch s1 = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s1.Elapsed.ToString(), trackingId);

            for (int i = 0; i < 10; i++)
                WriteLine(await GetPrimesCountAsync(i * 1_000_000 + 2, 1_000_000));

            DisplayCurrentMethodInfo(s1.Elapsed.ToString(), trackingId);
            WriteLine();


            Stopwatch s2 = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s2.Elapsed.ToString(), trackingId);

            for (int i = 9; i >= 0; i--)
                WriteLine(await GetPrimesCountAsync(i * 1_000_000 + 2, (i + 1) * 1_000_000));

            DisplayCurrentMethodInfo(s2.Elapsed.ToString(), trackingId);
            WriteLine();

            
            Stopwatch s3 = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s3.Elapsed.ToString(), trackingId);

            for (int i = 10; i > 0; i--)
                await WaitWithMessage(i * 1000, string.Format("Waited for {0} second(s)", i));

            DisplayCurrentMethodInfo(s3.Elapsed.ToString(), trackingId);
            WriteLine();

            // Parallelism

            // Calling asynchronous method without awaiting it allows the code the follows
            // to execute in parallel.
            Stopwatch s11 = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(" - GetPrimesCountAsync1 : " + s11.Elapsed.ToString(), trackingId);
            int lastIteration1 = 0;
            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                var awaiter = GetPrimesCountAsync1(i * 1_000_000 + 2, 1_000_000).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    WriteLine(awaiter.GetResult());

                    if (++lastIteration1 == 10)
                    {
                        DisplayCurrentMethodInfo(" - GetPrimesCountAsync1 : " + s11.Elapsed.ToString(), trackingId);
                        WriteLine();
                    }
                    
                });
            }

            Stopwatch s21 = Stopwatch.StartNew();
            DisplayCurrentMethodInfo( " - GetPrimesCountAsync2 : " + s21.Elapsed.ToString(), trackingId);

            int lastIteration2 = 0;
            for (int i = 9; i >= 0; i--)
            {
                
                int temp = i;
                var awaiter = GetPrimesCountAsync2(i * 1_000_000 + 2, (i + 1) * 1_000_000).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    WriteLine(awaiter.GetResult());

                    if (++lastIteration2 == 10)
                    {
                        DisplayCurrentMethodInfo(" - GetPrimesCountAsync2 : " + s21.Elapsed.ToString(), trackingId);
                        WriteLine();
                    }
                });
            }

            Stopwatch s31 = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(" - WaitWithMessage1 : " + s31.Elapsed.ToString(), trackingId);

            int lastIteration3 = 0;
            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                var awaiter = WaitWithMessage(i * 1000, string.Format("Waited for {0} second(s)", i)).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    if (++lastIteration3 == 10)
                    {
                        DisplayCurrentMethodInfo(" - WaitWithMessage1 : " + s31.Elapsed.ToString(), trackingId);
                        WriteLine();
                    }
                    
                });
            }

            // By awaiting the operations afterward, we "end" parallelism at that point.
            // Concurrency created in this manner occurs whether or not the operation are
            // initiated on a UI thread, although there's a difference in how it occurs.
            // In both cases, we get the same "true" concurrency occuring in the
            // bottom-level operations that initiate it (such as `Task.Delay` or code
            // farmed to `Task.Run`). Methods above in the call stack will be subject to
            // true concurrency only if the operation was initiated without a
            // synchronization context present; otherwise they will be subject to the
            // pseudoconcurrency (and simplified thread safety), whereby the only places
            // we can be preempted is at an `await` statement.
            Stopwatch s4 = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(" - WaitWithMessage2 : " + s4.Elapsed.ToString(), trackingId);

            var task1 = WaitWithMessage(9000, "Waited for 9 seconds");
            var task2 = WaitWithMessage(5000, "Waited for 5 seconds");
            var task3 = WaitWithMessage(1000, "Waited for 1 second");

            await task1;
            await task2;
            await task3;

            DisplayCurrentMethodInfo(" - WaitWithMessage2 : " + s4.Elapsed.ToString(), trackingId);
            WriteLine();

            // Asynchronous lambda expressions

            // Unamed methods (lambda expressions and anonymous methods)
            // can be asynchronous if preceded by the `async` keyword.
            // An asynchronous lambda expression can be used when attaching
            // event handlers. Asynchronous lambda expressions can also
            // return a `Task<TResult>`.
            Func<Task> delayAsyncLambdaExpression = async () =>
            {
                await Task.Delay(3000);
                WriteLine("Waited for 3 seconds");
            };
            await delayAsyncLambdaExpression();

            Func<Task<string>> nonVoidDelayAsyncLambdaExpression = async () =>
            {
                await Task.Delay(2000);
                return "Waited for 2 seconds";
            };
            WriteLine(await nonVoidDelayAsyncLambdaExpression());
            WriteLine();

            // Optmization through completing synchronously.
            string[] urls = "www.facebook.com www.google.com www.linkedin.com www.twitter.com www.instagram.com www.facebook.com www.google.com www.linkedin.com".Split();
            await DownloadWebPagesSequentialAsync(urls);
            WriteLine();

            _cache = new Dictionary<string, Task<string>>();
            await DownloadWebPagesParallelAsync(urls);
        }

        /// <summary>
        /// Computes and counts prime numbers from a given start index in
        /// natural numbers in an interval of a given length.
        /// </summary>
        /// <param name="startIndex">A given start index in a given interval.</param>
        /// <param name="intervalLength">A given interval length.</param>
        /// <returns>A task that is signaled upon completion of the counting
        /// and that contains the result.</returns>
        static Task<int> GetPrimesCountAsync(int startIndex, int intervalLength)
        {
            return Task.Run(() =>
            {
                int trackingId = TrackingId++;
                Stopwatch s = Stopwatch.StartNew();
                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);
                 
                int count = ParallelEnumerable.Range(startIndex, intervalLength).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);

                return count;
            });
        }

        static Task<int> GetPrimesCountAsync1(int startIndex, int intervalLength)
        {
            return Task.Run(() =>
            {
                int trackingId = TrackingId++;
                Stopwatch s = Stopwatch.StartNew();
                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

                int count = ParallelEnumerable.Range(startIndex, intervalLength).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);

                return count;
            });
        }

        static Task<int> GetPrimesCountAsync2(int startIndex, int intervalLength)
        {
            return Task.Run(() =>
            {
                int trackingId = TrackingId++;
                Stopwatch s = Stopwatch.StartNew();
                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

                int count = ParallelEnumerable.Range(startIndex, intervalLength).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);

                return count;
            });
        }

        /// <summary>
        /// Uses an asynchronous function to counts and displays prime numbers
        /// in the first one million natural numbers.
        /// </summary>
        /// <param name="countPrimeNumbersAsync">An asynchronous method used
        /// to count prime numbers</param>
        static async Task DisplayPrimesCountAsync(Func<int, int, Task<int>> countPrimeNumbersAsync)
        {
            int count = await countPrimeNumbersAsync(2, 1_000_000);
            WriteLine("There are {0} primes between {1} and {2}", count, 0, 1_000_000);
        }

        /// <summary>
        /// Counts and displays prime numbers starting from a given index in natural
        /// numbers in a interval of given length, then continues to the next
        /// interval of the same length and so on by repeating this process
        /// in all of the given total number of intervals. Displays also
        /// the total count of prime numbers in all of the given 
        /// total number of intervals.
        /// </summary>
        /// <param name="countPrimeNumbersAsync">An asynchronous method used
        /// to count prime numbers in a given interval.</param>
        /// <param name="intervalLength">The given length of the interval.</param>
        /// <param name="totalIntervals">The total number of intervalls.</param>
        /// <returns>A task that it signals upon completion</returns>
        static async Task DisplayPrimesCountsAsync(
            Func<int, int, Task<int>> countPrimeNumbersAsync,
            int intervalLength,
            int totalIntervals)
        {
            int totalCount = 0;
            int count;

            for (int i = 0; i < totalIntervals; i++)
            {
                int begin = i * intervalLength;
                int end = (i + 1) * intervalLength - 1;

                // The `async` and `await` keywords, let you write asynchronous
                // code that has the same structure and simplicity as synchronous
                // code, as well as eliminating the "plumbing" of asynchronous
                // programming.

                // The `await` keyword simplifies the attaching of continuations.
                count = await countPrimeNumbersAsync(i * intervalLength + 2, intervalLength);
                totalCount += count;
                WriteLine("There are {0} primes between {1} and {2}", count, begin, end);
            }

            WriteLine(
                "There are a total of {0} between 0 and {1} ",
                totalCount,
                totalIntervals * intervalLength - 1);
        }

        static async Task WaitWithMessage(int milliseconds, string message)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            await Task.Delay(milliseconds);
            WriteLine(s.Elapsed.ToString() + " : " + message);

            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);
        }

        /// <summary>
        /// Download the web pages contained in a given array of urls.
        /// </summary>
        /// <param name="urls">A given array of urls</param>
        /// <returns>A task that is signaled upon completion.</returns>
        static async Task DownloadWebPagesSequentialAsync(string[] urls)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            try
            {
                foreach (string url in urls)
                {
                    var uri = new Uri("https://" + url);
                    string html = await GetWebPageAsync(uri.ToString());
                    WriteLine("The content length of the web page " + url + " is " + html.Length);
                }
            }
            catch (WebException ex)
            {
                WriteLine(ex.Message);
            }
            finally
            {
                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);
            }
        }

        /// <summary>
        /// Download the web pages contained in a given array of urls.
        /// </summary>
        /// <param name="urls">A given array of urls</param>
        /// <returns>A task that is signaled upon completion.</returns>
        static async Task DownloadWebPagesParallelAsync(string[] urls)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            try
            {
                Task<string>[] downloadTasks = new Task<string>[urls.Length];
                int i = 0;
                foreach (string url in urls)
                {
                    var uri = new Uri("https://" + url);
                    downloadTasks[i++] = GetWebPageAsync(uri.ToString());
                }

                int j = 0;
                foreach (var dt in downloadTasks)
                {
                    string html = await dt;
                    WriteLine("The content length of the web page " + urls[j++] + " is " + html.Length);
                }
            }
            catch (WebException ex)
            {
                WriteLine(ex.Message);
            }
            finally
            {
                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);
            }
        }

        // The cache of "futures" (`Task<string>`) that caches the tasks of downloading pages.
        static Dictionary<string, Task<string>> _cache = new Dictionary<string, Task<string>>();

        /// <summary>
        /// Fetches a task used to get the content web page identified by a given URI.
        /// </summary>
        /// <param name="uri">A URI of the web page to fetch.</param>
        /// <returns>A task that is signaled upon completion of downloading 
        /// the web page and that contains the downloaded content.</returns>
        static Task<string> GetWebPageAsync(string uri)
        {
            int trackingId = TrackingId++;
            Stopwatch s = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - entering", trackingId);

            // Add thread-safety if there is no protection of
            // synchronization context. This does not hurt
            // concurrency because we are not locking for the
            // duration of downloading a page, we are locking
            // for the small duration of checking the cache,
            // starting a new task if necessary and updating
            // the cache with that task.
            lock (_cache)
            {
                // Should a completed task associated to a URI exist in 
                // the cache, the method returns an *already-signaled* 
                // task. This is referred to as *synchronous completion*.
                // Awaiting this task is cheap, thanks to the compiler
                // optimization through synchronous completion.

                // If we call this method repeatedly with the same URI
                // (Uniform Ressource Identifier), we are now guaranteed
                // to get the same `Task<string>` object and no multiple
                // redundant downloads. Subsequent calls to the same URI
                // asynchronously wait upon the result of the in-progress
                // request. This has the additional benefit of minimizing 
                // GC load.
                Task<string> downloadTask;
                if (_cache.TryGetValue(uri, out downloadTask))
                {
                    DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);
                    return downloadTask;
                }

                // This method is not marked as `async` since we are directly
                // returning the task we obtain from calling `WebClient`'s
                // method.
                _cache[uri] = new WebClient().DownloadStringTaskAsync(uri);
                DisplayCurrentMethodInfo(s.Elapsed.ToString() + " - exiting", trackingId);
                return _cache[uri];
            }
        }
    }
}
