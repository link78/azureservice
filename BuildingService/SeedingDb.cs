using BuildingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingService
{
    public static class SeedingDb
    {
        public static void EnsuredSeedData(this AppDBContext context)
        {



            if (context.Departments.Any())
            {
                return;
            }

            var data = new List<Department>()
            {
                new Department{ Name="IT", EmployeeCount= 2,
                    Employees = new List<Employees>()
                {
                        new Employees
                        {
                             First_name="Aline", Last_Name="Wallaris",
                            Position ="Database Admin", HireDate= DateTime.Parse("12/10/2015"), Title="Ms"
                        },
                        new Employees
                        {
                            First_name="Adamien", Last_Name="Toris", Position="Software Developer",
                            HireDate =DateTime.Parse("5/15/2010"), Title="Mr"
                        }

                }
                },

                new Department { Name="Finance", EmployeeCount=10,
                    Employees = new List<Employees>()
                    {
                        new Employees{  First_name="Tarte", Last_Name="Fry", Position="CFO",
                            HireDate =DateTime.Parse("5/25/2015"), Title="Ms."},

                         new Employees
                        {
                            First_name="Zackaria", Last_Name="Hamada", Position="Financier advisor",
                            HireDate =DateTime.Parse("5/15/2010"), Title="Mr"
                        }
                    }

                },

                new Department
                {
                     Name="Accountants", EmployeeCount= 4,
                     Employees = new List<Employees>()
                    {
                        new Employees{  First_name="Diamond", Last_Name="Red", Position="Accountant Manager",
                            HireDate =DateTime.Parse("5/25/2015"), Title="Ms."}
                }

        }

            };

            context.AddRange(data);
            context.SaveChanges();

        }
    }
}
