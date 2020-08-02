using System;
using System.Collections.Generic;

namespace task_1
{
    abstract class Worker : IComparable<Worker>
    {
        public string Name { get; }
        public string LastName { get; }        

        public Worker(string name, string lastName)
        {
            Name = name;
            LastName = lastName; 
        }

        public abstract void AveragePayment();        

        public int CompareTo(Worker w)
        {
            return this.LastName.CompareTo(w.LastName); ;           
        }
    }

    class LastNameComparator : IComparer<Worker>
    {
        public int Compare(Worker x, Worker y)
        {
            return string.Compare(x.LastName, y.LastName);
        }
    }
}
