using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class EmployeeService
    {

        private readonly List<Employee> employees = new List<Employee>()
        {
            new Employee(1,"Vasu"),
            new Employee(2,"Dipak"),
            new Employee(3,"Mistry")
        };

        public EmployeeService()
        {
        }

        public List<Employee> GetEmployees()
        {
            return this.employees;
        }
    }
}

