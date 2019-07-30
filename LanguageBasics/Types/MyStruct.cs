namespace Basics.Types
{
    struct MyStruct
    {
        // Technically, the CLR positions fields
        // within the type at an address that's
        // a multiple of the fields' size (up to
        // a maximum of 8 bytes). Thus, the
        // following actually consumes 16 bytes
        // of memory (with the seven bytes
        // following the first field "wasted").

        public byte b;
        public long l;

        // This behavior can be overriden with
        // `StructLayout` attribute, used to
        // map a struct to unmanaged memory.
    }
}
