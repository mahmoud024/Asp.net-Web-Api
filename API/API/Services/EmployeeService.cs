using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using API.Model;


namespace API.Services
{


    public class EmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT EMPID, EMPNAME, EMPGMAIL, JOB_TITLE, DEPARTMENT, HIRE_DATE, SALARY, STATUS FROM EMPLOYEE";

                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                EmpId = reader.GetInt32(0),
                                EmpName = reader.GetString(1),
                                EmpGmail = reader.GetString(2),
                                JobTitle = reader.GetString(3),
                                Department = reader.GetString(4),
                                HireDate = reader.GetDateTime(5),
                                Salary = reader.GetDecimal(6),
                                Status = reader.GetString(7)
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }
    }

}
