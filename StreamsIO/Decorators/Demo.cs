using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading.Tasks;

using static System.Console;

using static Core.FileHelper;
using static Core.ConsoleHelper;
using static Core.DiagnosticHelper;

namespace StreamsIO.Decorators
{
    static class Demo
    {
        static double fsWithBs, fsWithoutBs;
        static int numberOfPerfomanceTests = 100;

        /// <summary>
        /// Demonstrates usage of decorator streams.
        /// </summary>
        internal static async Task TestAsync()
        {
            try
            {
                // BufferedStream decorators
                /*
                RunFileStreamWithBuffering();
                WriteLine();

                RunFileStreamWihoutBuffering();
                WriteLine();
                */
                //BenchmarkBufferedStream();

                // Compression stream.

                // `DeflateStream` and `GZipStream` use a popular compression
                // algorithm similar to that aof the ZIP format. They differ
                // in that `GZipStream` writes an additional protocol at the
                // start and end - including a CRC to detect errors.
                // `GZipStream` also confirms to a standard recognized by other
                // software.

                // Both streams allow reading and writing:
                // - you always *write* to the stream when compressing.
                // - you always *read* from the stream when decompressing.

                // `DeflateStream` and `GZipStream` are decorators: they compress
                // or decompress data from another stream that you supply in the
                // constructor.
                string path1 = CreatePath("Files/compressed.bin");
                CompressFileStream(path1);
                DecompressFileStream(path1);

                string path2 = CreatePath("Files/compressed-text.bin");
                await CompressTextFileAsync(path2);
                await DecompressTextFileAsync(path2);

                // Compressing in memory.
                InMemoryCompression();
                await InMemoryCompressionAsync();

                // Working with ZIP files

                // The advantage of ZIP-file compression format
                // (via ZipArchive and ZipFile classes) over
                // `DeflateStream` and `GZipStream` is that it
                // acts as a container for multiple files, and
                // is compatible with ZIP files created with
                // Windows Explorer or other compression utilities.

                // `ZipArchive` works with streams, whereas `ZipFile`
                // addresses the more common scenario of working with
                // files. (`ZipFile` is a static helper class for
                // `ZipArchive`)

                //ZipFileCompression();

                DisplayZippedFiles(CreatePath("ZippedFiles"));

                //CreateEmptyZipWithStream();

                //AddEntryToZipWithStream();

                string pathToZipFile = CreatePath("zipped-files.zip");

                //AddEntryToZipFromFile(CreatePath("Files/tmp.txt"), pathToZipFile);
                //AddEntryToZipFromFile(CreatePath("Files/tmp1.txt"), pathToZipFile);
                //AddEntryToZipFromFile(CreatePath("Files/test1.txt"), pathToZipFile);
                //AddEntryToZipFromFile(CreatePath("Files/test2.txt"), pathToZipFile);

                //AddFileToZipInMemory(CreatePath("Files/decompressed-text.bin"), pathToZipFile);

                DisplayZippedFiles(pathToZipFile);
            }
            catch (IOException ex)
            {
                DisplayError(ex.ToString());
            }
        }

