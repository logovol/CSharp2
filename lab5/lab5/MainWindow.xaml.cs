using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Department> itemsD = new ObservableCollection<Department>();
        ObservableCollection<Employee> itemsE = new ObservableCollection<Employee>();
        List<Employee> listEmpl;
        List<Department> listDep;

        public MainWindow()
        {
            InitializeComponent();
            Fill();
        }

        void Fill()
        {
            listEmpl = new List<Employee>();
            listDep = new List<Department>();
            listDep.Add(new Department() { Id = 3, Name = "it" });
            listDep.Add(new Department() { Id = 2, Name = "financial" });
            listDep.Add(new Department() { Id = 1, Name = "administration" });            

            listEmpl.Add(new Employee() { Id = 1, Name = "Mikhail", Age = 29, Salary = 1000, department = listDep[0] });
            listEmpl.Add(new Employee() { Id = 2, Name = "Semion", Age = 25, Salary = 1000, department = listDep[0] });
            listEmpl.Add(new Employee() { Id = 3, Name = "Leonid", Age = 22, Salary = 980, department = listDep[1] });
            listEmpl.Add(new Employee() { Id = 4, Name = "Sara", Age = 27, Salary = 950, department = listDep[1] });
            listEmpl.Add(new Employee() { Id = 5, Name = "Barbara", Age = 21, Salary = 800, department = listDep[1] });
            listEmpl.Add(new Employee() { Id = 6, Name = "Mark", Age = 29, Salary = 2000, department = listDep[2] });
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Departments.SelectedItem = listDep[0];
            foreach (var dep in listDep)
            {
                itemsD.Add(dep);
            }

            Departments.ItemsSource = itemsD;
        }
                
        private void Window_Activated(object sender, EventArgs e)
        {
            itemsE.Clear();
            Departments.ItemsSource = itemsD;

            foreach (var item in listEmpl)
            {
                if (item.department == Departments.SelectedItem)
                {
                    itemsE.Add(item);
                }
            }
            lbEmployee.ItemsSource = itemsE;
        }

        public void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Department selectedItem = (Department)comboBox.SelectedItem;

            itemsE.Clear();
            foreach (var item in listEmpl)
            {                
                if (item.department == selectedItem)
                {
                    itemsE.Add(item);                    
                }
            }
            lbEmployee.ItemsSource = itemsE;
        }

        private void lbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(e.AddedItems[0].ToString());
        }

        private void Button_ClickEditRow(object sender, RoutedEventArgs e)
        {
            if (lbEmployee.SelectedItem != null)
            {
                EditWindow editWindow = new EditWindow();
                editWindow.Owner = this;                
                object t = lbEmployee.SelectedItem;
                Employee empl = t as Employee;
                editWindow.Title = "Редактирование";
                editWindow.btnAddSave.Content = "Сохранить";
                editWindow.Empl = empl;
                editWindow.tbID.Text = empl.Id.ToString();
                editWindow.tbName.Text = empl.Name;
                editWindow.tbAge.Text = empl.Age.ToString();
                editWindow.tbSalary.Text = empl.Salary.ToString();
                editWindow.cbDepartment.SelectedItem = empl.department;
                editWindow.cbDepartment.Text = empl.department.ToString();
                editWindow.cbDepartment.ItemsSource = itemsD;
                editWindow.itemsE = itemsE;
                editWindow.Show();
            }           
        }

        private void Button_ClickAddDepartment(object sender, RoutedEventArgs e)
        {
            AddDepWindow addDepWindow = new AddDepWindow();
            addDepWindow.Owner = this;
            addDepWindow.listD = listDep;
            addDepWindow.itemsD = itemsD;
            addDepWindow.Title = "Добавление департамента";
            addDepWindow.tbID.Text = (listDep.Count() + 1).ToString();
            addDepWindow.ShowDialog();
        }

        private void Button_ClickAddEmployee(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Owner = this;
            editWindow.listE = listEmpl;
            editWindow.itemsE = itemsE;
            editWindow.Title = "Добавление работника";
            editWindow.btnAddSave.Content = "Добавить";
            editWindow.cbDepartment.ItemsSource = itemsD;
            editWindow.tbID.Text = (listEmpl.Count() + 1).ToString();
            editWindow.ShowDialog();           
        }
    } 
}
