using EMSProj.Models;
using EMSProj.ViewModel;

namespace EMSProj.Util
{
    public enum SortOrder
    {
        Ascending = 0,
        Descending = 1
    }

    public class DataSort
    {
        public DataSort()
        {
        }

        public List<Department> GetSortedDepart(IEnumerable<Department> objDepart, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                    objDepart = objDepart.OrderBy(x => x.Name).ToList();
                else
                    objDepart = objDepart.OrderByDescending(x => x.Name).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    objDepart = objDepart.OrderBy(x => x.Description).ToList();
                else
                    objDepart = objDepart.OrderByDescending(x => x.Description).ToList();
            }
            return objDepart.ToList();
        }

        public List<EmployeeViewModel> GetSortedEmp(IEnumerable<EmployeeViewModel> objEmp, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "firstname")
            {
                if (sortOrder == SortOrder.Ascending)
                    objEmp = objEmp.OrderBy(x => x.FirstName).ToList();
                else
                    objEmp = objEmp.OrderByDescending(x => x.FirstName).ToList();
            }
            else if (SortProperty.ToLower() == "lastname")
            {
                if (sortOrder == SortOrder.Ascending)
                    objEmp = objEmp.OrderBy(x => x.LastName).ToList();
                else
                    objEmp = objEmp.OrderByDescending(x => x.LastName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    objEmp = objEmp.OrderBy(x => x.Phone).ToList();
                else
                    objEmp = objEmp.OrderByDescending(x => x.Phone).ToList();
            }
            return objEmp.ToList();
        }

        public List<Department> GetDepartmentSearch(IEnumerable<Department> objDepart, string searchText="")
        {
            if(searchText != "" && searchText != null)
                objDepart = objDepart.Where(x => x.Name.ToLower().Contains(searchText) 
                    || x.Description.ToLower().Contains(searchText)).ToList();
             else
                objDepart = objDepart.ToList();

            return objDepart.ToList();
        }
    }
}
