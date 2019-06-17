namespace Advanced.Delegates.GenericParameters
{
    delegate TResult Transformer<in T, out TResult>(T arg);

    static class Transformers
    {
        /// <summary>
        /// A generalized utility method that works on on any type <typeparamref name="T"/>.
        /// Applies a <see cref="Transformer{T}"/> delegate to each element in
        /// an array.
        /// </summary>
        /// <param name="values">A sequence of values of type <typeparamref name="T"/>.</param>
        /// <param name="t">A delegate  instance of <see cref="Transformer{T}"/> delegate 
        /// type, for specifying a plug-in transform.</param>
        internal static void ArrayTransform<T>(T[] values, Transformer<T, T> t)
        {
            // ArrayTransformer is a high-order function, because it's a 
            // function that takes a function as an argument.
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t(values[i]);
            }
        }
    }
}
