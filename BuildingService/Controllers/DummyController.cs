using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingService.Abstract;

namespace BuildingService.Controllers
{
    [Produces("application/json")]
    [Route("api/Dummy")]
    public class DummyController : Controller
    {
        private IRepoDepartment repo;

        public DummyController(IRepoDepartment _repo)
        {
            repo = _repo;
        }

        [HttpGet]
        public IActionResult TestDb()
        {
            return Ok();
        }
    }
}