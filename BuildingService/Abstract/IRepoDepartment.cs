using BuildingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingService.Abstract
{
   public interface IRepoDepartment
    {
        IEnumerable<Department> GetAll { get; }
        Department CreateNew(Department d);
        Department Update(Department d);
        Department GetDepartment(int id);
        Department Delete(int id);

    }

    public class RepoDepartment:IRepoDepartment
    {
       protected readonly AppDBContext context;

        public RepoDepartment(AppDBContext _context)
        {
            context = _context;
        }

        public IEnumerable<Department> GetAll => context.Departments;


        public Department CreateNew(Department d)
        {
            context.Departments.Add(d);
            return d;
        }

        public Department Delete(int id)
        {
            var q = context.Departments.SingleOrDefault(d => d.ID == id);
            if( q != null)
            {
                context.Departments.Remove(q);
            }

            return q;
        }

        public Department GetDepartment(int id)
        {
            // var model = context.Departments.Find(id);

            return context.Departments.SingleOrDefault(d => d.ID == id);
        }

        public Department Update(Department d)
        {
            var up = this.GetDepartment(d.ID);

            if(up != null)
            {
                this.CreateNew(up);
            }

            return up;
        }
    }
}
