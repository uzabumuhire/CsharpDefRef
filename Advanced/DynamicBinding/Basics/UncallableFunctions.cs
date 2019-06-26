using static System.Console;

namespace Advanced.DynamicBinding.Basics
{
    class UncallableFunctions: IUncallableFunctions
    {
        void IUncallableFunctions.UF()
        {
            WriteLine("Called through the lens of IUncallabeFunctions interface.");
        }
    }
}
