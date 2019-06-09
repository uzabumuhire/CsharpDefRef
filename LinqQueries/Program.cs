using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace LinqQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            FilterNames1(new string[] { "Tom", "Dick", "Harry" }, 4);
            FilterNames2(new string[] { "Tom", "Dick", "Harry" }, "a");
            FilterNames3(new string[] { "Tom", "Dick", "Harry", "Mary", "Jay" }, "a");
        }

        // Extracts and prints all names with a given length
        static void FilterNames1(string[] names, int length)
        {
            //IEnumerable<string> filteredNames = Enumerable.Where(
            //    names, 
            //    n => n.Length >= length);

            // Because standard query operators are implemented as extension
            // methods, we can call `Where` directly on names since `names` is 
            // a `string` and a `string` implements `IEnumerable<string>`.  

            // The input argument corresponds to an input element. In this case,
            // input argument `n` represents each name in the array and is of 
            // type `string`. 

            // The `Where` operator requires that the lambda expression return
            // a `bool` value, which if `true`, indicate that the element should
            // be included in the output sequence `filteredNames`
            IEnumerable<string> filteredNames = names
                .Where(n => n.Length >= length);

            foreach (string fn in filteredNames)
                WriteLine(fn);
        }

        // Extracts and prints all names containing the given letter
        static void FilterNames2(string[] names, string letter)
        {
            //IEnumerable<string> filteredNames = names
            //    .Where(n => n.Contains("a"));

            // Using query expression syntax
            IEnumerable<string> filteredNames = from n in names
                                                where n.Contains(letter)
                                                select n;

            foreach (string fn in filteredNames)
                WriteLine(fn);
        }

        // Extracts all names containing the given letter, sorts them by their
        // length, converts them to uppercase and prints them
        static void FilterNames3(string[] names, string letter)
        {
            // The variable `n` is privately scoped to each of
            // the lamda expressions.

            // `Where`, `OrderBy` and `Select` are standard query operators 
            // that resolve to extension methods in the `Enumerable` class
            // in `System.Linq`.

            // `Where`  emits a filtered version of the  input sequence.
            // `OrderBy` emits a sorted version of its input sequence.
            // `Select` emits a sequence where each input element is 
            // transformed or projected with a given lambda expression.

            // Data flows from left to right through the chain of operators,
            // so the data is first filtered, then sorted, then projected.

            // A query operator never alters the input sequence, instead, it
            // returns a new sequence. This is consistent with the functional
            // programming paradigm.

            //IEnumerable<string> filteredNames = names
            //    .Where(n => n.Contains(letter))
            //    .OrderBy(n => n.Length)
            //    .Select(n => n.ToUpper());

            // Using query (expression) syntax
            IEnumerable<string> filteredNames =
                from    n in names
                where   n.Contains("a") // Filter elements
                orderby n.Length        // Sort elements
                select  n.ToUpper();    // Transform (project) each element    

            foreach (string fn in filteredNames)
                WriteLine(fn);
        }
    }
}
