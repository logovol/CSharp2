using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using lab5;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:44342/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private async void allEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Employee> employees = await GetEmployeesAsync(client.BaseAddress + "api/Employees");
            EmployeeDataGrid.ItemsSource = employees;
        }

        private async void allDepartmentsButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Department> departments = await GetDepartmentsAsync(client.BaseAddress + "api/Departments");
            DepartmentDataGrid.ItemsSource = departments;
        }

        private async void idEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = new List<Employee>();
            if (idemployeeTextBox.Text != String.Empty)
            {
                Employee employee = await GetEmployeeAsync(client.BaseAddress + "api/Employees/" + idemployeeTextBox.Text);
                if (employee != null)
                    employees.Add(employee);
            }
            else
            {
                employees = (List<Employee>)await GetEmployeesAsync(client.BaseAddress + "api/Employees");
            }
            EmployeeDataGrid.ItemsSource = employees;
        }

        private async void idDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            List<Department> departments = new List<Department>();
            if (iddepartmentTextBox.Text != String.Empty)
            {
                Department department = await GetDepartmentAsync(client.BaseAddress + "api/Departments/" + iddepartmentTextBox.Text);
                if (department != null)
                    departments.Add(department);
            }
            else
            {
                departments = (List<Department>)await GetDepartmentsAsync(client.BaseAddress + "api/Departments");
            }
            DepartmentDataGrid.ItemsSource = departments;
        }
        static async Task<IEnumerable<Employee>> GetEmployeesAsync(string path)
        {
            IEnumerable<Employee> employees = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadAsAsync<IEnumerable<Employee>>();
                }
            }
            catch (Exception)
            {
            }
            return employees;
        }

        static async Task<IEnumerable<Department>> GetDepartmentsAsync(string path)
        {
            IEnumerable<Department> departments = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    departments = await response.Content.ReadAsAsync<IEnumerable<Department>>();
                }
            }
            catch (Exception)
            {
            }
            return departments;
        }

        static async Task<Department> GetDepartmentAsync(string path)
        {
            Department department = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    department = await response.Content.ReadAsAsync<Department>();
                }
            }
            catch (Exception)
            {
            }
            return department;
        }

        static async Task<Employee> GetEmployeeAsync(string path)
        {
            Employee employee = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    employee = await response.Content.ReadAsAsync<Employee>();
                }
            }
            catch (Exception)
            {
            }
            return employee;
        }
    }
}
