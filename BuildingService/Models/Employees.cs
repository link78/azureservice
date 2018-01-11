using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingService.Models
{
    public class Employees
    {
        public int ID { get; set; }
        public string First_name { get; set; }
        public string Last_Name { get; set; }
        public string Position { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }


        //public int DepartId { get; set; }
        //public virtual Department Department { get; set; }
        //public Employees() { }

        //public Employees(int id)
        //{
        //    this.ID = id;
        //}

        //public Employees(string first, string last, string posi, string title, DateTime hire, int id): this(id)
        //{
        //    this.First_name = first;
        //    this.Last_Name = last;
        //    this.Position = posi;
        //    this.Title = title;
        //    this.HireDate = hire;
        //}

        //public override string ToString()
        //{
        //    return $"{this.First_name} - {this.Last_Name}";
        //}
    }
}
