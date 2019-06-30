using static Core.ConsoleHelper;

namespace Advanced.UnsafeCodePointers.Basics
{
    class UnsafeClass
    {
        UnsafeUnicodeString uus;
        internal UnsafeUnicodeString Uus
        {
            get;
        }

        internal UnsafeClass(string s)
        {
            uus.Length = (short)s.Length;
            unsafe
            {
                // The fixed `keyword` is used to
                // pin the object the object on the
                // heap that contains the buffer
                // which will be the instance of
                // `UnsafeClass`.

                // Hence `fixed` means two different
                // things : fixed in size (like the
                // `uus.Buffer`) and fixed in place
                // (like the instance of `UnsafeClass`).

                // The two are often used together,
                // in that a fixed-size buffer must
                // be fixed in place to be used.
                fixed (byte* cursor = uus.Buffer)
                    for (int i = 0; i < s.Length; i++)
                        cursor[i] = (byte)s[i]; // *cursor++ = (byte)s[i];
            }
        }

        internal void Display()
        {
            unsafe
            {
                fixed (byte* cursor = uus.Buffer)
                    for (int i = 0; i < uus.Length; i++)
                        DisplaySpaceVal(cursor[i]); // DisplaySpaceVal(*cursor++);
            }
        }
    }
}
