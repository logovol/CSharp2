using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public Department department { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
