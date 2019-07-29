using static System.Console;

using static Core.ConsoleHelper;

namespace Core
{
    public static class MatrixHelper
    {
        /// <summary>
        /// Displays a 2-dimensional matrix.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="matrix2D"/>.</typeparam>
        /// <param name="matrix2D">A 2-dimensional matrix.</param>
        public static void Display2DMatrix<T>(T[,] matrix2D)
        {
            int width = matrix2D.GetLength(0);
            int height = matrix2D.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    DisplaySpaceVal(matrix2D[i, j]);
                }
                WriteLine();
            }
        }

        /// <summary>
        /// Previews a 2-dimensional matrix.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="matrix2D"/>.</typeparam>
        /// <param name="matrix2D">A 2-dimensional matrix.</param>
        /// <param name="preview">The maximum number of elements to preview.</param>
        public static void Preview2DMatrix<T>(T[,] matrix2D, int preview)
        {
            int count = 0;
            int width = matrix2D.GetLength(0);
            int height = matrix2D.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (count < preview)
                    {
                        DisplaySpaceVal(matrix2D[i, j]);
                        count++;
                    }
                    else
                    {
                        WriteLine();
                        return;
                    }
                }
                WriteLine();
            }
        }

        /// <summary>
        /// Displays a 1-dimensional matrix.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="matrix1D"/>.</typeparam>
        /// <param name="matrix1D">A 1-dimensional matrix.</param>
        public static void Display1DMatrix<T>(T[] matrix1D)
        {
            for (int i = 0; i < matrix1D.Length; i++)
            {
                DisplaySpaceVal(matrix1D[i]);
            }
        }

        /// <summary>
        /// Previews a 1-dimensional matrix.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="matrix1D"/>.</typeparam>
        /// <param name="matrix1D">A 1-dimensional matrix.</param>
        /// <param name="preview">The maximum number of elements to preview.</param>
        public static void Preview1DMatrix<T>(T[] matrix1D, int preview)
        {
            for (int i = 0; i < preview && i < matrix1D.Length; i++)
            {
                DisplaySpaceVal(matrix1D[i]);
            }
        }

        /// <summary>
        /// Displays a jagged 2-dimensional array.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="jaggedMatrix2D"/>.</typeparam>
        /// <param name="jaggedMatrix2D">A jagged 2-dimensional array.</param>
        public static void Display2DJaggedMatrix<T>(T[][] jaggedMatrix2D)
        {
            for (int i = 0; i < jaggedMatrix2D.Length; i++)
            {
                for (int j = 0; j < jaggedMatrix2D[i].Length; j++)
                {
                    DisplaySpaceVal(jaggedMatrix2D[i][j]);
                }
                WriteLine();
            }
        }

        /// <summary>
        /// Previews a jagged 2-dimensional array.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="jaggedMatrix2D"/>.</typeparam>
        /// <param name="jaggedMatrix2D">A jagged 2-dimensional array.</param>
        /// <param name="preview">The maximum number of elements to preview.</param>
        public static void Preview2DJaggedMatrix<T>(T[][] jaggedMatrix2D, int preview)
        {
            int count = 0;
            for (int i = 0; i < jaggedMatrix2D.Length; i++)
            {
                for (int j = 0; j < jaggedMatrix2D[i].Length; j++)
                {
                    if (count < preview)
                    {
                        DisplaySpaceVal(jaggedMatrix2D[i][j]);
                        count++;
                    }
                    else
                    {
                        WriteLine();
                        return;
                    } 
                }
                WriteLine();
            }
        }
    }
}
