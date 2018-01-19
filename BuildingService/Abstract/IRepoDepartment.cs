using BuildingService.Models;
using Microsoft.EntityFrameworkCore;
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
        Department GetDepartment(int id, bool includeEmployee);
        Department Delete(int id);
        IEnumerable<Employees> GetEmployeesFromDepartment(int departmentId);
        Employees GetEmployeesFromDepartment(int departmentId, int employeeId);

        bool DepartmentExist(int departmentId);
        void RemoveEmployee(Employees emp);
        void addEmployeeForDepartment(int departmentId, Employees employee);
        bool Save();

    }

    public class RepoDepartment:IRepoDepartment
    {
         protected readonly AppDBContext context;
        

        public RepoDepartment(AppDBContext _context)
        {
            context = _context;
        }

        public IEnumerable<Department> GetAll => context.Departments.OrderBy(d => d.Name).ToList();


        public bool DepartmentExist(int departmentId)
        {
            return context.Departments.Any(d => d.ID == departmentId);
        }


        public Department CreateNew(Department d)
        {
            context.Add(d);
            context.SaveChanges();
            return d;
        }

        public Department Delete(int id)
        {
            var q = context.Departments.SingleOrDefault(d => d.ID == id);
            if (q != null)
            {
                context.Remove(q);
                context.SaveChanges();
            }

            return q;
        }

        public Department GetDepartment(int id, bool includeEmployee)
        {
            // var model = context.Departments.Find(id);
            if (includeEmployee)
            {
                return context.Departments.Include(e => e.Employees).Where(d => d.ID == id).FirstOrDefault();
            }
            return context.Departments.SingleOrDefault(d => d.ID == id);

        }



        public IEnumerable<Employees> GetEmployeesFromDepartment(int departmentId)
        {
            return context.Employees.Where(e => e.DepartmentId == departmentId).ToList();
        }



        public Employees GetEmployeesFromDepartment(int departmentId, int employeeId)
        {
            var model = context.Employees.Where(d => d.DepartmentId == departmentId && d.ID == employeeId).FirstOrDefault();
            return model;
        }




        public Department Update(Department d)
        {
           
            context.Entry(d).State = EntityState.Modified;
            context.SaveChanges();
            

            return d;
        }

        public void addEmployeeForDepartment(int departmentId, Employees employee)
        {
            var department = GetDepartment(departmentId, false);
            department.Employees.Add(employee);
           
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public void RemoveEmployee(Employees emp)
        {
            context.Employees.Remove(emp);
        }
    }
}
