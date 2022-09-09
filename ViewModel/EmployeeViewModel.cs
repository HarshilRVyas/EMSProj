using System;
using EMSProj.Models;

namespace EMSProj.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }        
        public DateTime DateOfJoin { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartName { get; set; }
        public string DepartDescrName { get; set; }

        public List<Department> departmentList { get; set; } 
    }
}
