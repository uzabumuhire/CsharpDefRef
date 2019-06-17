namespace Advanced.Delegates.Interfaces
{
    public class Cuber : ITransformer<int, int>
    {
        public int Transform(int x) => x * x * x;
    }
}
