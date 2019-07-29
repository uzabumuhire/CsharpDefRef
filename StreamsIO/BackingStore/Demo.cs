using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

using static System.Console;

using static Core.FileHelper;
using static Core.StreamHelper;
using static Core.ConsoleHelper;
using static Core.DiagnosticHelper;

namespace StreamsIO.BackingStore
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of backing store streams.
        /// </summary>
        /// <returns>A task that is signaled upon completion or fault.</returns>
        internal static async Task TestAsync()
        {
            try
            {
                Directory.CreateDirectory(CreatePath("Files"));

                // Using a file stream synchronously.
                ReadWriteSeek();
                WriteLine();

                // Using a file stream asynchronously.
                await ReadWriteSeekAsync();
                WriteLine();

                // Reading a file stream into memory.
                using (Stream s = new FileStream(
                    CreatePath("Files/test1.txt"),
                    FileMode.Open))
                {
                    byte[] data = ReadFromStreamToMemory(s, 10);
                    WriteLine(data.Length);
                    foreach (byte d in data)
                        DisplaySpaceVal(d);
                }
                WriteLine();

                // Read-only (<=> `FileMode.Open` with `FileAccess.Read`).
                using (FileStream fs = File.OpenRead(CreatePath("Files/test1.txt")))
                {
                    DisplayStreamInfo(fs);
                }

                // Leaves existing content intact with the stream positioned at zero.
                // Write-only (<=> `FileMode.OpenOrCreate` with `FileAccess.Write`)
                using (FileStream fs = File.OpenWrite(CreatePath("Files/test1.txt")))
                {
                    DisplayStreamInfo(fs);
                }

                // Truncates any existing content.
                // Read/Write (<=> `FileMode.Create`).
                using (FileStream fs = File.Create(CreatePath("Files/tmp.txt")))
                {
                    DisplayStreamInfo(fs);
                }
                
                using (FileStream fs = File.Create(CreatePath("Files/tmp1.txt")))
                {
                    DisplayStreamInfo(fs);
                }

                // File.Create and FileMode.Create will throw an exception if used
                // on hidden files. To overwrite a hidden file, you must delete
                // and re-create it.
                // if (File.Exists("hidden.txt")) File.Delete("hidden.txt");

                // Constructing a `FileStream` with just a filename and `FileMode`
                // gives you (with just one exception : `FileMode.Append`) a readable
                // writable stream.

                // Opens an existing file for read/write access without overwriting it.
                using (FileStream fs = new FileStream(
                    CreatePath("Files/tmp.txt"),
                    FileMode.Open))
                {
                    DisplayStreamInfo(fs);
                }

                // You can request a downgrade if you also supply a `FileAccess` argument.
                // Opens an existing file for read only access without overwriting it.
                using (FileStream fs = new FileStream(
                    CreatePath("Files/tmp.txt"),
                    FileMode.Open,
                    FileAccess.Read))
                {
                    DisplayStreamInfo(fs);
                }

                // Open for appending.
                // With `FileMode.Append` you get a write-only stream.
                using (FileStream fs = new FileStream(
                    CreatePath("Files/tmp.txt"),
                    FileMode.Append))
                {
                    DisplayStreamInfo(fs);
                }

                // To append with read-write support, you must instead use
                // `FileMode.Open` or `FileMode.OpenOrCreate` and then
                // seek the end of the stream.
                using (FileStream fs = new FileStream(
                    CreatePath("Files/tmp.txt"),
                    FileMode.Open))
                {
                    // Sets the current position
                    // to the end of the stream.
                    fs.Seek(0, SeekOrigin.End);
                    DisplayStreamInfo(fs);
                }

                // When a program starts the current directory  may or may not
                // coincide with that of the program's executable. For this
                // reason, you should never rely on the current directory for
                // locating additional runtime files packaged along with your
                // executable.
                WriteLine(Environment.CurrentDirectory);

                // Returns the application base directory, which in normal
                // cases is the folder containing the program's executable.
                // To specify a filename relative to this directory, you
                // can call `Path.Combine`.
                WriteLine(AppDomain.CurrentDomain.BaseDirectory);

                WriteLine("DONE");
            }
            catch (Exception ex)
            {
                DisplayError(ex.ToString());
            }
        }

        /// <summary>
        /// Demonstrates usage of <see cref="FileStream"/> synchronously.
        /// </summary>
        /// <exception cref="IOException">If there is an I/O issue.</exception>
        static void ReadWriteSeek()
        {
            // Create a file called test.txt in the current directory.
            using (Stream s = new FileStream(CreatePath("Files/test1.txt"), FileMode.Create))
            {
                int trackingId = TrackingId++;
                Stopwatch sw = Stopwatch.StartNew();
                DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - entering", trackingId);

                WriteLine(s.CanRead);
                WriteLine(s.CanWrite);
                WriteLine(s.CanSeek);

                s.WriteByte(101);
                s.WriteByte(102);
                byte[] block = { 1, 2, 3, 4, 5 };
                s.Write(block, 0, block.Length); // write a block of 5 bytes

                WriteLine(s.Length);
                WriteLine(s.Position);
                s.Position = 0; // move the position back to the start

                WriteLine(s.ReadByte());
                WriteLine(s.ReadByte());

                // Read from the stream  back into the block array.
                WriteLine(s.Read(block, 0, block.Length));

                // Assuming the last Read returned 5, we'll be at
                // the end of the file, so Read will now return 0.
                WriteLine(s.Read(block, 0, block.Length));

                DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - exiting", trackingId);
            }
        }

        /// <summary>
        /// Demonstrates usage of <see cref="FileStream"/> asynchronously.
        /// </summary>
        /// <returns>A task that is signaled upon completion or fault.</returns>
        /// <exception cref="IOException">If there is an I/O issue.</exception>
        static async Task ReadWriteSeekAsync()
        {
            // Create a file called test.txt in the current directory.
            using (Stream s = new FileStream(CreatePath("Files/test2.txt"), FileMode.Create))
            {
                int trackingId = TrackingId++;
                Stopwatch sw = Stopwatch.StartNew();
                DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - entering", trackingId);

                WriteLine(s.CanRead);
                WriteLine(s.CanWrite);
                WriteLine(s.CanSeek);

                s.WriteByte(101);
                s.WriteByte(102);
                byte[] block = { 1, 2, 3, 4, 5 };
                await s.WriteAsync(block, 0, block.Length); // write a block of 5 bytes

                WriteLine(s.Length);
                WriteLine(s.Position);
                s.Position = 0; // move the position back to the start

                WriteLine(s.ReadByte());
                WriteLine(s.ReadByte());

                // Read from the stream  back into the block array.
                WriteLine(await s.ReadAsync(block, 0, block.Length));

                // Assuming the last Read returned 5, we'll be at
                // the end of the file, so Read will now return 0.
                WriteLine(await s.ReadAsync(block, 0, block.Length));

                DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - exiting", trackingId);
            }
        }

        /// <summary>
        /// Read a given number of bytes from a given stream into memory.
        /// </summary>
        /// <param name="s">A given stream to read from.</param>
        /// <param name="count">A given number of bytes to read from a given stream.</param>
        /// <returns>The array containing the bytes read from a given stream</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="s"/> is null</exception>
        /// <exception cref="ArgumentException">if <paramref name="count"/> is lower than or equal to 0</exception>
        /// <exception cref="IOException">If there is an I/O issue.</exception>
        static byte[] ReadFromStreamToMemory(Stream s, int count)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            if (count < 0)
                throw new ArgumentException(nameof(count) + " must be positive");

            // `Read` receives a block of data from the stream into an array.
            // It returns the number of bytes received, which is always either
            // less than or equal to the `count` argument. If it's less than
            // `count`, it means that the end of the stream has been reached
            // or the stream is giving you the data in smaller chunks (as is
            // often the case with network streams).

            // With read, you can be certain that you've reached the end of
            // the stream only when the method returns 0. So if you have
            // a given number of bytes stream, the following code may fail
            // to read it all into memory. If chunk received from the stream
            // is smaller than memory.Length.
            // s.Read(memory, 0, memory.Length);

            // The correct way to read a given number of bytes stream into
            // memory is the following code or use the `BinaryReader` type
            // byte[] memory = new BinaryReader(s).ReadBytes(count);
            // If the stream is less than `count` bytes, the byte array
            // returned reflects the actual stream size.
            // If the stream is seekable, you can read its entire contents
            // replacing `count` with `(int)s.Length`.

            byte[] memory = new byte[count];

            // `bytesRead` will always end up at a given number of bytes,
            // unless the stream is smaller in length.
            int bytesRead = 0;
            int chunkSize = 1;
            while (bytesRead < memory.Length && chunkSize > 0)
            {
                bytesRead +=
                    chunkSize = s.Read(memory, bytesRead, memory.Length - bytesRead);
            }

            return memory;
        }
    }
}
