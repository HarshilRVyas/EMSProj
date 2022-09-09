using System;
using EMSProj.Models;
using Microsoft.EntityFrameworkCore;

namespace EMSProj.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Employee> EmpDetails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmpDepartmentMap> EmpDepMapping { get; set; }
    }
}
