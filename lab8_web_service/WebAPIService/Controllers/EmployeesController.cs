using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIService.Models;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System;

namespace WebAPIService.Controllers
{
    public class EmployeesController : ApiController
    {        
        string connectionString = @"Data Source=NEMAN\SQLEXPRESS;Initial Catalog=lesson7;Integrated Security=True;Pooling=False";
                
        List<Employee> employees = new List<Employee>();
        
        public IEnumerable<Employee> GetAllEmployees()
        {
            string sqlExpression = "SELECT E.Id, E.Name, Age, Salary, Department, D.Name FROM Employee E LEFT JOIN Departments D ON E.Department = D.Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)        // Если есть данные
                {                       
                    while (reader.Read())  // Построчно считываем данные
                    {
                        employees.Add(new Employee
                        {
                            Id = Convert.ToInt32(reader.GetValue(0)),
                            Name = reader.GetString(1),
                            Age = Convert.ToInt32(reader["Age"]),
                            Salary = Convert.ToInt32(reader["Salary"]),
                            department = new Department { Id = Convert.ToInt32(reader["Department"]), Name = reader.GetString(5) }
                        });                       
                    }
                }
                reader.Close();

                return employees;
            }

        }

        public IHttpActionResult GetEmployees(int id)
        {
            var x = GetAllEmployees();
            var employee = x.FirstOrDefault((p) => p.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

    }
}


