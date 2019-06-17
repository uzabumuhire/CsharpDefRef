namespace Advanced.Delegates.PlugInMethods
{
    delegate int Transformer(int x);

    static class Transformers
    {
        /// <summary>
        /// Applies a <see cref="Transformer"/> delegate to each element in
        /// an integer array.
        /// </summary>
        /// <param name="values">A sequence of integer values.</param>
        /// <param name="t">A delegate of <see cref="Transformer"/> delegate 
        /// type, for specifying a plug-in transform.</param>
        internal static void ArrayTransform(int[] values, Transformer t)
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
