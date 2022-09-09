using EMSProj.Models;

namespace EMSProj.ViewModel
{
    public class EmpDepartMap
    {
        public int MapId { get; set; }
        public int EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartName { get; set; }

        //public List<Department> departList { get; set; }

        //public List<Employee> empList { get; set; }
    }
}
