

namespace Basics.Types
{
    /// <summary>
    /// Serves as a blueprint for unit conversions.
    /// </summary>
    class UnitConverter
    {
        // A type holds data via *data members* and 
        // provide *function member* that use
        // that data.

        // The data member and function member that
        // operates on the *instance* of the type
        // are called instance members. 

        int ratio; // data member : field

        // A function member : constructor

        // A *constructor* is defined like a
        // method, except that the method
        // name and return types are reduced
        // to the name of the enclosing type.
        internal UnitConverter(int unitRatio)
        {
            ratio = unitRatio;
        }

        // A function member : method
        internal int Convert(int unit)
        {
            return unit * ratio;
        }
    }
}
