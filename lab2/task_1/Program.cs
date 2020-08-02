using System;

namespace task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[4];
            workers[0] = new HourlyRateWorker("Михаил", "Янгель", 70);
            workers[1] = new HourlyRateWorker("Альберт", "Эйнштэйн", 100);
            workers[2] = new FixedRateWorker("Нильс", "Бор", 3000);
            workers[3] = new FixedRateWorker("Айзек", "Азимов", 1000);

            //Реализовано два интерфейса
            Console.WriteLine("******\nОтсортировали по фамилии\n******");
            // IComparer<Worker>
            Array.Sort(workers, new LastNameComparator());
            // IComparable<Worker>
            Array.Sort(workers);

            foreach (Worker w in workers)
            {
                Console.WriteLine($"{w.Name} {w.LastName}");
            }

            Console.WriteLine();
            Console.WriteLine($"******\nforeach работает для класса {typeof(FactoryWorker)}\n******");

            // Реализуем IEnumerable, IEnumerator
            FactoryWorker workersArr = new FactoryWorker();           
            foreach (var item in workersArr)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
