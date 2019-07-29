using System.Collections;
using System.Collections.Generic;

using static Core.ConsoleHelper;

namespace Core
{
    public static class CollectionsHelper
    {
        /// <summary>
        /// Displays elements of the enumerable collection with space.
        /// </summary>
        /// <param name="ec">enumerable collection</param>
        /// <typeparam name="T">type of the elements</typeparam>
        /// <exception cref="System.IO.IOException"></exception>
        /// <see cref="System.Console"/>
        public static void DisplayCollectionWithSpace<T>(IEnumerable<T> ec)
        {
            foreach (T item in ec)
            {
                DisplaySpaceVal(item);
            }
        }

        /// <summary>
        /// Displays elements of the enumerable collection without space.
        /// </summary>
        /// <param name="ec">enumerable collection</param>
        /// <typeparam name="T">type of the elements</typeparam>
        /// <exception cref="System.IO.IOException"></exception>
        /// <see cref="System.Console"/>
        public static void DisplayCollectionWithoutSpace<T>(IEnumerable<T> ec)
        {
            foreach (T item in ec)
            {
                DisplayVal(item, "");
            }
        }

        /// <summary>
        /// Counts unique elements in any collection by using the nongeneric 
        /// <see cref="IEnumerable"/> to provide type for collections
        /// accross all elements type. 
        /// </summary>
        /// <param name="collection">A given collection.</param>
        /// <returns>The total number of elements in <paramref name="collection"/></returns>
        public static int Count(IEnumerable collection)
        {
            // Because C# offers covariance with generic interfaces
            // it might seem valid to have this method instead accept
            // `IEnumerable<object>`. This, however, would fail with
            // value-type elements (covariance only works with elements for 
            // *reference conversions* not *boxing conversions*) and with 
            // legacy collections that don't implement `IEnumerable<T>`,
            // such as `ControlCollection` in WindowsForm.
            HashSet<object> elements = new HashSet<object>();
            return Count(collection, elements);
        }

        /// <summary>
        /// Uses a <see cref="HashSet{T}"/> to count unique elements in any
        /// collection recursively without having issues with cyclic
        /// references.
        /// </summary>
        /// <param name="collection">A given collection.</param>
        /// <param name="elements">A set to prevent cyclic references</param>
        /// <returns>The total number of elements in <paramref name="collection"/></returns>
        static int Count(IEnumerable collection, HashSet<object> elements)
        {
            int count = 0;
            foreach(object element in collection)
            {
                if (element.GetType().Name == typeof(string).Name)
                {
                    elements.Add(element);
                    count++;
                }   
                else
                {
                    // `var` keyword is used since we don't know
                    // the type that will be statically bound to
                    // `subCollection`.

                    // The `as` operator performs downcast that
                    // evaluates to null (rather than throw an
                    // exception) if the downcast fails.

                    var subCollection = element as IEnumerable;
                    if (subCollection != null)
                    {
                        count += Count(subCollection, elements);
                    }
                    else if (!elements.Contains(element))
                    {
                        // We use a `HashSet` to prevents cyclic references which 
                        // will cause infinite recursion and crash the method.
                        // That is if the element is already in the `HashSet`
                        // we don't recount it twice.

                        elements.Add(element);
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
