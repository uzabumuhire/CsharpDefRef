using System.IO;
using System.Text;

using static System.Console;

using static Core.FileHelper;
using static Core.ConsoleHelper;

namespace StreamsIO.Adapters
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of stream adapters.
        /// </summary>
        internal static void Test()
        {
            try
            {
                string path1 = CreatePath("Files/test-decorator-stream-writer-reader1.txt");
                WriteToTextFileWithStreamWriter(path1);
                ReadFromTextFileWithStreamReader(path1);

                string path2 = CreatePath("Files/test-decorator-stream-writer-reader2.txt");
                WriteToTextFileWithFile(path2);
                AppendToTextFileWithFile(path2);
                ReadFromTextFileWithFile(path2);

                string path3 = CreatePath("Files/data-decorator-stream-writer-reader.txt");
                WriteDataToTextFileWithFile(path3);
                ReadDataFromTextFileWithFile(path3);

                string path4 = CreatePath("Files/char-decorator-stream-utf8.txt");
                WriteCharsToTextFileUTF8(path4);
                ReadBytesFromStreamUTF8(path4);

                string path5 = CreatePath("Files/char-decorator-stream-utf16.txt");
                WriteCharsToTextFileUTF16(path5);
                ReadBytesFromStreamUTF16(path5);

                string path6 = CreatePath("Files/people.data");
                SavePeopleData(path6);
                LoadPeopeData(path6);

                // Closing and disposing stream adapters.

                // `Close` and `Dispose` are synonymous with adapters,
                // as they are with streams.

                // You can close the adapter only or close the adapter, and 
                // then close the stream. This is semantically identical since
                // closing an adapter automatically close the underlying stream.
                // Whenever you nest `using` statements, disposal happens from
                // the inside out : the adapter is first closed, and then the
                // stream. Furthermore, if an exception is thrown from within
                // the adapter's constructor, the stream still closes.
                // Never close a stream before closing or flushing its writer
                // for you will amputate any data that's buffered in the adapter.
                using (FileStream fs = File.Create(CreatePath("temp.txt")))
                using (TextWriter writer = new StreamWriter(fs))
                    writer.WriteLine("Line");

                // For writers, you can flush the adapter, and then close the 
                // stream. For reader, you can close just the stream.
                // Because adapters are in the unusual category of *optional*
                // disposal objects.
                // Sometimes, you might choose not to dispose an adapter so that
                // when you've finished with the adapter, you want to leave the
                // underlying stream open for subsequent use.
                using (FileStream fs = new FileStream("temp.txt", FileMode.Create))
                {
                    StreamWriter writer = new StreamWriter(fs);

                    // Write to a file.
                    writer.WriteLine("Hello");

                    // We call `Flush` to ensure that
                    // the `StreamWriter`'s buffer is
                    // written to the underlying stream.
                    // If we disposed the `StreamWriter`,
                    // it would also close the underlying
                    // `FileStream`, causing the subsequent
                    // read to fail.
                    writer.Flush();

                    // Reposition the stream.
                    fs.Position = 0;

                    // Read the first byte before closing the stream.
                    WriteLine((char)fs.ReadByte());
                }

                // Stream adapters - with their optional disposal semantics -
                // do not implement the extended disposal pattern where the
                // finalizer calls `Dispose`. This allows an abandoned adapter
                // to evade automatic disposal when the garbage collector
                // catches up with it.

                // From Framework 4.5, there's a new constructor on
                // `StreamReader`/`StreamWriter` that instructs it to
                // keep the stream open after disposal.
                // Hence we can rewrite the preceding example as follows:
                using (var fs = new FileStream("temp.txt", FileMode.Create))
                {
                    using (var writer = new StreamWriter(fs, new UTF8Encoding(false, true), 0x100, true))
                        writer.WriteLine("Hello");

                    // The stream is still open after disposal.
                    fs.Position = 0;
                    WriteLine((char)fs.ReadByte());
                    WriteLine(fs.Length);
                }

            }
            catch (IOException ex)
            {
                DisplayError(ex.ToString());
            }
            
        }

        /// <summary>
        /// Uses <see cref="StreamWriter"/> to write two lines of text to a file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void WriteToTextFileWithStreamWriter(string path)
        {
            using (FileStream fs = File.Create(path))
            using (TextWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Line1");
                writer.WriteLine("Line2");
            }
        }

        /// <summary>
        /// Uses <see cref="StreamReader"/> to read two lines of text from a file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void ReadFromTextFileWithStreamReader(string path)
        {
            using(FileStream fs = File.OpenRead(path))
            using(TextReader reader = new StreamReader(fs))
            {
                WriteLine(reader.ReadLine());
                WriteLine(reader.ReadLine());
            }
        }

        /// <summary>
        /// Uses <see cref="File.CreateText"/> to get a <see cref="StreamWriter"/> used to
        /// write two lines of text to a file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void WriteToTextFileWithFile(string path)
        {
            using (TextWriter writer = File.CreateText(path))
            {
                writer.WriteLine("Line1");
                writer.WriteLine("Line2");
            }
        }

        /// <summary>
        /// Uses <see cref="File.AppendText"/> to get a <see cref="StreamWriter"/> used to
        /// append one line of text to a file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void AppendToTextFileWithFile(string path)
        {
            using (TextWriter writer = File.AppendText(path))
                writer.WriteLine("Line3");
        }

        /// <summary>
        /// Uses <see cref="File.OpenText"/> to get a <see cref="StreamReader"/> used to
        /// read from the text file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void ReadFromTextFileWithFile(string path)
        {
            // Illustrate how to test for the end of a file via
            // `reader.Peek()`. Another option is to read until
            // `reader.ReadLine()` returns null.
            using (TextReader reader = File.OpenText(path))
                while (reader.Peek() > -1)
                    WriteLine(reader.ReadLine());
        }

        /// <summary>
        /// Writes integer and boolean values to a text file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void WriteDataToTextFileWithFile(string path)
        {
            using (TextWriter w = File.CreateText(path))
            {
                // Writes other type such integers or boolean.
                w.WriteLine(123);
                w.WriteLine(true);
            }
        }

        /// <summary>
        /// Reads integer and boolean values from a text file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void ReadDataFromTextFileWithFile(string path)
        {
            using (TextReader r = File.OpenText(path))
            {
                // Because `TextWriter` invokes `ToString` on
                // the type to write, you must parse a string
                // when reading it back.
                int myInt = int.Parse(r.ReadLine());
                WriteLine(2 * myInt);
                bool truthy = bool.Parse(r.ReadLine());
                WriteLine(!truthy == false);
            }
        }

        /// <summary>
        /// Writes characters to at text file using UTF-8.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void WriteCharsToTextFileUTF8(string path)
        {
            // `File.CreateText` use default UTF-8 encoding.
            using (TextWriter w = File.CreateText(path))
                w.WriteLine("danish special alphabet æ ø å");
        }

        /// <summary>
        /// Reads bytes from the stream that was encoded using UTF-8.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void ReadBytesFromStreamUTF8(string path)
        {
            // æ, ø, å characters requires more than a single
            // byte to encode in UTF-8 (in this case, two).
            // UTF-8 is efficient with the Western alphabet as
            // most popular characters consume just one byte.
            // It also downgrades easily to ASCII simply by
            // ignoring all bytes above 127. Its disadvantage
            // is that seeking within a stream is troublesome,
            // since a character's position does not correspond
            // to its byte position in the stream.
            using (Stream s = File.OpenRead(path))
                for (int b; (b = s.ReadByte()) > -1;)
                    WriteLine(b + " " + (char) b);
        }

        /// <summary>
        /// Writes characters to at text file using UTF-16.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void WriteCharsToTextFileUTF16(string path)
        {
            // An alternative is UTF-16 (labeled `Unicode`) in
            // the `Encoding` class.
            using (Stream s = File.Create(path))
            using (TextWriter w = new StreamWriter(s, Encoding.Unicode))
                w.WriteLine("danish special alphabet æ ø å");
        }

        /// <summary>
        /// Reads bytes from the stream that was encoded using UTF-16.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        static void ReadBytesFromStreamUTF16(string path)
        {
            // UTF-16 uses either 2 or 4 bytes per character.
            // There are close to a million Unicode characters
            // allocated or reserved, so 2 bytes is not always
            // enough. However, because the C# `char` type is
            // itself 16 bits wide, a UTF-16 encoding will
            // always use exactly 2 bytes per .NET `char`.
            // This makes it easy to jump to a particular
            // character index within a stream.

            // UTF-16 uses a 2-byte prefix to identify whether
            // the byte pairs are written in *little-endian* or
            // *big-endian* order (the least significant byte
            // first or the most significant byte first).
            // The little-endian order is standard for
            // Windows-based systems.
            foreach (byte b in File.ReadAllBytes(path))
                WriteLine(b + " " + (char)b);
        }

        /// <summary>
        /// Save people data.
        /// </summary>
        /// <param name="path">The path to the file to write to.</param>
        /// <exception cref="IOException">If there is an IO issue.</exception>
        /// <exception cref="DirectoryNotFoundException">If the directory does not exist.</exception>
        /// <exception cref="PathTooLongException">If the specified path is too long.</exception>
        static void SavePeopleData(string path)
        {
            using (Stream s = File.Create(path))
            {
                new Person()
                {
                    Name = "Jean Jacques Uzabumuhire",
                    Age = 37,
                    Height = 1.78
                }
                .SaveData(s);

                new Person()
                {
                    Name = "Marie Ange Gahozo",
                    Age = 31,
                    Height = 1.70
                }
                .SaveData(s);

                new Person()
                {
                    Name = "Avery-Rose Isimbi Nefertiti Uzabumuhire",
                    Age = 1,
                    Height = 0.96
                }
                .SaveData(s);
            }
        }

        /// <summary>
        /// Loads the people data.
        /// </summary>
        /// <param name="path">A path where to load the data from.</param>
        static void LoadPeopeData(string path)
        {
            using (Stream s = File.OpenRead(path))
            {
                WriteLine(new Person(s));
                WriteLine(new Person(s));
                WriteLine(new Person(s));
            }
        }


    }
}
