using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Windows;
using System.Buffers;

namespace lab5
{
    [Serializable]
    public class Employee : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get
            { return name; }

            set
            {
                if (name != value)
                {
                    name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }

        }
        
        public int Age { get; set; }
        public double Salary { get; set; }
        public Department department { get; set; }

        public Employee()
        { }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{Name}";
        }

        public static void SaveXML(List<Employee> obj, string filename)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Employee>));
            Stream filestream = new FileStream(filename, FileMode.Create, FileAccess.Write);

            try
            {               
                xmlFormat.Serialize(filestream, obj);
            }
            catch (Exception e)
            {

                System.Windows.MessageBox.Show(e.Message);
            }
            
            filestream.Close();
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));                
                Console.Beep();
            }
        }

    }
}