        /// <summary>
        /// Adds a file to the ZIP file entirely in memory.
        /// </summary>
        /// <param name="pathToFile">A path to the file</param>
        /// <param name="pathToZip">A path to the ZIP file</param>
        static void AddFileToZipInMemory(string pathToFile, string pathToZip)
        {
            // create a working memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] data = File.ReadAllBytes(pathToZip);
                ms.Write(data, 0, data.Length);

                // create a zip
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Update, true))
                {
                    // add the entry to the zip
                    ZipArchiveEntry zipEntry = zip.CreateEntry(Path.GetFileName(pathToFile));

                    // add the  bytes to the zip entry by opening the original file and copying the bytes 
                    using (MemoryStream originalFileMemoryStream = new MemoryStream(File.ReadAllBytes(pathToFile)))
                    {
                        using (Stream entryStream = zipEntry.Open())
                        {
                            originalFileMemoryStream.CopyTo(entryStream);
                        }
                    }
                }
                File.WriteAllBytes(pathToZip, ms.ToArray());
            }
        }

        /// <summary>
        /// Create an empty zip file from a stream.
        /// </summary>
        static void CreateEmptyZipWithStream()
        {
            string zippedFiles = CreatePath("zipped-files.zip");
            using (FileStream zip = new FileStream(zippedFiles, FileMode.Create))
            {
                ZipArchive archive = new ZipArchive(zip, ZipArchiveMode.Create);
                archive.Dispose();
            }
        }

        /// <summary>
        /// Adds a file to the ZIP file.
        /// </summary>
        /// <param name="pathToFile">A path to the file</param>
        /// <param name="pathToZip">A path to the ZIP file</param>
        static void AddEntryToZipFromFile(string pathToFile, string pathToZip)
        {
            using (ZipArchive zip = ZipFile.Open(pathToZip, ZipArchiveMode.Update))
                zip.CreateEntryFromFile(pathToFile, Path.GetFileName(pathToFile));
        }

        /// <summary>
        /// Shows how to create a new entry and write to it by using a stream.
        /// </summary>
        static void AddEntryToZipWithStream()
        {
            string zippedFiles = CreatePath("zipped-files.zip");
            using (FileStream zipToOpen = new FileStream(zippedFiles, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        writer.WriteLine("Information about this package.");
                        writer.WriteLine("========================");
                    }
                }
            }
        }

        /// <summary>
        /// Demonstrates zip file compression format.
        /// </summary>
        static void ZipFileCompression()
        {
            string directoryWithFiles = CreatePath("Files");
            string zippedFiles = CreatePath("ZippedFiles");
            string unzippedFiles = CreatePath("UnzippedFiles");

            // Adds all the files in a specified directory into a ZIP file.
            ZipFile.CreateFromDirectory(directoryWithFiles, zippedFiles);

            // Extracts a zip file to a directory.
            ZipFile.ExtractToDirectory(zippedFiles, unzippedFiles);
        }

        /// <summary>
        /// Use a memory stream to compress entirely in memory asynchronously.
        /// </summary>
        /// <returns>-A task that is signaled upon completion.</returns>
        static async Task InMemoryCompressionAsync()
        {
            byte[] data = new byte[1000];

            MemoryStream ms = new MemoryStream();

            // The additional flat sent to `DeflateStream`'s constructor tells it not
            // to follow the usual protocol of taking the underlying stream with it
            // in disposal. In other words, the memory stream is left open, allowing
            // us to position to zero and  reread it.
            using (Stream ds = new DeflateStream(ms, CompressionMode.Compress, true))
                await ds.WriteAsync(data, 0, data.Length);

            byte[] compressed = ms.ToArray();

            WriteLine(
                "Compressed data ({0} bytes)in memory is now {1} bytes",
                data.Length,
                compressed.Length);

            // Reposition
            ms.Position = 0;

            using (Stream ds = new DeflateStream(ms, CompressionMode.Decompress))
                for (int i = 0; i < 1000; i += await ds.ReadAsync(data, i, 1000 - i)) { }
        }

        /// <summary>
        /// Use a memory stream to compress entirely in memory.
        /// </summary>
        static void InMemoryCompression()
        {
            // We can expect a good compression
            // ratio from an empty array.
            byte[] data = new byte[1000];

            var ms = new MemoryStream();
            using (Stream ds = new DeflateStream(ms, CompressionMode.Compress))
                ds.Write(data, 0, data.Length);

            // The above `using` statement around the 
            // `DeflateStream` closes it in a textbook 
            // fashion, flushing any unwritten buffers 
            // in the process. This also closes the
            // `MemoryStream` it wraps - meaning we
            // must then call `ToArray` to extract its
            // data.
            byte[] compressed = ms.ToArray();
            WriteLine(
                "Compressed data ({0} bytes)in memory is now {1} bytes",
                data.Length,
                compressed.Length);

            // Decompress back to data array.
            ms = new MemoryStream(compressed);
            using (Stream ds = new DeflateStream(ms, CompressionMode.Decompress))
                for (int i = 0; i < 1000; i += ds.Read(data, i, 1000 - i)) { }
        }

        /// <summary>
        /// Decompresses a text file.
        /// </summary>
        /// <param name="path">A path to the file to read from.</param>
        /// <returns>A task that is signaled upon completion or fault.</returns>
        static async Task DecompressTextFileAsync(string path)
        {
            using (Stream s = File.OpenRead(path))
            using (Stream ds = new DeflateStream(s, CompressionMode.Decompress))
            using (TextReader r = new StreamReader(ds))
            {
                string content = await r.ReadToEndAsync();
                WriteLine(content);

                string decompressedPath = CreatePath("Files/decompressed-text.bin");
                using (Stream fs = File.Create(decompressedPath))
                using (TextWriter w = new StreamWriter(fs))
                    for (int i = 0; i < 1000; i++)
                        await w.WriteAsync(content);

                WriteLine("The decompressed file is " + new FileInfo(decompressedPath).Length + " bytes");
            }  
        }

        /// <summary>
        /// Compresses a text file.
        /// </summary>
        /// <param name="path">A path to file to write to.</param>
        /// <returns>A task that is signaled upon completion or fault.</returns>
        static async Task CompressTextFileAsync(string path)
        {
            string[] words = "The quick brown fox jumps over the lazy dog".Split();
            Random rand = new Random();

            using (Stream s = File.Create(path))
            using (Stream ds = new DeflateStream(s, CompressionMode.Compress))
            using (TextWriter w = new StreamWriter(ds))
                for (int i = 0; i < 1000; i++)
                    await w.WriteAsync(words[rand.Next(words.Length)] + " ");

            WriteLine("The compressed file is " + new FileInfo(path).Length + " bytes");
        }

        /// <summary>
        /// Decompress a series of bytes from the file.
        /// </summary>
        /// <param name="path">A path to the file to read from.</param>
        static void DecompressFileStream(string path)
        {
            using (Stream s = File.OpenRead(path))
            using (Stream ds = new DeflateStream(s, CompressionMode.Decompress))
            {
                string decompressedFile = CreatePath("Files/decompressed.bin");
                var fs = File.Create(decompressedFile);
               
                for (byte i = 0; i < 100; i++)
                {
                    int b = ds.ReadByte();
                    fs.WriteByte((byte)b);
                }

                fs.Flush();
                fs.Close();

                WriteLine("The decompressed file is " + new FileInfo(decompressedFile).Length + " bytes");
            }    
        }

        /// <summary>
        /// Compresses a series of bytes to the file.
        /// </summary>
        /// <param name="path">A path to the file to write to</param>
        static void CompressFileStream(string path)
        {
            using (Stream s = File.Create(path))
            using (Stream ds = new DeflateStream(s, CompressionMode.Compress))
                for (byte i = 0; i < 100; i++)
                    ds.WriteByte(i);

            WriteLine("The compressed file is " + new FileInfo(path).Length + " bytes");
        }

        /// <summary>
        /// Compares the performance of <see cref="FileStream"/> with
        /// or without 
        /// </summary>
        static void BenchmarkBufferedStream()
        {
            // Coupling a `BufferedStream` to a
            // `FileStream`, is of limited value
            // because `FileStream` has an already
            // built-in buffering. Its only use
            // might be in enlarging the buffer
            // on a an already constructed `FileStream`.
            for (int i = 0; i < numberOfPerfomanceTests; i++)
            {
                RunFileStreamWithBuffering();
                WriteLine();
                RunFileStreamWihoutBuffering();
                WriteLine();
            }

            WriteLine();
            double bufferedRuntime = fsWithBs / numberOfPerfomanceTests / 1000;
            double unbufferedRuntime = fsWithoutBs / numberOfPerfomanceTests / 1000;
            DisplayInfo("Average runtime performance with buffer : " + bufferedRuntime + " micro seconds");
            DisplayInfo("Average runtime performance without buffer : " + unbufferedRuntime + " micro seconds");
            int performanceRatio = (int)(unbufferedRuntime / bufferedRuntime);
            if ( performanceRatio < 2)
                DisplayInfo(string.Format(
                    "FileStream with additional buffering is {0:P} faster than normal FileStream",
                    (unbufferedRuntime - bufferedRuntime) / unbufferedRuntime));
            else
                DisplayInfo(string.Format(
                    "FileStream with additional buffering is {0:P} times faster than normal FileStream",
                    performanceRatio));
        }

        /// <summary>
        /// Demonstrates how buffering improves performance by reducing
        /// round trips to the backing store via the read-ahead buffering.
        /// </summary>
        /// <exception cref="IOException">If there is an I/O issue.</exception>
        static void RunFileStreamWithBuffering()
        {
            string path = CreatePath("Files/myData1.bin");

            // Write 100KB to a file.
            File.WriteAllBytes(path, new byte[100_000]) ;

            int trackingId = TrackingId++;
            Stopwatch sw = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - entering", trackingId);

            // `BufferedStream` decorates or wraps, another stream with
            // buffering capability.

            // Wrap a `FileStream` in a 20 KB `BufferedStream`. Closing a
            // `BufferedStream` automatically closes the underlying backing
            // store stream, in this case, `FileStream`.
            using (FileStream fs = File.OpenRead(path))
            using(BufferedStream bs = new BufferedStream(fs, 20_000)) // creates a 20KB buffer
            {
                for (int i = 0; i < 90_000; i++)
                {
                    bs.ReadByte();
                    // The underlying backing store file stream advances 20,000 bytes
                    // after reading just 1 byte, thanks to the read-ahead buffering.
                    // We could call `bs.ReadByte` another 19,999 times before the
                    // `FileStream` would be hit again.
                }

                DisplayInfo("Cursor position for backing store : " + fs.Position);
            }

            sw.Stop();
            fsWithBs += sw.Elapsed.TotalMilliseconds * 1_000_000;
            DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - exiting", trackingId);
        }

        /// <summary>
        /// Demonstrates how not using buffering hurts performance.
        /// </summary>
        /// <exception cref="IOException">If there is an I/O issue.</exception>
        static void RunFileStreamWihoutBuffering()
        {

            string path = CreatePath("Files/myData2.bin");

            // Write 100KB to a file.
            File.WriteAllBytes(path, new byte[100_000]);

            int trackingId = TrackingId++;
            Stopwatch sw = Stopwatch.StartNew();
            DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - entering", trackingId);

            using (FileStream fs = File.OpenRead(path))
            {
                for (int i = 0; i < 90_000; i++)
                {
                    fs.ReadByte();
                    // The underlying backing store file stream advances 20,000 bytes
                    // after reading just 1 byte, thanks to the read-ahead buffering.
                    // We could call `bs.ReadByte` another 19,999 times before the
                    // `FileStream` would be hit again.
                }

                DisplayInfo("Cursor position for backing store : " + fs.Position);
            }

            sw.Stop();
            fsWithoutBs += sw.Elapsed.TotalMilliseconds * 1_000_000;
            DisplayCurrentMethodInfo(sw.Elapsed.ToString() + " - exiting", trackingId);
        }
    }
}
