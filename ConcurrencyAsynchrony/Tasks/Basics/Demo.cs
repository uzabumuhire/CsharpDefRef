using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

using static System.Math;
using static System.Console;


using static Core.TaskHelper;
using static Core.ConsoleHelper;
using static Core.DiagnosticHelper;

namespace ConcurrencyAsynchrony.Tasks.Basics
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of tasks.
        /// </summary>
        internal static void Test()
        {
            /*
            StartTask();

            WaitTask();

            StartLongRunningTask();

            ReturnResultFromTask();

            HandleExceptionFromTask();
            */

            CountPrimeNumbersPooledTask(3_000_000);

            CountPrimeNumbersLongRunningTask(3_000_000);

            CountPrimeNumbersWithoutTask(3_000_000);

            ContinuationWithAwaiterLongRunningTask(3_000_000);

            ContinuationWithAwaiterPooledTask(3_000_000);

            ContinuationWithoutAwaiterLongRunningTask(3_000_000);

            ContinuationWithoutAwaiterPooledTask(3_000_000);

            /*
            Stopwatch s1 = Stopwatch.StartNew();
            CountPrimeNumbersPooledTask(10_000_000);
            CountPrimeNumbersPooledTask(10_000_000);
            CountPrimeNumbersPooledTask(10_000_000);
            CountPrimeNumbersPooledTask(10_000_000);
            DisplayCurrentMethodInfo(s1.Elapsed.ToString());
            */

            /*
            Stopwatch s2 = Stopwatch.StartNew();
            CountPrimeNumbersLongRunningTask(10_000_000);
            CountPrimeNumbersLongRunningTask(10_000_000);
            CountPrimeNumbersLongRunningTask(10_000_000);
            CountPrimeNumbersLongRunningTask(10_000_000);
            DisplayCurrentMethodInfo(s2.Elapsed.ToString());
            */

            /*
            Stopwatch s3 = Stopwatch.StartNew();
            CountPrimeNumbersWithoutTask(10_000_000);
            CountPrimeNumbersWithoutTask(10_000_000);
            CountPrimeNumbersWithoutTask(10_000_000);
            CountPrimeNumbersWithoutTask(10_000_000);
            DisplayCurrentMethodInfo(s3.Elapsed.ToString());
            */

            /*
            Stopwatch s4 = Stopwatch.StartNew();
            ContinuationWithAwaiterLongRunningTask(10_000_000);
            ContinuationWithAwaiterLongRunningTask(10_000_000);
            ContinuationWithAwaiterLongRunningTask(10_000_000);
            ContinuationWithAwaiterLongRunningTask(10_000_000);
            DisplayCurrentMethodInfo(s4.Elapsed.ToString());
            */

            /*
            Stopwatch s5 = Stopwatch.StartNew();
            ContinuationWithAwaiterPooledTask(10_000_000);
            ContinuationWithAwaiterPooledTask(10_000_000);
            ContinuationWithAwaiterPooledTask(10_000_000);
            ContinuationWithAwaiterPooledTask(10_000_000);
            DisplayCurrentMethodInfo(s5.Elapsed.ToString());
            */

            /*
            Stopwatch s6 = Stopwatch.StartNew();
            ContinuationWithoutAwaiterLongRunningTask(10_000_000);
            ContinuationWithoutAwaiterLongRunningTask(10_000_000);
            ContinuationWithoutAwaiterLongRunningTask(10_000_000);
            ContinuationWithoutAwaiterLongRunningTask(10_000_000);
            DisplayCurrentMethodInfo(s6.Elapsed.ToString());
            */

            /*
            Stopwatch s7 = Stopwatch.StartNew();
            ContinuationWithoutAwaiterPooledTask(10_000_000);
            ContinuationWithoutAwaiterPooledTask(10_000_000);
            ContinuationWithoutAwaiterPooledTask(10_000_000);
            ContinuationWithoutAwaiterPooledTask(10_000_000);
            DisplayCurrentMethodInfo(s7.Elapsed.ToString());
            */
        }

        /// <summary>
        /// Demonstrates how to start a task.
        /// </summary>
        static void StartTask()
        {
            // In Framework 4.0, you can start a `Task`
            // by calling `Task.Factory.StartNew`.

            // `Task.Run` is a shortcut for
            // `Task.Factory.StartNew`.

            // From Framework 4.5, the easiest way to
            // start a `Task` is with the static method
            // `Task.Run` by simply passing in an `Action`
            // delegate.
            Task.Run(() =>
            {
                DisplayCurrentTaskInfo("Entering");
                WriteLine("Task has started");
                DisplayCurrentTaskInfo("Exiting");
            });
        }

        /// <summary>
        /// Demonstrates how to wait for a task.
        /// </summary>
        static void WaitTask()
        {
            // Calling `Wait` on a task blocks until it completes
            // and is the equivalent of calling `Join` on a thread.

            // `Wait` let you optionally specify a timeout and
            // a cancellation token to end the wait early.
            Task task = Task.Run(() =>
            {
                DisplayCurrentTaskInfo("Entering");
                WriteLine("Started another task");
                Thread.Sleep(2000);
                WriteLine("Task finished");
                DisplayCurrentTaskInfo("Exiting");
            });
            WriteLine("Completed: " + task.IsCompleted);
            task.Wait(); // blocks until task is completed

        }

        /// <summary>
        /// Demonstrates how to start a long-running task
        /// by preventing usage of a pooled thread.
        /// </summary>
        static void StartLongRunningTask()
        {
            // By default, the CLR runs tasks on pooled threads
            // which is ideal for short-running compute-bound
            // work. For longer-running and blocking operations
            // you can prevent use of a pooled thread as follows.
            Task task = Task.Factory.StartNew(() =>
            {
                DisplayCurrentTaskInfo("Entering");
                WriteLine("Started a long-running task");
                Thread.Sleep(5000);
                WriteLine("Long-running task finished");
                DisplayCurrentTaskInfo("Exiting");
            },
            TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Demonstrates how to return a value from a task.
        /// </summary>
        static void ReturnResultFromTask()
        {
            // `Task` has a generic subclass called
            // `Task<TResult>` that allows a task to emit
            // a return value. You can obtain `Task.Run`
            // with a `Func<TResult> delegate or
            // a compatible lambda epxression instead
            // of an `Action`.
            Task<int> task = Task.Run(() =>
            {
                DisplayCurrentTaskInfo("Entering");
                WriteLine("Returning a result from a task");
                DisplayCurrentTaskInfo("Exiting");
                return 3;
            });

            // To obtain the result form the task you can
            // query the `Result` property. If the task hasn't
            // yet finished, accessing this property will block
            // the current thread until the task finishes.
            int result = task.Result;
            WriteLine(result);
        }

        /// <summary>
        /// Counts the total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">A given upper bound.</param>
        /// <returns>The total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).</returns>
        static int CountPrimeNumbersPooledTask(int upperBound)
        {
            int? taskId = -1;
            Stopwatch s = Stopwatch.StartNew();

            Task<int> primeNumberTask = Task.Run(()=>
            {
                taskId = Task.CurrentId;
                DisplayCurrentTaskInfo("Entering", taskId);
                
                WriteLine("Counting prime numbers up to {0} ...", upperBound);

                // Use LINQ to count the number of prime numbers
                // up to a given upper bound.
                int primeNumberCount = Enumerable.Range(2, upperBound).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0)); 

                WriteLine(
                    "The total number of prime numbers  up to {0} is {1}",
                    upperBound,
                    primeNumberCount);

                DisplayCurrentTaskInfo("Exiting", taskId);

                return primeNumberCount;
            });

            int result = primeNumberTask.Result;

            DisplayCurrentTaskInfo(s.Elapsed.ToString(), taskId);

            return result;
        }

        /// <summary>
        /// Counts the total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">A given upper bound.</param>
        /// <returns>The total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).</returns>
        static int CountPrimeNumbersLongRunningTask(int upperBound)
        {
            int? taskId = -1;
            Stopwatch s = Stopwatch.StartNew();

            Task<int> primeNumberTask = Task.Factory.StartNew(() =>
            {
                taskId = Task.CurrentId;
                DisplayCurrentTaskInfo("Entering", taskId);

                WriteLine("Counting prime numbers up to {0} ...", upperBound);

                // Use LINQ to count the number of prime numbers
                // up to a given upper bound.
                int primeNumberCount = Enumerable.Range(2, upperBound).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                WriteLine(
                    "The total number of prime numbers  up to {0} is {1}",
                    upperBound,
                    primeNumberCount);

                DisplayCurrentTaskInfo("Exiting", taskId);

                return primeNumberCount;
            },
            TaskCreationOptions.LongRunning);

            int result = primeNumberTask.Result;

            DisplayCurrentTaskInfo(s.Elapsed.ToString(), taskId);

            return result;
        }

        /// <summary>
        /// Start a task that throws a <see cref="NullReferenceException"/>
        /// and handles the the exception outside of the task.
        /// </summary>
        static void HandleExceptionFromTask()
        {
            Task task = Task.Run(() =>
            {
                DisplayCurrentTaskInfo("Entering");
                throw null;
                //DisplayCurrentTaskInfo("Exiting"); // unreachable
            });
            try
            {
                // The exception is automatically rethrown to
                // whoever calls `Wait()` or access the `Result`
                // property of `Task<TResult>`.
                task.Wait();
            }
            catch (AggregateException aex)
            {
                // The CLR wraps the exception in a `AggregateException`
                // in order to play well with parallel programming scenarios.
                if (aex.InnerException is NullReferenceException)
                    DisplayError(aex.InnerException.ToString());
                else
                    throw; // propagate exception.
            }
        }

        /// <summary>
        /// Counts the total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">A given upper bound.</param>
        /// <returns>The total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).</returns>
        static int ContinuationWithAwaiterLongRunningTask(int upperBound)
        {
            // A *continuation* says to a task, "when you are finished,
            // continue by doing something else." A continuation is usually
            // implemented by a callback that executes once upon completion
            // of an operation. 
            int result = -1;
            int? taskId = -1;
            Stopwatch s = Stopwatch.StartNew();

            Task<int> primeNumberTask = Task.Factory.StartNew(() =>
            {
                taskId = Task.CurrentId;
                DisplayCurrentTaskInfo("Entering", taskId);
                WriteLine("Counting prime numbers up to {0} ...", upperBound);

                // Use LINQ to count the number of prime numbers
                // up to a given upper bound.
                int primeNumberCount = Enumerable.Range(2, upperBound).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                WriteLine(
                    "The total number of prime numbers  up to {0} is {1}",
                    upperBound,
                    primeNumberCount);
                DisplayCurrentTaskInfo("Exiting", taskId);
                return primeNumberCount;
            },
            TaskCreationOptions.LongRunning);

            // The first way to attach a continuation was introduced in
            // Framework 4.5 and is particularly significant because it's
            // used by C#'s asynchronous functions.

            // Calling `GetAwaiter` on the task (`primeNumberTask`) returns
            // an awaiter object whose `OnCompleted` method tells *antecedent*
            // task (`primeNumberTask`) to execute an `Action` delegate when
            // finishes or faults. It's valid to attach a continuation to
            // an already-completed task, in which case the continuation
            // will be scheduled to execute right away.

            // An *awaiter* is an object that exposes two methods
            // (`OnCompleted` and `GetResult`) and a boolean property
            // called `IsCompleted`. 
            var awaiter = primeNumberTask.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                // If an antecedent task faults, the exception is re-thrown
                // when the continuation code calls `awaiter.GetResult()`
                // withoud being wrapped into an `AggregateException` allowing
                // for simpler and cleaner `catch` blocks. For nongeneric
                // task `awaiter.GetResult()` has a void return value.
                // Its useful function is then solely to rethrow exceptions.
                result = awaiter.GetResult();
                DisplayCurrentTaskInfo(s.Elapsed.ToString(), taskId);
            });
            return result;
        }

        /// <summary>
        /// Counts the total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">A given upper bound.</param>
        /// <returns>The total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).</returns>
        static int ContinuationWithAwaiterPooledTask(int upperBound)
        {
            int result = -1;
            int? taskId = -1;
            Stopwatch s = Stopwatch.StartNew();

            Task<int> primeNumberTask = Task.Run(() =>
            {
                taskId = Task.CurrentId;
                DisplayCurrentTaskInfo("Entering", taskId);
                WriteLine("Counting prime numbers up to {0} ...", upperBound);

                // Use LINQ to count the number of prime numbers
                // up to a given upper bound.
                int primeNumberCount = Enumerable.Range(2, upperBound).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                WriteLine(
                    "The total number of prime numbers  up to {0} is {1}",
                    upperBound,
                    primeNumberCount);
                DisplayCurrentTaskInfo("Exiting", taskId);
                return primeNumberCount;
            });
 
            var awaiter = primeNumberTask.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                result = awaiter.GetResult();
                DisplayCurrentTaskInfo(s.Elapsed.ToString(), taskId);
            });
            return result;
        }

        /// <summary>
        /// Counts the total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">A given upper bound.</param>
        /// <returns>The total number of prime numbers up to a given 
        /// upper bound (<paramref name="upperBound"/>).</returns>
        static int ContinuationWithoutAwaiterLongRunningTask(int upperBound)
        { 
            int result = -1;
            int? taskId = -1;
            Stopwatch s = Stopwatch.StartNew();

            Task<int> primeNumberTask = Task.Factory.StartNew(() =>
            {
                taskId = Task.CurrentId;
                DisplayCurrentTaskInfo("Entering", taskId);
                WriteLine("Counting prime numbers up to {0} ...", upperBound);

                // Use LINQ to count the number of prime numbers
                // up to a given upper bound.
                int primeNumberCount = Enumerable.Range(2, upperBound).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                WriteLine(
                    "The total number of prime numbers  up to {0} is {1}",
                    upperBound,
                    primeNumberCount);
                DisplayCurrentTaskInfo("Exiting", taskId);
                return primeNumberCount;
            },
            TaskCreationOptions.LongRunning);

            // The other way to attach a continuation is by calling
            // the task's `ContinueWith` method. `ContinueWith` itself
            // returns a task, which is useful if you want to attach
            // further continuations. However, you must deal directly
            // with `AggregateException` if the task faults, and write
            // extra code to marshal continuation to the UI applications.
            // And in non-UI contexts, if you want continuation to
            // execute on the same thread you must specify
            // `TaskContinuationOptions.ExecuteSynchronously` otherwise
            // it will bounce to the thread pool. `ContinueWith` is
            // particularly useful in parallel programming.
            primeNumberTask.ContinueWith(antecedent =>
            {
                result = antecedent.Result;
                DisplayCurrentTaskInfo(s.Elapsed.ToString(), taskId);
            });
            return result;
        }

        /// <summary>
        /// Counts the total number of prime numbers up to a given 
        /// upper bound (<paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">A given upper bound.</param>
        /// <returns>The total number of prime numbers up to a given 
        /// upper bound (<paramref name="upperBound"/>).</returns>
        static int ContinuationWithoutAwaiterPooledTask(int upperBound)
        {
            int result = -1;
            int? taskId = -1;
            Stopwatch s = Stopwatch.StartNew();

            Task<int> primeNumberTask = Task.Run(() =>
            {
                taskId = Task.CurrentId;
                DisplayCurrentTaskInfo("Entering", taskId);
                WriteLine("Counting prime numbers up to {0} ...", upperBound);

                // Use LINQ to count the number of prime numbers
                // up to a given upper bound.
                int primeNumberCount = Enumerable.Range(2, upperBound).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

                WriteLine(
                    "The total number of prime numbers  up to {0} is {1}",
                    upperBound,
                    primeNumberCount);
                DisplayCurrentTaskInfo("Exiting", taskId);
                return primeNumberCount;
            });

            primeNumberTask.ContinueWith(antecedent =>
            {
                result = antecedent.Result;
                DisplayCurrentTaskInfo(s.Elapsed.ToString(), taskId);
            });
            return result;
        }

        /// <summary>
        /// Counts the total number of prime numbers up to a given
        /// upper bound (<paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">A given upper bound.</param>
        /// <returns>The total number of prime numbers up to a given 
        /// upper bound (<paramref name="upperBound"/>).</returns>
        static int CountPrimeNumbersWithoutTask(int upperBound)
        {
            int trackingId = TrackingId++;
            DisplayCurrentMethodInfo("Entering", trackingId);
            int result = -1;
            Stopwatch s = Stopwatch.StartNew();

            WriteLine("Counting prime numbers up to {0} ...", upperBound);

            // Use LINQ to count the number of prime numbers
            // up to a given upper bound.
           result = Enumerable.Range(2, upperBound).Count(n =>
                Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));

            WriteLine(
                "The total number of prime numbers  up to {0} is {1}",
                upperBound,
                result);

            DisplayCurrentMethodInfo("Exiting", trackingId);
            DisplayCurrentMethodInfo(s.Elapsed.ToString(), trackingId);

            return result;
        }
    }
}
