using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

namespace lab5
{
    [Serializable]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department()
        { }

        public override string ToString()
        {
            return $"{Name}";
        }

        //public static bool operator ==(Department x, Department y) => (x.Name == y.Name && x.Id == y.Id);
        //public static bool operator !=(Department x, Department y) => (x.Name != y.Name && x.Id == y.Id);

    }
}
