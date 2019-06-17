using System;
namespace Advanced.Delegates.Interfaces
{
    class Squarer : ITransformer<int, int>
    {
        public int Transform(int x) => x * x;
    }
}
