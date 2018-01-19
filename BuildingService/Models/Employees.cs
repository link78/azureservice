using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingService.Models
{
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string First_name { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string Last_Name { get; set; }
        [Required, MaxLength(50), MinLength(3)]
        public string Position { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }

        public Department Department { get; set; }
        
        public int DepartmentId { get; set; }

    }
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string First_name { get; set; }
        public string Last_Name { get; set; }
        public string Position { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
    }

    public class EmployeeDtoCreation
    {
       
        public string First_name { get; set; }
        public string Last_Name { get; set; }
        public string Position { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
    }
}
