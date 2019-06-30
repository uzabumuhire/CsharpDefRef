using System.IO;

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
    }
}
