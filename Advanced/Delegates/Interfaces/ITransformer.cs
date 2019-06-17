namespace Advanced.Delegates.Interfaces
{
    interface ITransformer<in T, out TResult>
    {
        TResult Transform(T x);
    }
}
