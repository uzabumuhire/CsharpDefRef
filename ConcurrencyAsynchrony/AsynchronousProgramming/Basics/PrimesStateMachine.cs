using System;
using System.Diagnostics;
using System.Threading.Tasks;

using static Core.DiagnosticHelper;

namespace ConcurrencyAsynchrony.AsynchronousProgramming.Basics
{
    class PrimesStateMachine
    {
        TaskCompletionSource<object> _tcs = new TaskCompletionSource<object>();

        /// <summary>
        /// Returns the <see cref="Task"/> associated to this <see cref="PrimesStateMachine"/>
        /// </summary>
        internal Task Task
        {
            get
            {
                return _tcs.Task;
            }
        }

        /// <summary>
        /// <para>
        /// Counts and displays prime numbers starting from a given index
        /// in a interval of given length, then continues to the next
        /// interval of the same length and so on by repeating this process
        /// in all of the given total number of intervals. Displays also
        /// the total count of prime numbers in all of the given 
        /// total number of intervals.
        /// </para>
        /// Displays runtime measurement of the count in each interval
        /// and also of the total count in all intervals.
        /// <para>
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
        internal void DisplayPrimeCountsAsyncRecursiveSeq(
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
                    // for loop and resorting to a recursive
                    // call in the continuation. This is
                    // important when for instance Task B
                    // depend on the result of Task A.
                    DisplayPrimeCountsAsyncRecursiveSeq(
                        countPrimeNumbersAsync,
                        intervalLength,
                        intervalStartIndex,
                        totalIntervals,
                        totalCount + count,
                        s, trackingId);
                else
                {
                    DisplayCurrentMethodInfo(
                        s.Elapsed.ToString() +
                        string.Format(" - There are a total of {0} between 0 and {1} ",
                            totalCount + count,
                            totalIntervals * intervalLength - 1),
                        trackingId);

                    _tcs.SetResult(null);

                    DisplayCurrentMethodInfo("Exiting", trackingId);
                }   
            });
        }
    }
}
