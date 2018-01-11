using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildingService.Abstract;
using BuildingService.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildingService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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
            return this.Ok(repo.GetAll);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var m = repo.GetDepartment(id);

            if (m != null)
            {
                return this.Ok(m);
            }
            else
            {
                return this.NotFound();
            }
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

            if(repo.Update(d)== null)
            {
                return this.NotFound();
            } else
            {
                return this.Ok(d.ID);
            }
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