using System;
using System.Drawing;

using static Core.ConsoleHelper;
using static Core.FileHelper;

namespace Advanced.UnsafeCodePointers.Basics
{
    static class Demo
    {
        internal static void Test()
        {
            // Get original image
            string original = CreateFullPath("UnsafeCodePointers/Basics/Images/original.bmp");

            Bitmap image = new Bitmap(original);

            // Applies an inverted color filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.InvertColorSwapFilter, "inverted-color-filtered");

            // Applies a transparency filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.TransparencyFilter, "transparency-filtered");

            // Applies a blue color filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.BlueColorFilter, "blue-color-filtered");

            // Applies a red color filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.RedColorFilter, "red-color-filtered");

            // Applies a green color filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.GreenColorFilter, "green-color-filtered");

            // Applies a half color swap filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.HalfColorSwapFilter, "half-color-swap-filtered");

            // Applies a shift right color swap filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.ShiftRightColorSwapFilter, "shift-right-color-swap-filtered");

            // Applies a shift left color swap filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.ShiftLeftColorSwapFilter, "shift-left-color-swap-filtered");

            // Applies a blue and red color swap filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.BlueRedColorSwapFilter, "blue-red-color-swap-filtered");

            // Applies a blue and green color swap filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.BlueGreenColorSwapFilter, "blue-green-color-swap-filtered");

            // Applies a red and green color swap filter to the original image
            ApplyBitmapImageFilter(image, BitmapProcessor.RedGreenColorSwapFilter, "red-green-color-swap-filtered");

            MemoryManager mmDemo = new MemoryManager();
            mmDemo.PinManagedObject();

            DisplayBar();

            MemoryManagerValue mmvDemo = new MemoryManagerValue();
            mmvDemo.PinManagedMemoryValue();

            DisplayBar();

            AllocateStackBlockMemory(10);

            DisplayBar();

            UnsafeClass myUnsafeName = new UnsafeClass("Jean Jacques Uzabumuhire");
            myUnsafeName.Display();

            DisplayBar();

            short[] a = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
            RawMemoryManager(a);
        }

        /// <summary>
        /// Apply a <paramref name="filter"/> to a given <see cref="Bitmap"/> <paramref name="image"/>
        /// and save the image with the specified output name.
        /// </summary>
        /// <param name="image">The image in bitmap format.</param>
        /// <param name="filter">The filter to apply to the <paramref name="image"/>.</param>
        /// <param name="outputImageName">The output name of the filtered image.</param>
        static void ApplyBitmapImageFilter(Bitmap image, BitmapImageFilter filter, string outputImageName)
        {
            string outputPath = CreateFullPath("UnsafeCodePointers/Basics/Images/" + outputImageName + ".bmp");

            int[,] matrix;
            byte[] bitmap;
            try
            {
                matrix = BitmapProcessor.BitmapToArray2D(image);
                //Preview2DMatrix(matrix, 8);
                //WriteLine();

                bitmap = BitmapProcessor.BitmapToArray1D(image);
                //Preview1DMatrix(bitmap, 32);
                //WriteLine();

                filter(bitmap);
                //Preview1DMatrix(bitmap, 32);
                //WriteLine();

                matrix = BitmapProcessor.Array1DToArray2D(bitmap, image.Width, image.Height);
                //Preview2DMatrix(matrix, 8);
                //WriteLine();

                BitmapProcessor.Array2DToBitmap(matrix).Save(outputPath);
            }
            catch (Exception ex)
            {
                DisplayError(ex.ToString());
            }
        }

        /// <summary>
        /// Allocates memory in a block of a given <paramref name="size"/>.
        /// </summary>
        /// <param name="size">The size of the allocated memory block.</param>
        static void AllocateStackBlockMemory(int size)
        {
            unsafe
            {
                // Uses the `stackalloc` keyword to
                // allocate memory in a block on the
                // stack. Since it is allocated on
                // the stack, its lifetime is limited
                // to the execution of the method,
                // just as with any other local variable
                // (whose life hasn't been extended
                // by virtue of being captured by a
                // lambda expression, iterator block,
                // or asynchronous function.
                int* a = stackalloc int[size];
                for(int i = 0; i < size; ++i)
                {
                    // Allocated memory block
                    // may use the [] operator
                    // to index into memory.
                    DisplaySpaceVal(a[i]); // DisplaySpaceVal(*a++);
                }  
            }
        }

        unsafe static void RawMemoryManager(short[] a)
        {
            // Displays the original array
            foreach (short x in a)
                DisplaySpaceVal(x);

            DisplayBar();

            fixed (short* ptrToRawMemory = a)
            {
                // `sizeof` returns the size of value-type in bytes.
                Zap(ptrToRawMemory, a.Length * sizeof(short));
            }

            DisplayBar();

            // Displays the modified arrayß
            foreach (short x in a)
                DisplaySpaceVal(x);
        }

        /// <summary>
        /// Demonstrates how to deal with raw memory.
        /// </summary>
        /// <param name="memory">A pointer to raw memory.</param>
        /// <param name="byteCount">The size in bytes of the raw <paramref name="memory"/>.</param>
        unsafe static void Zap(void* memory, int byteCount)
        {
            // A void pointer (`void*`) makes no assumptions about the type
            // of the underlying data and is useful for functions that deal
            // with raw memory. An implicit conversion exist from  any
            // pointer type to `void*`. A `void*` cannot be dereferenced,
            // and arthimetic operations cannot be performed on void pointers.
            byte* cursor = (byte*)memory;

            // Print raw memory.
            for (int i = 0; i < byteCount; i++)
                DisplaySpaceVal(cursor[i]); // DisplaySpaceVal(*cursor++);

            // Initializes raw memory to 0 (short is 2 * byte)
            for (int i = 0; i < byteCount; i += 2) 
                cursor[i] = 0;
        }
    }
}
