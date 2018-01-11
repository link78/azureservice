using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingService.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int EmployeeCount { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }


        //public Department()
        //{
        //    this.Employees = new List<Employees>();
        //}
        //public Department(int id)
        //{
        //    this.ID = id;
        //}
        //public Department(int id, string name): this(id)
        //{
        //    this.Name = name;
        //}

        //public override string ToString()
        //{
        //    return this.Name;
        //}
    }
}
