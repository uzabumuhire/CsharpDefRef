using System;
using System.Collections.Generic;

namespace Advanced.ExtensionMethods.Basics
{
    /// <summary>
    /// Extension methods for dealing with sequences.
    /// A sequence is any object that implements <see cref="IEnumerable{T}"/>.
    /// An element is each item in the sequence. 
    /// </summary>
    static class SequenceHelper
    {
        /// <summary>
        /// An extension method for <see cref="string"/> to get
        /// the first element of a given <paramref name="sequence"/>
        /// </summary>
        /// <typeparam name="T">The type of the element in the sequence.</typeparam>
        /// <param name="sequence">An enumerable collection of items.</param>
        /// <returns>The first element of <paramref name="sequence"/>.</returns>
        /// <exception cref="InvalidOperationException">If the sequence is empty.</exception>
        /// <exception cref="NullReferenceException">If the sequence is null.</exception>
        internal static T First<T>(this IEnumerable<T> sequence)
        {
            foreach (T element in sequence)
            {
                return element;
            }

            throw new InvalidOperationException("No elements");
        }
    }
}
