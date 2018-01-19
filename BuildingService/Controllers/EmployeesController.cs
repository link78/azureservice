using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingService.Abstract;
using BuildingService.Models;
using Microsoft.Extensions.Logging;
using BuildingService.Services;

namespace BuildingService.Controllers
{
    [Produces("application/json")]
    [Route("api/departments")]
    public class EmployeesController : Controller
    {

        private ILogger<EmployeesController> logger;
        private IRepoDepartment repo;
        private IMailService mailService;

        public EmployeesController (IRepoDepartment _repo, 
            ILogger<EmployeesController> _logger, IMailService _mailService)
        {
            repo = _repo;
            logger = _logger;
            mailService = _mailService;
        }




        [HttpGet("{departmentId}/employees")]
        public IActionResult GetEmployees(int departmentId)
        {
            
                if (!repo.DepartmentExist(departmentId))
                {
                    logger.LogInformation($"Department with id {departmentId} wasn't found when accessing employee ");
                    return NotFound();
                }

            var foundEmployee = repo.GetEmployeesFromDepartment(departmentId);

            var result = AutoMapper.Mapper.Map<IEnumerable<EmployeeDto>>(foundEmployee);
               
            
            return Ok(result);
                  
        }


        [HttpGet("{departmentId}/employees/{id}", Name ="GetOneEmployee")]
        public IActionResult GetEmployee(int departmentId, int id)
        {
            // check to see if departement exist?

            if (!repo.DepartmentExist(departmentId))
            {
                
                return NotFound();
            }

            var empFound = repo.GetEmployeesFromDepartment(departmentId, id);

            if(empFound == null)
            {
                return NotFound();
            }

            var emp = AutoMapper.Mapper.Map<EmployeeDto>(empFound);
            return Ok(emp);
           

            
        }

        [HttpPost("{departmentId}/employees")]
        public IActionResult CreateEmployee(int departmentId, [FromBody] EmployeeDtoCreation employee)
        {
            if(employee == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // check to see if departement exist?

            if (!repo.DepartmentExist(departmentId))
            {

                return NotFound();
            }

            // mapping
            var finalEmployee = AutoMapper.Mapper.Map<Employees>(employee);
            repo.addEmployeeForDepartment(departmentId, finalEmployee);
            if (!repo.Save())
            {
                return StatusCode(500, "An error happened while handling yor request");
            }

            var createdEmployee = AutoMapper.Mapper.Map<EmployeeDto>(finalEmployee);

            return CreatedAtAction("GetEmployee",
                new { departmentId = departmentId, id = createdEmployee.ID }, createdEmployee);
        }

        [HttpPut("{departmentId}/employees/{id}")]
        public IActionResult UpdateEmployee(int departmentId, int id, [FromBody] EmployeeDtoCreation employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // check to see if departement exist?

            if (!repo.DepartmentExist(departmentId))
            {

                return NotFound();
            }
            // check to find the employee to update
            var findEmp = repo.GetEmployeesFromDepartment(departmentId, id);


            if (findEmp == null)
            {
                return NotFound(); // return null if it doesn't exist
            }

            AutoMapper.Mapper.Map(employee, findEmp);
            if (!repo.Save())
            {
                return StatusCode(500, "An error happened while handling yor request");
            }

            return NoContent();

        }

        [HttpDelete("{departmentId}/employees/{id}")]
        public IActionResult DeleteEmployee(int departmentId, int id)
        {
            // check to see if departement exist?

            if (!repo.DepartmentExist(departmentId))
            {

                return NotFound();
            }

            var model = repo.GetEmployeesFromDepartment(departmentId, id);

            if (model == null)
            {
                return NotFound();
            }

            repo.RemoveEmployee(model);
            if (!repo.Save())
            {
                return StatusCode(500, "An error happened while handling yor request");
            }
            mailService.Send("Employee delete.", $"Employee {model.Last_Name}-{model.First_name} with id {model.ID} was deleted");

            return NoContent();
        }

    }
}