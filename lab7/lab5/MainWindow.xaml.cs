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
using System.Data;
using System.Data.SqlClient;
using lab6;

namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        SqlDataAdapter adapterDep;
        DataTable dt;
        DataTable dtDep;

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

            string connectionString = @"Data Source=NEMAN\SQLEXPRESS;Initial Catalog=lesson7;Integrated Security=True;Pooling=False";
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();
            adapterDep = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT Id, Name, Age, Salary, Department FROM Employee", connection);
            SqlCommand commandDep = new SqlCommand("SELECT Id, Name FROM Departments", connection);
            adapter.SelectCommand = command;
            adapterDep.SelectCommand = commandDep;

            //insert
            command = new SqlCommand(@"INSERT INTO Employee (Name, Age, Salary, Department) VALUES (@Name, @Age, @Salary, @Department); SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, 10, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;

            commandDep = new SqlCommand(@"INSERT INTO Departments (Name) VALUES (@Name); SET @ID = @@IDENTITY;", connection);
            commandDep.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            SqlParameter paramDep = commandDep.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            paramDep.Direction = ParameterDirection.Output;
            adapterDep.InsertCommand = commandDep;

            // update
            command = new SqlCommand(@"UPDATE People SET Name = @Name, Age = @Age, Salary = @Salary, Department= @Department WHERE Id = @Id", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, 100, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;

            commandDep = new SqlCommand(@"UPDATE Departments SET Name = @Name WHERE Id = @ID ", connection);
            commandDep.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            paramDep = commandDep.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            paramDep.SourceVersion = DataRowVersion.Original;
            adapterDep.UpdateCommand = commandDep;

            //delete
            command = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = command;
            dt = new DataTable();
            adapter.Fill(dt);
            EmployeeDataGrid.DataContext = dt.DefaultView;

            commandDep = new SqlCommand("DELETE FROM Departments WHERE Id = @IDd ", connection);
            paramDep = commandDep.Parameters.Add("@IDd", SqlDbType.Int, 0, "ID");
            paramDep.SourceVersion = DataRowVersion.Original;
            adapterDep.DeleteCommand = commandDep;
            dtDep = new DataTable();
            adapterDep.Fill(dtDep);
            DepartmentDataGrid.DataContext = dtDep.DefaultView;
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
                editWindow.StartDep = (Department)Departments.SelectedItem;
                editWindow.listE = listEmpl;
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
            Employee.SaveXML(listEmpl, "data.xml");
            EditWindow editWindow = new EditWindow();
            editWindow.Owner = this;
            editWindow.listE = listEmpl;
            editWindow.itemsE = itemsE;
            editWindow.Title = "Добавление работника";
            editWindow.btnAddSave.Content = "Добавить";
            editWindow.cbDepartment.ItemsSource = itemsD;
            editWindow.tbID.Text = (listEmpl.Count() + 1).ToString();
            editWindow.StartDep = (Department)Departments.SelectedItem;
            editWindow.ShowDialog();           
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Добавим новую строку
            DataRow newRow = dt.NewRow();
            EditWindow1 editWindow = new EditWindow1(newRow);            
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                dt.Rows.Add(editWindow.resultRow);
                adapter.Update(dt);
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;
            newRow.BeginEdit();
            EditWindow1 editWindow = new EditWindow1(newRow.Row);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                adapter.Update(dt);
            }
            else
            {
                newRow.CancelEdit();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;

            newRow.Row.Delete();
            adapter.Update(dt);
        }

        private void addButtonDep_Click(object sender, RoutedEventArgs e)
        {
            // Добавим новую строку
            DataRow newRow = dtDep.NewRow();
            EditWindow2 editWindow = new EditWindow2(newRow);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                dtDep.Rows.Add(editWindow.resultRow);
                adapterDep.Update(dtDep);
            }
        }

        private void updateButtonDep_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)DepartmentDataGrid.SelectedItem;
            newRow.BeginEdit();
            EditWindow2 editWindow = new EditWindow2(newRow.Row);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                adapterDep.Update(dtDep);
            }
            else
            {
                newRow.CancelEdit();
            }
        }

        private void deleteButtonDep_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;

            newRow.Row.Delete();
            adapterDep.Update(dtDep);
        }
    } 
}
