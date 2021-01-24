using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.APIClient.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Column("EmployeeID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage ="Employee ID is required")]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Employee First Name is required")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(20,ErrorMessage ="First Name must have less than 20 characters")]
        public string FirstName { get; set; }
        [Column("LastName")]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "Last Name must have less than 30 characters")]
        [Required(ErrorMessage = "Employee Last Name is required")]
        public string LastName { get; set; }
        [Column("Title")]
        [Display(Name = "Title")]
        [StringLength(30, ErrorMessage = "Title must have less than 30 characters")]
        [Required(ErrorMessage = "Employee Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Employee Birth Date is required")]
        [Column("BirthDate")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Employee Hire Date is required")]
        [Column("HireDate")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Employee Country is required")]
        [Column("Country")]
        [Display(Name = "Country")]
        [StringLength(50, ErrorMessage = "Country must have less than 49 characters")]
        public string Country { get; set; }
        [Column("Notes")]
        [Display(Name = "Notes")]
        [StringLength(1000, ErrorMessage = "Notes must have less than 1000 characters")]
        public string Notes { get; set; }

    }
}
