using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingService.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BindNever]
        public int ID { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int EmployeeCount { get; set; }

        public virtual ICollection<Employees> Employees { get; set; } =
            new List<Employees>();


        
    }


    public class DepartmentViewModle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class DepartmentDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int EmployeeCount { get; set; }

        public ICollection<EmployeeDto> Employees { get; set; } =
            new List<EmployeeDto>();
    }
}
