using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static task_3.Program;

// Лях Павел
// *Дан фрагмент программы:
// Dictionary<string, int> dict = new Dictionary<string, int>()
//   {
//     {"four",4 },
//     {"two",2 },
//     { "one",1 },
//     {"three",3 },
//   };
// var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
// foreach (var pair in d)
// {
//     Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
// }
//
// а.Свернуть обращение к OrderBy с использованием лямбда-выражения =>.
// b. * Развернуть обращение к OrderBy с использованием делегата.

namespace task_3
{
    class Program
    {
        // public delegate int Comparison<in T>(T x, T y);

        static void Main(string[] args)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
              {
                {"four",  4},
                {"two",   2},
                {"one",   1},
                {"three", 3},
              };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine();

            //////////////// --- a --- ////////////////
            var d1 = dict.OrderBy(x => x.Value);
            foreach (var pair in d1)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine();

            //////////////// --- b --- ////////////////
            Func<KeyValuePair<string, int>, int> predicate = new Func<KeyValuePair<string, int>, int>(Method);            
            var d2 = dict.OrderBy(predicate);
            foreach (var pair in d2)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.ReadLine();

       }   
        public static int Method(KeyValuePair<string, int> d1)
        {            
            return d1.Value;
        }        
    }
}
