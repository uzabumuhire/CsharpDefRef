using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

using static System.Math;

using static Core.DiagnosticHelper;

namespace ConcurrencyAsynchrony.AsynchronousProgramming.Basics
{
    static class Demo
    {
        internal static void Test()
        {
            /*
            ///*
            DisplayPrimeCountsSyncSeq(GetPrimesCountNonParallelSync, 1_000_000, 10);
            DisplayPrimeCountsSyncSeq(GetPrimesCountSemiParallelSync, 1_000_000, 10);
            DisplayPrimeCountsSyncSeq(GetPrimesCountFullParallelSync, 1_000_000, 10);
            //*/

            /*
            ///*
            DisplayPrimeCountsAsyncNonSeq(GetPrimeCountNonParallelAsync, 1_000_000, 10);
            DisplayPrimeCountsAsyncNonSeq(GetPrimeCountSemiParallelAsync, 1_000_000, 10);
            DisplayPrimeCountsAsyncNonSeq(GetPrimeCountFullParallelAsync, 1_000_000, 10);
            //*/

            /*
            ///*
            DisplayPrimeCountsAsyncSeq(GetPrimeCountNonParallelAsync, 1_000_000, 10);
            DisplayPrimeCountsAsyncSeq(GetPrimeCountSemiParallelAsync, 1_000_000, 10);
            DisplayPrimeCountsAsyncSeq(GetPrimeCountFullParallelAsync, 1_000_000, 10);
            //*/

            /*
            ///*
            DisplayPrimeCountsSeqAsync(GetPrimeCountNonParallelAsync, 1_000_000, 10);
            DisplayPrimeCountsSeqAsync(GetPrimeCountSemiParallelAsync, 1_000_000, 10);
            DisplayPrimeCountsSeqAsync(GetPrimeCountFullParallelAsync, 1_000_000, 10);
            //*/

            /*
            ///*
            _ = DisplayPrimesCountsSeqAsync(GetPrimeCountNonParallelAsync, 1_000_000, 10);
            _ = DisplayPrimesCountsSeqAsync(GetPrimeCountSemiParallelAsync, 1_000_000, 10);
            _ = DisplayPrimesCountsSeqAsync(GetPrimeCountFullParallelAsync, 1_000_000, 10);
            //*/
          
        }

        /// <summary>
        /// Counts and displays prime numbers starting from a given index
        /// in a interval of given length, then continues to the next
        /// interval of the same length and so on by repeating this process
        /// in all of the given total number of intervals in a sequential
        /// order. Displays also the total count of prime numbers
        /// in all of the given total number of intervals.
        /// </summary>
        /// <param name="countPrimeNumbersSync">An synchronous method used
        /// to count prime numbers in a given interval.</param>
        /// <param name="intervalLength">The given length of the interval.</param>
        /// <param name="totalIntervals">The total number of intervalls.</param>
        static void DisplayPrimeCountsSyncSeq(
            Func<int, int, int> countPrimeNumbersSync,
            int intervalLength,
            int totalIntervals)
        {
            int trackingId = TrackingId++;
            DisplayCurrentMethodInfo("Entering", trackingId);
            Stopwatch s = Stopwatch.StartNew();

            int totalCount = 0;
            int count;

            for (int i = 0; i < totalIntervals; i++)
            {
                int begin = i * intervalLength;
                int end = (i + 1) * intervalLength - 1;
                count = countPrimeNumbersSync(i * intervalLength + 2, intervalLength);
                totalCount += count;
                DisplayCurrentMethodInfo(
                    s.Elapsed.ToString() +
                    string.Format(" - There are {0} primes between {1} and {2}", count, begin, end),
                    trackingId);
            }

            DisplayCurrentMethodInfo(
                s.Elapsed.ToString() +
                string.Format(
                    " - There are a total of {0} between 0 and {1} ",
                    totalCount,
                    totalIntervals * intervalLength - 1),
                trackingId);

            DisplayCurrentMethodInfo("Exiting", trackingId);
        }

