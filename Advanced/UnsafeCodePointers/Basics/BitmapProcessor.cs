using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Advanced.UnsafeCodePointers.Basics
{
    internal delegate void BitmapImageFilter(byte[] bitmap);

    static class BitmapProcessor
    {
        /// <summary>
        /// Applies a blue filter to a 1D representation of a bitmap image.
        /// </summary>
        /// <param name="bitmap">A 2D representation of a bitmap image.</param>
        internal static void BlueColorFilter(byte[] bitmap)
        {
            ArgbFilter(bitmap, 0, 255);
        }

        internal static void RedColorFilter(byte[] bitmap)
        {
            ArgbFilter(bitmap, 2, 255);
        }

        internal static void GreenColorFilter(byte[] bitmap)
        {
            ArgbFilter(bitmap, 1, 255);
        }

        /// <summary>
        /// Applies a transparency filter to a 1D representation of a bitmap image.
        /// </summary>
        /// <param name="bitmap">A 2D representation of a bitmap image.</param>
        internal static void TransparencyFilter(byte[] bitmap)
        {
            ArgbFilter(bitmap, 3, 100);
        }

        internal unsafe static void ArgbFilter(byte[] bitmap, int argbIndex, byte argbFilter)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;

                for (int i = argbIndex; i < length; i += 4)
                {
                    cursor[i] = argbFilter; // *cursor++ = argbFilter;
                }
            }
        }

        internal unsafe static void InvertColorSwapFilter(byte[] bitmap)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;
            byte sourceBlue = 0, sourceGreen = 0, sourceRed = 0, maxValue = 255;

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;
                for (int i = 0; i < length; i += 4)
                {
                    sourceBlue = cursor[i]; 
                    sourceGreen = cursor[i + 1]; 
                    sourceRed = cursor[i + 2]; 

                    cursor[i] = (byte)(maxValue - sourceBlue);
                    cursor[i + 1] = (byte)(maxValue - sourceGreen);
                    cursor[i + 2] = (byte)(maxValue - sourceRed);
                }
            }
        }

        internal unsafe static void HalfColorSwapFilter(byte[] bitmap)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;
            byte sourceBlue = 0, sourceGreen = 0, sourceRed = 0, half = 2; 

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;
                for (int i = 0; i < length; i += 4)
                {
                    sourceBlue = cursor[i];
                    sourceGreen = cursor[i + 1];
                    sourceRed = cursor[i + 2];

                    cursor[i] = (byte)(sourceBlue / half);
                    cursor[i + 1] = (byte)(sourceGreen / half);
                    cursor[i + 2] = (byte)(sourceRed / half);
                }
            }
        }

        internal unsafe static void ShiftRightColorSwapFilter(byte[] bitmap)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;
            byte sourceBlue = 0, sourceGreen = 0, sourceRed = 0;

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;
                for (int i = 0; i < length; i += 4)
                {
                    sourceBlue = cursor[i];
                    sourceGreen = cursor[i + 1];
                    sourceRed = cursor[i + 2];

                    cursor[i] = sourceRed;
                    cursor[i + 1] = sourceBlue;
                    cursor[i + 2] = sourceGreen;
                }
            }
        }

        internal unsafe static void ShiftLeftColorSwapFilter(byte[] bitmap)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;
            byte sourceBlue = 0, sourceGreen = 0, sourceRed = 0;

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;
                for (int i = 0; i < length; i += 4)
                {
                    sourceBlue = cursor[i];
                    sourceGreen = cursor[i + 1];
                    sourceRed = cursor[i + 2];

                    cursor[i] = sourceGreen;
                    cursor[i + 1] = sourceRed;
                    cursor[i + 2] = sourceBlue;
                }
            }
        }

        internal unsafe static void BlueRedColorSwapFilter(byte[] bitmap)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;
            byte sourceBlue = 0, sourceRed = 0;

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;
                for (int i = 0; i < length; i += 4)
                {
                    sourceBlue = cursor[i];
                    sourceRed = cursor[i + 2];

                    cursor[i] = sourceRed;
                    cursor[i + 2] = sourceBlue;
                }
            }
        }

        internal unsafe static void BlueGreenColorSwapFilter(byte[] bitmap)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;
            byte sourceBlue = 0, sourceGreen = 0;

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;
                for (int i = 0; i < length; i += 4)
                {
                    sourceBlue = cursor[i];
                    sourceGreen = cursor[i + 1];

                    cursor[i] = sourceGreen;
                    cursor[i + 1] = sourceBlue;
                }
            }
        }

        internal unsafe static void RedGreenColorSwapFilter(byte[] bitmap)
        {
            // In C# an Image’s ARGB components are actually
            // stored in the format Blue, Green, Red, Alpha.

            int length = bitmap.Length;
            byte sourceGreen = 0, sourceRed = 0;

            fixed (byte* address = bitmap)
            {
                byte* cursor = address;
                for (int i = 0; i < length; i += 4)
                {
                    sourceGreen = cursor[i + 1];
                    sourceRed = cursor[i + 2];

                    cursor[i + 1] = sourceRed;
                    cursor[i + 2] = sourceGreen;
                }
            }
        }

        /// <summary>
        /// Converts an <paramref name="image"/> to 2D integer array.
        /// </summary>
        /// <param name="image">The <see cref="Bitmap"/> image to convert.</param>
        /// <returns>2D array representing the <paramref name="image"/>.</returns>
        internal unsafe static int[,] BitmapToArray2D(Bitmap image)
        {
            int[,] array2D = new int[image.Width, image.Height];

            BitmapData bitmapData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            byte* address = (byte*)bitmapData.Scan0; // the first pixel data in a bitmap

            int paddingOffset = bitmapData.Stride - (image.Width * 4);//4 bytes per pixel

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    byte[] temp = new byte[4];
                    temp[0] = address[0];
                    temp[1] = address[1];
                    temp[2] = address[2];
                    temp[3] = address[3];
                    array2D[i, j] = BitConverter.ToInt32(temp, 0);
                    ////array2D[i, j] = (int)address[0];

                    //4 bytes per pixel
                    address += 4;
                }

                address += paddingOffset;
            }

            image.UnlockBits(bitmapData);

            return array2D;
        }

        /// <summary>
        /// Converts an <paramref name="image"/> to 1D byte array.
        /// </summary>
        /// <param name="image">The <see cref="Bitmap"/> image to convert.</param>
        /// <returns>1D byte array representing the <paramref name="image"/>.</returns>
        internal unsafe static byte[] BitmapToArray1D(Bitmap image)
        {
            byte[] array1D = new byte[image.Width * image.Height * 4];

            BitmapData bitmapData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            byte* address = (byte*)bitmapData.Scan0;

            for (int i = 0; i < array1D.Length; i++)
            {
                array1D[i] = address[i];
            }

            image.UnlockBits(bitmapData);

            return array1D;
        }

        /// <summary>
        /// Converts 1D byte array to a 2D integer array.
        /// </summary>
        /// <param name="array1D">A given 1D byte array</param>
        /// <param name="width">The width of the 2D integer array to be returned.</param>
        /// <param name="height">The height of the 2D integer array to be returned.</param>
        /// <returns>Returns a 2D integer array from <paramref name="array1D"/> with
        /// dimension <paramref name="width"/> x <paramref name="height"/></returns>
        internal unsafe static int[,] Array1DToArray2D(byte[] array1D, int width, int height)
        {
            int[,] array2D = new int[width, height];

                fixed (byte* address = array1D)
                {
                    byte* cursor = address;

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            byte[] temp = new byte[4];
                            temp[0] = cursor[0];
                            temp[1] = cursor[1];
                            temp[2] = cursor[2];
                            temp[3] = cursor[3];
                            array2D[i, j] = BitConverter.ToInt32(temp, 0);

                            //4 bytes per pixel
                            cursor += 4;
                        }
                    }
                } 

            return array2D;
        }

        /// <summary>
        /// Converts a 2D array of integers into a <see cref="Bitmap"/> image.
        /// </summary>
        /// <param name="integers">A representation of an image in a 2D array of integers.</param>
        /// <returns>A <see cref="Bitmap"/> image from the 2D array of integers.</returns>
        internal unsafe static Bitmap Array2DToBitmap(int[,] integers)
        {
            int width = integers.GetLength(0);
            int height = integers.GetLength(1);

            int stride = width * 4;//int == 4-bytes

            Bitmap bitmap = null;

            fixed (int* intPtr = &integers[0, 0])
            {
                bitmap = new Bitmap(
                    width,
                    height,
                    stride,
                    PixelFormat.Format32bppArgb,
                    new IntPtr(intPtr));
            }

            return bitmap;
        }
    }
}
