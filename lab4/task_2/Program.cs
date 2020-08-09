using System;
using System.Collections.Generic;
using System.Linq;

// Лях Павел
// Дана коллекция List<T>.
// Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
// для целых чисел;
// *для обобщенной коллекции;
// **используя Linq.

namespace task_2
{

    class Numbers
    {
        public int Digit { get; set; }
        public Numbers(int x)
        {
            Digit = x;
        }
    }
        
    class Program
    {
        static void Main(string[] args)
        {

            //////////////// --- 1 --- ////////////////
            List<int> list = new List<int>() {0, 1, 2, 3, 4, 1, 4, 4, 5, 6, 7, 8, 9};
            var result1 = list.GroupBy(x => x);
            foreach (var item in result1)
            {
                Console.WriteLine($"{item.Key} {item.Count()}");
            }
            Console.WriteLine();

            //////////////// --- 2 --- ////////////////
            var result2 = Occurences(list);
            foreach (var item in result1)
            {
                Console.WriteLine($"{item.Key} {item.Count()}");
            }
            Console.WriteLine();

            //////////////// --- 3 --- ////////////////
            Numbers[] num =
            {
                new Numbers(0), new Numbers(1), new Numbers(2), new Numbers(3), new Numbers(4), new Numbers(1),
                new Numbers(4), new Numbers(4), new Numbers(5), new Numbers(6), new Numbers(7), new Numbers(8),
                new Numbers(9)
            };

            var result3 = from x in num
                          group x by x.Digit;

            foreach (var item in result3)
            {
                Console.WriteLine($"{item.Key} {item.Count()}");
            }
            Console.WriteLine();

            Console.ReadLine();
        }

        private static System.Collections.IEnumerable Occurences<T>(ICollection<T> list)
        {
            List<T> list1 = new List<T>();
            list1.AddRange(list);

            return list1.GroupBy(x => x);
        }
    }
}
