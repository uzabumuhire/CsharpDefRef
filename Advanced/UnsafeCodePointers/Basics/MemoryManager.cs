using static Core.ConsoleHelper;

namespace Advanced.UnsafeCodePointers.Basics
{
    /// <summary>
    /// <para>
    /// Typical usage of direct memory manipulation via pointers 
    /// within block of code marked unsafe and compiled with `/unsafe` 
    /// compiler option.
    /// </para>
    /// 
    /// <para>
    /// For every value type or reference type V, there is 
    /// a corresponding pointer type V*. A pointer instance holds
    /// the address of a variable. Pointers types can be (*unsafely*)
    /// cast to any other pointer type.
    /// </para>
    ///
    /// <para>
    /// The main pointer operators are :
    /// <list type="bullet">
    ///     <listheader>
    ///         <term>Operator</term>
    ///         <description>Meaning</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>&</c></term>
    ///         <description>The address-of operator returns a pointer
    ///         to the address of a variable.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>*</c></term>
    ///         <description>The dereference operator returns the variable
    ///         at the address of a pointer.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>-></c></term>
    ///         <description>The ponter-to-member operator is syntatic shortcut,
    ///         in which <c>x->y</c> is equivalent to <c>(*x).y</c>.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// Pointers types are primarily useful for interoperability
    /// with C/C++ APIs, but may also be used for accessing memory
    /// outside the managed heap or for performance-critical hotspots.
    /// </para>
    /// </summary>
    class MemoryManager
    {
        int x;

        /// <summary>
        /// Demonstrates the usage of the `fixed` statement to
        /// pin a managed object.
        /// </summary>
        internal void PinManagedObject()
        {
            MemoryManager mm = new MemoryManager();

            // By marking a type, type member or statement block
            // with `unsafe` keyword, you're permitted to use
            // pointer types and perform C/C++ style pointer
            // operations on memory within that scope.

            // Unsafe code can run faster than a corresponding
            // safe implementation. An unsafe C# method may
            // also be faster than calling an external C/C++
            // function or method, since there is no overhead
            // associated with leaving the managed execution
            // environment.
            unsafe
            {
                // The `fixed` statement is required to pin
                // managed object. During the execution of
                // a program, many objects are allocated and
                // deallocated from the heap. In order to avoid
                // unnecessary waste or fragmentation of memory,
                // the garbage collector moves objects around.
                // Pointing to an object is futile if its object
                // could change while referencing it, so the
                // `fixed` statement tells the garbage collector
                // to *pin* the object and not to move it around.
                // This may have an impact on the efficiency of
                // the runtime, so fixed blocks should be used
                // only briefly, and heap allocation should be
                // avoided within fixed block.

                // Within a `fixed` statement, you can get a pointer
                // to any value type, an array of value types or
                // a string. In the case of arrays and strings,
                // the pointer will actually point to the first
                // element, which is a value type.

                // Value types declared inline within reference types
                // require the reference type to be pinned, in this
                // case `mm` is pinned in order get a pointer to `x`
                // which is a value type.
                fixed (int* pointerToX = &mm.x)
                {
                    // Uses the *dereference* operator `*`
                    // to return the variable at the address
                    // of the pointer.
                    *pointerToX = 9; 
                }
            }

            DisplaySpaceVal(mm.x); 
        }
    }
}
