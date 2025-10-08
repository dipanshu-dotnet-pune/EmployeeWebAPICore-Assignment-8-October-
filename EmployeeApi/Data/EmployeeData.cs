using System.Collections.Generic;
using EmployeeApi.Models;

namespace EmployeeApi.Data
{
    public static class EmployeeData
    {
        public static List<Employee> Employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Dipanshu", Department = "Development", MobileNo = "9999999999", Email = "dipanshu@gmail.com" },
            new Employee { Id = 2, Name = "Mahesh", Department = "HR", MobileNo = "9998887776", Email = "mahesh@gmail.com" }
        };
    }
}
