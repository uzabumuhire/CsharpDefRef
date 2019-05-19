using System;
using System.Threading;

namespace ConcurrencyAsynchrony
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteXY();
            Write10();
        }

        static void WriteXY()
        {
            Thread t = new Thread(WriteY); // kick off a new thread
            t.Start(); // running WriteY

            // Simultaneously, do something on the main thread
            for (int i = 0; i < 1000; i++)
                Console.Write("X");
        }

        static void WriteY()
        {
            for (int i = 0; i < 1000; i++)
                Console.Write("y");
        }

        static void Write10()
        {
            new Thread(Write5).Start();
            Write5();
        }

        static void Write5()
        {
            // Declare and use a local variable `cycles`
            for (int cylces = 0; cylces < 5; cylces++)
                Console.Write("?");
        }


    }
}