        static int GetPrimesCountSemiParallelSync(int start, int count)
        {
            return ParallelEnumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));
        }

        static int GetPrimesCountFullParallelSync(int start, int count)
        {
            return ParallelEnumerable.Range(start, count).Count(n =>
                ParallelEnumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));
        }

        static int GetPrimesCountNonParallelSync(int start, int count)
        {
            return Enumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0));
        }

        /// <summary>
        /// Counts and displays prime numbers starting from a given index
        /// in a interval of given length, then continues to the next
        /// interval of the same length and so on by repeating this process
        /// in all of the given total number of intervals in a non sequential
        /// order. Displays also the total count of prime numbers
        /// in all of the given total number of intervals.
        /// </summary>
        /// <param name="countPrimeNumbersAsync">An asynchronous method used
        /// to count prime numbers in a given interval.</param>
        /// <param name="intervalLength">The given length of the interval.</param>
        /// <param name="totalIntervals">The total number of intervalls.</param>
        static void DisplayPrimeCountsAsyncNonSeq(
            Func<int, int, Task<int>> countPrimeNumbersAsync,
            int intervalLength,
            int totalIntervals)
        {
            int trackingId = TrackingId++;
            DisplayCurrentMethodInfo("Entering", trackingId);
            Stopwatch s = Stopwatch.StartNew();

            int lastExecution = 0;
            int totalCount = 0;
            int count = 0;

            for (int i = 0; i < totalIntervals; i++)
            {
                // The loop will rapidly spin through ten iterations
                // (the methods being nonblocking), all of the operations
                // will execute in parallel.

                // Executing theses tasks in parallel is undesirable in
                // in this case because their internal implementations
                // are already parallelized and it will only make us
                // wait longer to see the first results which will
                // no sequential.
                int temp = i;
                int begin = i * intervalLength;
                int end = (i + 1) * intervalLength - 1;
                var awaiter = countPrimeNumbersAsync(
                    i * intervalLength + 2,
                    intervalLength).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    count = awaiter.GetResult();
                    totalCount += count;
                    DisplayCurrentMethodInfo(
                        s.Elapsed.ToString() +
                        string.Format(" - There are {0} primes between {1} and {2}", count, begin, end),
                        trackingId);

                    // We print the total when we are in the last execution.
                    if (++lastExecution >= totalIntervals && totalCount != count)
                    {
                        DisplayCurrentMethodInfo(
                        s.Elapsed.ToString() +
                        string.Format(" - There are a total of {0} between 0 and {1} ",
                            totalCount,
                            totalIntervals * intervalLength - 1),
                        trackingId);
                        DisplayCurrentMethodInfo("Exiting", trackingId);
                    }
                });
            }
        }

        static Task<int> GetPrimeCountSemiParallelAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0)));
        }

        static Task<int> GetPrimeCountFullParallelAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    ParallelEnumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0)));
        }

        static Task<int> GetPrimeCountNonParallelAsync(int start, int count)
        {
            return Task.Run(() =>
                Enumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Sqrt(n) - 1).All(i => n % i > 0)));
        }

        /// <summary>
        /// Counts and displays prime numbers starting from a given index
        /// in a interval of given length, then continues to the next
        /// interval of the same length and so on by repeating this process
        /// in all of the given total number of intervals in a sequential
        /// order. Displays also the total count of prime numbers
        /// in all of the given total number of intervals.
        /// </summary>
        /// <param name="countPrimeNumbersAsync">An asynchronous method used
        /// to count prime numbers in a given interval.</param>
        /// <param name="intervalLength">The given length of the interval.</param>
        /// <param name="totalIntervals">The total number of intervalls.</param>
        static void DisplayPrimeCountsAsyncSeq(
            Func<int, int, Task<int>> countPrimeNumbersAsync,
            int intervalLength,
            int totalIntervals)
        {
            int trackingId = TrackingId++;
            DisplayCurrentMethodInfo("Entering", trackingId);
            Stopwatch s = Stopwatch.StartNew();

            DisplayPrimeCountsAsyncRecursiveSeq(
                countPrimeNumbersAsync,
                intervalLength, 0,
                totalIntervals, 0,
                s, trackingId);

            DisplayCurrentMethodInfo(s.Elapsed.ToString(), trackingId);
            DisplayCurrentMethodInfo("Exiting", trackingId);
        }

        /// <summary>
        /// <para>
        /// Counts and displays prime numbers starting from a given index
        /// in a interval of given length, then continues to the next
        /// interval of the same length and so on by repeating this process
        /// in all of the given total number of intervals in a sequential
        /// order. Displays also the total count of prime numbers
        /// in all of the given total number of intervals.
        /// </para>
        /// <para>
        /// Displays runtime measurement of the count in each interval
        /// and also of the total count in all intervals.
        /// </para>
        /// </summary>
        /// <param name="countPrimeNumbersAsync">An asynchronous function used
        /// to count prime numbers in a given interval.</param>
        /// <param name="intervalLength">The given length of the interval.</param>
        /// <param name="intervalStartIndex">The given index to start from.</param>
        /// <param name="totalIntervals">The total number of intervalls.</param>
        /// <param name="totalCount">The total count of prime numbers in all
        /// of the preceding intervals.</param>
        /// <param name="s">Used for measuring runtime performance.</param>
        /// <param name="trackingId">Used for tracking the current method
        /// in runtime performance measurement.</param>
        static void DisplayPrimeCountsAsyncRecursiveSeq(
            Func<int, int, Task<int>> countPrimeNumbersAsync,
            int intervalLength,
            int intervalStartIndex,
            int totalIntervals,
            int totalCount,
            Stopwatch s,
            int trackingId)
        {
            if (intervalStartIndex == 0)
                DisplayCurrentMethodInfo("Entering", trackingId);

            int begin = intervalStartIndex * intervalLength;
            int end = (intervalStartIndex + 1) * intervalLength - 1;

            var awaiter = countPrimeNumbersAsync(
                    intervalStartIndex * intervalLength + 2,
                    intervalLength).GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                int count = awaiter.GetResult();

                DisplayCurrentMethodInfo(
                    s.Elapsed.ToString() +
                    string.Format(" - There are {0} primes between {1} and {2}", count, begin, end),
                    trackingId);

                if (++intervalStartIndex < totalIntervals)
                    // To get all the parallel iteration run
                    // sequential, we must trigger the next
                    // loop iteration from the continuation
                    // itself. This means eliminating the
                    // `for loop and resorting to a recursive
                    // call in the continuation. This is
                    // important when for instance Task B
                    // depend on the result of Task A.
                    DisplayPrimeCountsAsyncRecursiveSeq(
                        countPrimeNumbersAsync,
                        intervalLength,
                        intervalStartIndex,
                        totalIntervals,
                        totalCount + count, s,
                        trackingId);
                else
                {
                    DisplayCurrentMethodInfo(
                        s.Elapsed.ToString() +
                        string.Format(" - There are a total of {0} between 0 and {1} ",
                            totalCount + count,
                            totalIntervals * intervalLength - 1),
                        trackingId);
                    DisplayCurrentMethodInfo("Exiting", trackingId);
                }  
            });
        }

        /// <summary>
        /// Counts and displays prime numbers starting from a given index
        /// in a interval of given length, then continues to the next
        /// interval of the same length and so on by repeating this process
        /// in all of the given total number of intervals in a sequential
        /// order. Displays also the total count of prime numbers
        /// in all of the given total number of intervals.
        /// </summary>
        /// <param name="countPrimeNumbersAsync">An asynchronous method used
        /// to count prime numbers in a given interval.</param>
        /// <param name="intervalLength">The given length of the interval.</param>
        /// <param name="totalIntervals">The total number of intervalls.</param>
        /// <returns>A task that it signals upon completion</returns>
        static Task DisplayPrimeCountsSeqAsync(
            Func<int, int, Task<int>> countPrimeNumbersAsync,
            int intervalLength,
            int totalIntervals)
        {
            Stopwatch s = Stopwatch.StartNew();
            int trackingId = TrackingId++;
            DisplayCurrentMethodInfo("Entering", trackingId);

            // To make `DisplayPrimeCountsSeq` asynchronous, that
            // is to make it return a `Task` that it signals upon
            // completion, we must create a `TaskCompletionSource`.
            // by using `PrimesStateMachine` class.
            PrimesStateMachine machine = new PrimesStateMachine();
            machine.DisplayPrimeCountsAsyncRecursiveSeq(
                countPrimeNumbersAsync,
                intervalLength, 0,
                totalIntervals, 0,
                s, trackingId);

            DisplayCurrentMethodInfo(s.Elapsed.ToString(), trackingId);
            DisplayCurrentMethodInfo("Exiting", trackingId);

            return machine.Task;
        }

        /// <summary>
        /// Counts and displays prime numbers starting from a given index
        /// in a interval of given length, then continues to the next
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
        static async Task DisplayPrimesCountsSeqAsync(
            Func<int, int, Task<int>> countPrimeNumbersAsync,
            int intervalLength,
            int totalIntervals)
        {
            int trackingId = TrackingId++;
            DisplayCurrentMethodInfo("Entering", trackingId);
            Stopwatch s = Stopwatch.StartNew();

            int totalCount = 0;
            int count;

            for (int i = 0; i < totalIntervals; i++)
            {
                int begin = i * intervalLength;
                int end = (i + 1) * intervalLength - 1;

                // C#'s *asynchronous functions* do all of this work for us
                // with `async` and `await` keywords. Hence `async` and
                // `await` are essential for implementing asynchrony
                // without excessive complexity.
                count = await countPrimeNumbersAsync(i * intervalLength + 2, intervalLength);
                totalCount += count;
                DisplayCurrentMethodInfo(
                    s.Elapsed.ToString() +
                    string.Format(" - There are {0} primes between {1} and {2}", count, begin, end),
                    trackingId);
            }

            DisplayCurrentMethodInfo(
                s.Elapsed.ToString() +
                string.Format(
                    " - There are a total of {0} between 0 and {1} ",
                    totalCount,
                    totalIntervals * intervalLength - 1),
                trackingId);

            DisplayCurrentMethodInfo("Exiting", trackingId);
        }
    }
}
