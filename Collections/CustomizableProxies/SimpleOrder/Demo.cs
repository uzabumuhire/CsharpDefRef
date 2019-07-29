using System;
using System.Collections.ObjectModel;

namespace Collections.CustomizableProxies.SimpleOrder
{
    internal class Demo
    {
        /// <summary>
        /// Demonstrate a cusotmized extension of <see cref="KeyedCollection{TKey, TItem}"/>.
        /// </summary>
        internal static void Run()
        {
            SimpleOrder weekly = new SimpleOrder();
            /*
            weekly.Changed += new EventHandler<SimpleOrderChangedEventArgs>(ChangedHandler);
            */
            weekly.Changed += ChangedHandler;

            // The Add method, inherited from Collection, takes OrderItem.
            weekly.Add(new OrderItem(110072674, "Widget", 400, 45.17));
            weekly.Add(new OrderItem(110072675, "Sprocket", 27, 5.3));
            weekly.Add(new OrderItem(101030411, "Motor", 10, 237.5));
            weekly.Add(new OrderItem(110072684, "Gear", 175, 5.17));

            Display(weekly);

            // The Contains method of KeyedCollection takes the key, 
            // type, in this case int.
            Console.WriteLine("\nContains(101030411): {0}",
                weekly.Contains(101030411));

            // The default Item property of KeyedCollection takes a key.
            Console.WriteLine("\nweekly[101030411].Description: {0}",
                weekly[101030411].Description);

            // The Remove method of KeyedCollection takes a key.
            Console.WriteLine("\nRemove(101030411)");
            weekly.Remove(101030411);
            Display(weekly);

            // The Insert method, inherited from Collection, takes an 
            // index and an OrderItem.
            Console.WriteLine("\nInsert(2, New OrderItem(...))");
            weekly.Insert(2, new OrderItem(111033401, "Nut", 10, .5));
            Display(weekly);

            // The default Item property is overloaded. One overload comes
            // from KeyedCollection<int, OrderItem>; that overload
            // is read-only, and takes Integer because it retrieves by key. 
            // The other overload comes from Collection<OrderItem>, the 
            // base class of KeyedCollection<int, OrderItem>; it 
            // retrieves by index, so it also takes an Integer. The compiler
            // uses the most-derived overload, from KeyedCollection, so the
            // only way to access SimpleOrder by index is to cast it to
            // Collection<OrderItem>. Otherwise the index is interpreted
            // as a key, and KeyNotFoundException is thrown.
            Collection<OrderItem> coweekly = weekly;
            Console.WriteLine("\ncoweekly[2].Description: {0}",
                coweekly[2].Description);

            Console.WriteLine("\ncoweekly[2] = new OrderItem(...)");
            coweekly[2] = new OrderItem(127700026, "Crank", 27, 5.98);

            OrderItem temp = coweekly[2];

            // The IndexOf method inherited from Collection<OrderItem> 
            // takes an OrderItem instead of a key
            // 
            Console.WriteLine("\nIndexOf(temp): {0}", weekly.IndexOf(temp));

            // The inherited Remove method also takes an OrderItem.
            //
            Console.WriteLine("\nRemove(temp)");
            weekly.Remove(temp);
            Display(weekly);

            Console.WriteLine("\nRemoveAt(0)");
            weekly.RemoveAt(0);
            Display(weekly);

            // Increase the quantity for a line item.
            Console.WriteLine("\ncoweekly[1].Quantity += 1000");
            coweekly[1].Quantity += 1000; // Does not fire the event.
            Display(weekly);

            Console.WriteLine("\ncoweekly(1) = New OrderItem(...)");
            coweekly[1] = new OrderItem(
                coweekly[1].PartNumber,
                coweekly[1].Description, 
                coweekly[1].Quantity + 1000,
                coweekly[1].UnitPrice);
            Display(weekly);

            Console.WriteLine();
            weekly.Clear();
        }

        static void Display(SimpleOrder order)
        {
            Console.WriteLine();
            foreach (OrderItem item in order)
            {
                Console.WriteLine(item);
            }
        }

        static void ChangedHandler(object source, SimpleOrderChangedEventArgs e)
        {
            OrderItem item = e.ChangedItem;

            if (e.ChangeType == ChangeType.Replaced)
            {
                OrderItem replacement = e.ReplacedWith;

                Console.WriteLine("{0} (quantity {1}) was replaced " +
                    "by {2}, (quantity {3}).", item.Description,
                    item.Quantity, replacement.Description,
                    replacement.Quantity);
            }
            else if (e.ChangeType == ChangeType.Cleared)
            {
                Console.WriteLine("The order list was cleared.");
            }
            else
            {
                Console.WriteLine("{0} (quantity {1}) was {2}.",
                    item.Description, item.Quantity, e.ChangeType);
            }
        }
    }
}
