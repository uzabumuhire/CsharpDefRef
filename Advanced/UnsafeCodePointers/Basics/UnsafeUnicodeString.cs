
namespace Advanced.UnsafeCodePointers.Basics
{
    unsafe struct UnsafeUnicodeString
    {
        internal short Length;

        // The fixed keyword has another use,
        // which is to create fixed-sized
        // buffers within strucs.

        // Allocate the block of 30 bytes.
        internal fixed byte Buffer[30];
    }
}
