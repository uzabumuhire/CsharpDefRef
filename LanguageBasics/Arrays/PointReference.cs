namespace Basics.Arrays
{
    class PointReference
    {
        // Defines a custom reference type
        // with the `class` keyword.

        // Reference types require separate
        // allocations of memory for the
        // reference and object.

        // The object consumes as many bytes 
        // as its  fields, plus additional 
        // administrative overhead.

        // The precise overhead is intrinsically
        // private to the implementation the
        // .NET runtime, but at a minimum, the
        // overhead is 8 bytes used to store a
        // key to the object's type, as well as
        // temporary information such as
        // its lock state for multi-threading
        // and a flag to indicate whether it
        // has been fixed from movement by
        // the garbage collector.

        // Each reference requires an extra 4 or
        // 8 bytes, depending on whether
        // the .NET runtime is running on
        // a 32- or 64-bit platform.

        // PointReference takes in memory
        // at least 24 bytes :
        // 8 bytes for the reference
        // 8 bytes for the object data +
        // at least 8 bytes additional overhead
        public int X; // 4 bytes
        public int Y; // 4 bytes
    }
}
