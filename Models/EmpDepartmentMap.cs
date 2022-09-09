using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSProj.Models
{
    public class EmpDepartmentMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MapId { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
    }
}
