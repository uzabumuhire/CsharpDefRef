using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections.Enumeration.Custom
{
    class MyGenericIntList : IEnumerable<int>
    {
        int[] data = { 21, 22, 23, 24, 25 };

        // The generic enumerator is compatible with both `IEnumerable` and
        // `IEnumerable<T>`. We implement the nongeneric `GetEnumerator` 
        // method explicitly to avoid a naming conflict.
        public IEnumerator<int> GetEnumerator()
        {
            return new GenericIntEnumerator(this);
        }

        // Hidden from the public interface.
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

            // With generics it is faster because `IEnumerator<int>.Current` doesn't 
            // require casting from `int` to `object`, and so avoids overhead of 
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

            // Given we don't need a `Disposeì method, it's good pratice to
            // implement it explicitly, so it's hidden from the public
            // interface
            void IDisposable.Dispose() { }
        }
    }
}
