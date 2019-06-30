using static System.Console;

using static Core.ConsoleHelper;

namespace Core
{
    public static class MatrixHelper
    {
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

        public static void Preview2DMatrix<T>(T[,] matrix2D, int preview)
        {
            int width = matrix2D.GetLength(0);
            int height = matrix2D.GetLength(1);
            for (int i = 0; i < 1 && i < width; i++)
            {
                for (int j = 0; j < preview && j < height; j++)
                {
                    DisplaySpaceVal(matrix2D[i, j]);
                }
            }
        }

        public static void Display1DMatrix<T>(T[] matrix1D)
        {
            for (int i = 0; i < matrix1D.Length; i++)
            {
                DisplaySpaceVal(matrix1D[i]);
            }
        }

        public static void Preview1DMatrix<T>(T[] matrix1D, int preview)
        {
            for (int i = 0; i < preview && i < matrix1D.Length; i++)
            {
                DisplaySpaceVal(matrix1D[i]);
            }
        }
    }
}
