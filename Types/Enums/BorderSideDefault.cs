namespace Types.Enums
{
    // An enum is a special value type that lets you specify
    // a group of named numeric constants.

    // Each enum member has an underlying integral value.
    // By default :
    // - underlying values are of type `int`
    // - the constants 0, 1, 2... are automatically
    //   assigned, in the declaration order of the
    //   enum members.

    /// <summary>
    /// Represents the side of the border.
    /// </summary>
    enum BorderSideDefault
    {
        Left,
        Right,
        Top,
        Bottom
    }
}
