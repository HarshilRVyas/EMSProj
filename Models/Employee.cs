using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSProj.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage="First Name is required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage="Last Name is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage="Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage="Email ID is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +  
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +  
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",  
                            ErrorMessage = "Email is not valid")]
        public string EmailAddress { get; set; }
        
        [Required(ErrorMessage="Phone No is required")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage="Date of Birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateOfBirth { get; set; }
        
        [Required(ErrorMessage="Date of Joining is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateOfJoin { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
    }
}
