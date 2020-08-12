using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        public Employee Empl { get; set; }
        public List<Employee> listE { get; set; }
        public ObservableCollection<Employee> itemsE { get; set; }

        public EditWindow()
        {            
            InitializeComponent();            
        }

        private void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            bool result1 =  int.TryParse(tbAge.Text, out int resultAge);
            bool result2 = double.TryParse(tbAge.Text, out double resultSalary);

            if (result1 && result2 && cbDepartment.SelectedItem != null
                && tbName.Text.Length > 0 && tbAge.Text.Length > 0 && tbSalary.Text.Length > 0)
            {
                if (btn.Content == "Сохранить")
                {
                    {
                        Empl.Name = tbName.Text;
                        Empl.Age = resultAge;
                        Empl.Salary = resultSalary;
                        Empl.department = (Department)cbDepartment.SelectedItem;
                        Close();                        
                    }

                }
                else if (btn.Content == "Добавить")
                {
                    Empl = new Employee();
                    Empl.Id = int.Parse(tbID.Text);
                    Empl.Name = tbName.Text;
                    Empl.Age = resultAge;
                    Empl.Salary = resultSalary;
                    Empl.department = (Department)cbDepartment.SelectedItem;
                    listE.Add(Empl);
                    itemsE.Add(Empl);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Заполните корректно все поля");
            }

        }
    }
}
