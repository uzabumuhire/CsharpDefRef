namespace Basics.Arrays
{
    struct PointValue
    {
        // Defines a custom value type
        // with the `struct` keyword.

        // Value-type instances occupy 
        // precisely the memory required  
        // to store their fields.

        // The content of built-in value type
        // `int`, is 32 bits of data.

        // `PointValue` takes 8 bytes
        // of memory.

        public int X; // 4 bytes
        public int Y; // 4 bytes
    }
}
