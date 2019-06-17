namespace Advanced.Delegates.Interfaces
{
    public static class Transformers
    {
        /// <summary>
        /// A generalized utility method that works on any type <typeparamref name="T"/>.
        /// Applies a <see cref="ITransformer{T, T}"/> to each element in an array.
        /// </summary>
        /// <param name="values">A sequence of values of type 
        /// <typeparamref name="T"/>.</param>
        /// <param name="t">An instance of <see cref="ITransformer{T, T}"/> 
        /// for specifying a plug-in transform.</param>
        internal static void ArrayTransform<T>(T[] values, ITransformer<T, T> t)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t.Transform(values[i]);
            }
        }
    }
}
