using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIService.Models;
using System.Data;
using System.Data.SqlClient;


namespace WebAPIService.Controllers
{
    public class DepartmentsController : ApiController
    {
        string connectionString = @"Data Source=NEMAN\SQLEXPRESS;Initial Catalog=lesson7;Integrated Security=True;Pooling=False";
        List<Department> departments = new List<Department>();

        public IEnumerable<Department> GetAllDepartments()
        {
            string sqlExpression = "SELECT Id, Name FROM Departments";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)        // Если есть данные
                {
                    while (reader.Read())  // Построчно считываем данные
                    {
                        departments.Add(new Department
                        {
                            Id = Convert.ToInt32(reader.GetValue(0)),
                            Name = reader.GetString(1)
                        });
                    }
                }
                reader.Close();

                return departments;
            }
        }
            public IHttpActionResult GetDepartments(int id)
            {
                var x = GetAllDepartments();
                var department = x.FirstOrDefault((p) => p.Id == id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);
            }
        
    }
}
