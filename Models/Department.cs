using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSProj.Models
{    
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        
        [Required(ErrorMessage="Department Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage="Department Description is required")]
        public string Description { get; set; }

        //public ICollection<Employee> Employees { get; set; }

    }
}
