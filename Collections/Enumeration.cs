using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    static class Enumeration
    {
        internal static IEnumerable<int> IntegerGenerator()
        {
            // If we nothing above a simple IEnumerator<T> implementation,
            // the yield return statement allows for an easier variation.
            // Rather than wrting a class, you can move then iteration logic
            // into a method returning a generic IEnumerable<T> and let the
            // compiler take care of the rest.
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
            yield return 5;
        }

        internal static void EnumerateArrays1<T>(T[] data)
        {
            var e = ((IEnumerable<T>)data).GetEnumerator();
            while (e.MoveNext())
                Console.Write(e.Current + " ");
        }

        internal static void EnumerateArrays2<T>(T[] data)
        {
            foreach (T item in data)
                Console.Write(item + " ");
        }

        internal static void EnumerateString1(string s, string separator)
        {
            // Because string implements IEnumerable
            // we can call GetEnumerator()
            IEnumerator e = s.GetEnumerator();

            while (e.MoveNext())
            {
                char c = (char)e.Current;
                Console.Write(c + separator);
            }
        }

        internal static void EnumerateString2(string s, string separator)
        {
            // It is rare to call methods on enumerators directly
            // like in SplitString1, because C# provides a syntactic
            // shortcut: the `foreach` statement
            foreach (char c in s)
                Console.Write(c + separator);
        }
    }

    class MyCollection : IEnumerable
    {
        int[] data = { 6, 7, 8, 9, 10 };

        // GetEnumerator doesn't appear to return an enumerator at all. 
        // The compiler writes a hidden nested enumerator class behind 
        // the scenes, and then refactors GetEnumerator to instantiate
        // and return that class.
        public IEnumerator GetEnumerator()
        {
            foreach (var i in data)
                yield return i;
        }
    }

    class MyGenericCollection : IEnumerable<int>
    {
        int[] data = { 11, 12, 13, 14, 15 };

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var i in data)
                yield return i;
        }

        // Because IEnumerable<T> inherits from IEnumerable, we must implement
        // both the generic and nongeneric versions of GetEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Simply calls the generic GetEnumerator because IEnumerator<T>
            // inherits from IEnumerator
            return GetEnumerator();
        }
    }

    class MyIntList : IEnumerable
    {
        int[] data = { 16, 17, 18, 19, 20 };

        public IEnumerator GetEnumerator()
        {
            return new IntEnumerator(this);
        }

        // Define an inner class for the enumerator
        class IntEnumerator : IEnumerator
        {
            MyIntList collection;
            int currentIndex = -1;

            public IntEnumerator(MyIntList collection)
            {
                this.collection = collection;
            }

            public object Current
            {
                get
                {
                    if (currentIndex == -1)
                        throw new InvalidOperationException("Enumeration not started!");

                    if (currentIndex == collection.data.Length)
                        throw new InvalidOperationException("Past end of list!");

                    return collection.data[currentIndex];
                }
            }

            // The first call to MoveNext should move to the first 
            // (and not the second) item in the list.
            public bool MoveNext()
            {
                if (currentIndex >= collection.data.Length - 1)
                    return false;

                return ++currentIndex < collection.data.Length;
            }

            // Implementing Reset is optional 
            // You can throw a NotSupportedException
            public void Reset()
            {
                currentIndex = -1;
            }
        }
    }

    class MyGenericIntList : IEnumerable<int>
    {
        int[] data = { 21, 22, 23, 24, 25 };

        // The generic enumerator is compatible with both IEnumerable and
        // IEnumerable<T>. We implement the nongeneric GetEnumerator 
        // method explicitly to avoid a naming conflict.
        public IEnumerator<int> GetEnumerator()
        {
            return new GenericIntEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GenericIntEnumerator(this);
        }

        class GenericIntEnumerator : IEnumerator<int>
        {
            MyGenericIntList collection;
            int currentIndex = -1;

            public GenericIntEnumerator(MyGenericIntList collection)
            {
                this.collection = collection;
            }

            // With generics is faster because IEnumerator<int>.Current doesn't 
            // require casting from int to object, and so avoids overhead of 
            // boxing.
            public int Current
            {
                get
                {
                    if (currentIndex == -1)
                        throw new InvalidOperationException("Enumeration not started!");

                    if (currentIndex == collection.data.Length)
                        throw new InvalidOperationException("Past end of list!");

                    return collection.data[currentIndex];
                }
            }
            // Hidden from the public interface
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (currentIndex >= collection.data.Length - 1)
                    return false;

                return ++currentIndex < collection.data.Length;
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            // Given we don't need a Dispose method, it's good pratice to
            // implement it explicitly, so it's hidden from the public
            // interface
            void IDisposable.Dispose() { }
        }
    }
}