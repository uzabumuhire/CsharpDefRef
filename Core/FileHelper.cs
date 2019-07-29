using System;
using System.IO;
using System.IO.Compression;

using static System.Console;

namespace Core
{
    public static class FileHelper
    {
        /// <summary>
        /// Create a full path from a relative path.
        /// </summary>
        /// <param name="relativePath">A relative path to the current Visual Studio project.</param>
        /// <returns>A full path.</returns>
        public static string CreateFullPath(string relativePath)
        {
            return Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                relativePath);
        }

        /// <summary>
        /// Create a full path from a relative path.
        /// </summary>
        /// <param name="relativePath">A relative path to the current base executable directory.</param>
        /// <returns>A full path.</returns>
        public static string CreatePath(string relativePath)
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                relativePath);
        }

        /// <summary>
        /// Display files in zipped directory at a given path.
        /// </summary>
        /// <param name="path">A path to the directory to read from.</param>
        public static void DisplayZippedFiles(string path)
        {
            // `ZipFile.Open` method for reading/writing individual entries.
            // Returns a `ZipArchive` object (which you can also obtain by
            // instantiating `ZipArchive` with a `Stream` object.
            // When calling `ZipArchive.Open`, you must specifiy a filename
            // and indicate whether you want to Read, Create or Update the
            // archive.
            using (ZipArchive zip = ZipFile.Open(path, ZipArchiveMode.Read))
            {
                WriteLine();
                WriteLine();
                string composite = "| {0,-50} | {1,20} |";

                WriteLine(string.Format(composite, "--------------------------------------------------", "--------------------"));
                WriteLine(string.Format(composite, "FILENAME", "FILE SIZE (bytes)"));
                WriteLine(string.Format(composite, "--------------------------------------------------", "--------------------"));

                // Enumeratre existing entries via the `Entries` property or
                // find a particular file with `GetEntry`.
                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    WriteLine(string.Format(composite, entry.FullName, entry.Length));
                    WriteLine(string.Format(composite, "--------------------------------------------------", "--------------------"));
                }
            }  
        }
    }
}
