using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildingService.Abstract;
using BuildingService.Models;
using AutoMapper;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildingService.Controllers
{
   
    [Produces("application/json")]
    [Route("api/departments")]
    public class DepartmentsController : Controller
    {
        // GET: /<controller>/
        private IRepoDepartment repo;
        

        public DepartmentsController (IRepoDepartment _repo)
        {
            repo = _repo;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var results = repo.GetAll;
           
            // using auto mapper
            var model = Mapper.Map<IEnumerable<DepartmentViewModle>>(results);
            
            return this.Ok(model);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, bool includeEmployee = false)
        {

            var model = repo.GetDepartment(id, includeEmployee);
            if(model == null)
            {
                NotFound();
            }

            if (includeEmployee)
            {
           

                var resultFromdepartment = Mapper.Map<DepartmentDto>(model);
                return Ok(resultFromdepartment);
               
            }

            // whithout employees
            var departmentOnly = Mapper.Map<DepartmentViewModle>(model);
            

            return Ok(departmentOnly);
                           
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Department d)
        {
           
            repo.CreateNew(d);

            return this.Ok(d);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Department d)
        {
            d.ID = id;
            
                repo.Update(d);
                return NoContent();
       
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var q = repo.Delete(id);

            if (q != null)
            {
                return this.NoContent();
            }
            else
            {
                return this.Ok(q.ID);
            }
        }

        
     
    }
}