using System;
using System.Threading;

using static System.Console;

using static Core.ThreadHelper;
using static Core.ConsoleHelper;


namespace ConcurrencyAsynchrony.Threading.Basics
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates basic usage of threads.
        /// </summary>
        internal static void Test()
        {
            CreateThread();

            WaitForThread();

            LocalState();
            
            SharedCommonInstance sci = new SharedCommonInstance();
            sci.SharedState();

            SharedCapturedLocalVariables sclv = new SharedCapturedLocalVariables();
            sclv.SharedState();

            SharedStaticFields ssf = new SharedStaticFields();
            ssf.SharedState();

            SharedStateThreadSafe ssts = new SharedStateThreadSafe();
            ssts.SharedState();
            
            PassingDataToThread();
            
            PassingCapturedVariables();
            
            HandledExceptionThread();

            //UnhandledExceptionThread(); // not handled exception shutdown the whole application

            ThreadSignaling();

            ThreadPooling();
        }

        /// <summary>
        /// Demonstrates how to creates threads.
        /// </summary>
        static void CreateThread()
        {
            // You can create and start a new thread by
            // instantiating a `Thread` object and calling
            // its `Start` method.
            // The simplest constructor for `Thread` takes
            // a `ThreadStart` delegate : a parameterless
            // method indicating where execution should begin.
            Thread t = new Thread(WriteY);
            t.Name = "WY";
            t.Start();

            // Simultaneously, do something on the main thread.
            for (int i = 0; i < 1000; i++)
            {
                Write("x");
            }
        }

        static void WriteY()
        {
            DisplayCurrentThreadInfo("Entering");
            for (int i = 0; i < 1000; i++)
            {
                Write("y");
            }
            DisplayCurrentThreadInfo("Exiting");
        }

        /// <summary>
        /// Demonstrates how to wait for a thread.
        /// </summary>
        static void WaitForThread()
        {
            // You can wait for another thread to end by
            // calling its `Join` method.
            Thread t = new Thread(WriteZ);
            t.Name = "WZ";
            t.Start();
            t.Join();
        }

        static void WriteZ()
        {
            DisplayCurrentThreadInfo("Entering");
            for (int i = 0; i < 1000; i++)
            {
                Write("z");
            }
            DisplayCurrentThreadInfo("Exiting");
        }

        /// <summary>
        /// Demonstrates how each thread has its own memory stack
        /// and how local variables are kept seperate.
        /// </summary>
        static void LocalState()
        {
            // The CLR assigns each thread its own memory stack
            // so that local variables are kept separate.

            // A separate copy of the cycles variable is created
            // on ech thread's memory stack, so the output is ten 1.
            Thread t = new Thread(Write1);
            t.Name = "W1";
            t.Start(); // call `Write1` on the new thread
            Write1(); // call `Write1` on the main thread
        }

        static void Write1()
        {
            DisplayCurrentThreadInfo("Entering");
            // Declare and use a local variable `cycles`.
            for (int cycles = 0; cycles < 5; cycles++)
                Write(1);
            DisplayCurrentThreadInfo("Exiting");
        }

        /// <summary>
        /// Demonstrates how to pass data to thread with lambda
        /// expressions and also via a thread's <c>Start</c> method.
        /// </summary>
        static void PassingDataToThread()
        {
            // The easiest way to pass arguments to the thread's startup
            // method is with lambda expressions that calls the method
            // with the desired arguments.
            Thread t1 = new Thread(() => Print("Hello from thread " + Thread.CurrentThread.Name));
            t1.Name = "PDTT1";
            t1.Start();

            // You can even wrap the entire implementation
            // in a multistatement lambda expression.
            Thread t2 = new Thread(() =>
            {
                DisplayCurrentThreadInfo("Entering");
                WriteLine("I am running on thread " + Thread.CurrentThread.Name);
                WriteLine("This is so easy on thread " + Thread.CurrentThread.Name);
                DisplayCurrentThreadInfo("Exiting");
            });
            t2.Name = "PDTT2";
            t2.Start();

            // Old-school technique prior C# 3.0,
            // where you need to pass an argument
            // into `Thread`'s Start` method.

            // This works because `Thread`'s constructor
            // is overloaded to accept either of two
            // delegates :
            // `public delegate void ThreadStart();`
            // `public delegate void ParameterizedThreadStart(object obj);`

            // The limitation of `ParameterizedThreadStart` is that
            // it accepts only one argument. And because it's of type
            // `object`, it usually needs to be cast.
            Thread t3 = new Thread(Print);
            t3.Name = "PDTT3";
            t3.Start("Hello from thread " + Thread.CurrentThread.Name);
        }

        static void Print(string message)
        {
            DisplayCurrentThreadInfo("Entering");
            WriteLine(message);
            DisplayCurrentThreadInfo("Exiting");
        }

        static void Print(object messageObj)
        {
            DisplayCurrentThreadInfo("Entering");
            string message = (string)messageObj; // casting needed
            WriteLine(message);
            DisplayCurrentThreadInfo("Exiting");
        }

        /// <summary>
        /// Demonstrates how captured variables by a lambda expression
        /// can make your output nondeterministic and how to solve the
        /// issue with a temporary variable.
        /// </summary>
        static void PassingCapturedVariables()
        {
            for (int i = 0; i < 10; i++)
            {
                // You must be careful about  accidentally modifying *captured variables*
                // in a lambda expression after starting a thread. Because the output will be
                // nondeterministic.

                // Since `i` variable refers to the *same* memory location throughout the
                // loop's lifetime. Each thread calls `WriteLine` on a variable whose value
                // may change as it is running.
                Thread t1 = new Thread(() =>
                {
                    DisplayCurrentThreadInfo("Entering");
                    WriteLine(i + " from " + Thread.CurrentThread.Name);
                    DisplayCurrentThreadInfo("Exiting");
                });
                t1.Name = "PCVT1" + i;
                t1.Start();

                // The solution is to use a temporary variable so that each digits 0 to 9 is
                // then written exactly once. The *ordering* is still undefined because
                // threads may start at indeterminate times. The temporary variable `temp`
                // is now local to each loop iteration. Therefore, each thread captures
                // a different memory location and there's no problem.
                int temp = i;
                Thread t2 = new Thread(() =>
                {
                    DisplayCurrentThreadInfo("Entering");
                    WriteLine(temp + " from " + Thread.CurrentThread.Name);
                    DisplayCurrentThreadInfo("Exiting");
                });
                t2.Name = "PCVT2" + i;
                t2.Start();
            }
        }

        /// <summary>
        /// Demonstrates the danger of unhandled exceptions in threads.
        /// </summary>
        static void UnhandledExceptionThread()
        {
            // Any try/catch/finally blocks in effect when a thread is created
            // are of no relevance to the thread when it starts executing.

            // The following try/catch statement is ineffective,
            // and the newly created thread will be encumbered with
            // an unhandled `NullReferenceException`.

            // This behavior makes sense when you consider that each thread
            // has an independent execution path.

            // The remedy is to move the exception handler into
            // the `ThreadExceptionUnhandled` method.
            try
            {
                Thread t = new Thread(ThreadExceptionUnhandled);
                t.Name = "UET";
                t.Start();
            }
            catch (Exception ex)
            {
                // We will never get here.
                DisplayError("From  thread " + Thread.CurrentThread.Name + "\n" + ex);
            }
        }

        static void ThreadExceptionUnhandled()
        {
            DisplayCurrentThreadInfo("Entering");
            throw null; // NullReferenceException thrown
            //DisplayCurrentThreadInfo("Exiting"); // Unreachable
        }

        /// <summary>
        /// Demonstrates how to handle exceptions with threads.
        /// </summary>
        static void HandledExceptionThread()
        {
            // Exception handling logic has been moved to
            // the `ThreadExceptionHandled` method.

            // You need an exception handler on all thread entry methods
            // in production applications - just as you do (usually at
            // a higher level, in the execution stack) on your main thread.

            // An unhandled exception causes the whole application to
            // shut down.
            Thread t = new Thread(ThreadExceptionHandled);
            t.Name = "HET";
            t.Start();
        }

        static void ThreadExceptionHandled()
        {
            DisplayCurrentThreadInfo("Entering");
            try
            {
                throw null; // NullReferenceException will get caught below
            }
            catch (Exception ex)
            {
                // Typically log the exception, and/or signal another thread
                // that we've come stuck.
                DisplayError("From  thread " + Thread.CurrentThread.Name + "\n" + ex);
            }
            DisplayCurrentThreadInfo("Exiting");
        }

        /// <summary>
        /// Demonstrates how to signal a thread.
        /// </summary>
        static void ThreadSignaling()
        {
            // Sometimes you need a thread to wait until receiving notification(s)
            // from other thread(s). This is called *signaling*.

            // The simplest signaling construct is `ManualResetEvent`. Calling `WaitOne`
            // on a `ManuelResetEvent` blocks the current thread until another thread
            // *opens* the signal by calling `Set`. After calling `Set` the signal
            // remains open and it may be closed again by calling `Reset`.
            ManualResetEvent signal = new ManualResetEvent(false);

            // We start up a thread that wait on a ManualResetEvent. It remains blocked
            // for 2 seconds until the main thread *signals* it.
            Thread t = new Thread(() =>
            {
                DisplayCurrentThreadInfo("Entering");
                DisplayInfo("Thread " + Thread.CurrentThread.Name + " is waiting for signal.");
                signal.WaitOne(); // block thread t
                signal.Dispose();
                DisplayInfo("Thread " + Thread.CurrentThread.Name + " has been signaled.");
                DisplayCurrentThreadInfo("Exiting");
            });
            t.Name = "TS";
            t.Start();

            Thread.Sleep(2000);
            signal.Set(); // open the signal to unblock thread t
        }

        /// <summary>
        /// Demonstrates thread pooling.
        /// </summary>
        static void ThreadPooling()
        {
            // Whenever you start a thread, a few hundred microseconds
            // are spent on things as a fresh local variable stack.
            // The *thread pool* cut this overhead by having a pool
            // of pre-created recyclable threads.

            // Threads pooling is essential for efficient parallel
            // programming and fine-grained concurrency. It allows
            // short operations to run without being overwhelmed
            // with the overhead of thread startup.

            // Pooled threads are always *background threads*,
            // their Name property cannot be set and blocking
            // them can degrade peformance.

            // To explicitly run something on a pooled thread,
            // use `Task.Run` (in System.Threading.Tasks) :
            // Task.Run(() => DisplayInfo("..."));

            // Prior Framework 4.0, tasks didn't exist,
            // a common alternative is to call
            // ThreadPool.QueueUserWorkItem.
            ThreadPool.QueueUserWorkItem( notUsed =>
            {
                DisplayCurrentThreadInfo("Entering");
                DisplayInfo("Thread " + Thread.CurrentThread.Name + " is pooled thread.");
                DisplayCurrentThreadInfo("Exiting");
            });
        }
    }
}
