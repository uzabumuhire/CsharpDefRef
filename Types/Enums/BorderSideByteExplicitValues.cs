
namespace Types.Enums
{
    // Specifies an explicit underlying value
    // for each enum member.

    /// <summary>
    /// Represents the side of the border.
    /// </summary>
    enum BorderSideByteExplicitValues : byte
    {
        Left = 1,
        Right = 2,
        Top = 10,
        Bottom = 11
    }

    // The compiler lets you explicitly assign some
    // of the enum members. The unassigned enum
    // keep incrementing from the last explicit
    // value. This is equivalent to the above code.

    /*
    enum BorderSideByteExplicitValues : byte
    {
        Left = 1,
        Right,
        Top = 10,
        Bottom
    }
    */
}
