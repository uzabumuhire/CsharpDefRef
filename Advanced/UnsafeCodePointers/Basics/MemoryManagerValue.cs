using static Core.ConsoleHelper;

namespace Advanced.UnsafeCodePointers.Basics
{
    struct MemoryManagerValue
    {
        int x;

        unsafe internal void PinManagedMemoryValue()
        {
            MemoryManagerValue mmv = new MemoryManagerValue();
            MemoryManagerValue* mmvPointer = &mmv;

            // C# also provides the C/C++ style pointer-to-member
            // operator, which can be used on structs.
            mmvPointer->x = 9;
            DisplaySpaceVal(mmv.x);
            DisplaySpaceVal(mmvPointer->x);
            DisplaySpaceVal((*mmvPointer).x);
        }
    }
}
