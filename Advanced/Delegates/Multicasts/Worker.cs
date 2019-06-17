namespace Advanced.Delegates.Multicasts
{
    /// <summary>
    /// Reports the progress of the work that have been done.
    /// </summary>
    delegate void ProgressRepoter(int percentComplete, System.IO.TextWriter writer);

    static class Worker
    {
        /// <summary>
        /// Simulates a method that takes a long time to execute and that
        /// regulary report progress to its caller by invoking 
        /// a <see cref="ProgressRepoter"/> delegate
        /// </summary>
        /// <param name="p">A <see cref="ProgressRepoter"/> delegate
        /// instance usde to indicate progress of the work that has
        /// been done.</param>
        internal static void HardWork(ProgressRepoter p)
        {
            // The full directory of the progresss report.
            string path = System.IO.Path.Combine(
                System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                "progress-report.txt");

            using (System.IO.FileStream fs = System.IO.File.Create(path))
            using (System.IO.TextWriter writer = new System.IO.StreamWriter(fs))
            {
                for (int i = 1; i < 101; i++)
                {
                    // Invoke the delegate. This will invoke the list of the 
                    // target methods assigned `p` delegate instance.
                    p(i, writer);

                    // Simulate hard work.
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
