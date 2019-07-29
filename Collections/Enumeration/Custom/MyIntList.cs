using System;
using System.Collections;

namespace Collections.Enumeration.Custom
{
    class MyIntList : IEnumerable
    {
        int[] data = { 16, 17, 18, 19, 20 };

        public IEnumerator GetEnumerator()
        {
            return new IntEnumerator(this);
        }

        // Define an inner class for the enumerator

        // This is what the compiler does behind the
        // scenes, in resolving iterators.

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

            // The first call to `MoveNext should move to the first 
            // (and not the second) item in the list.
            public bool MoveNext()
            {
                if (currentIndex >= collection.data.Length - 1)
                    return false;

                return ++currentIndex < collection.data.Length;
            }

            // Implementing `Reset` is optional 
            // You can throw a `NotSupportedException
            public void Reset()
            {
                currentIndex = -1;
            }
        }
    }
}
