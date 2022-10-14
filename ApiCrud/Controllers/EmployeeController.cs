using CRUD_Operations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SampleContext _context;

        public EmployeeController(SampleContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var result =  _context.Employees.ToList();
            if (result == null)
                return BadRequest();
            return Ok(result);  
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult Get(int id)
        {
            var result = _context.Employees.FirstOrDefault(m => m.Id == id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Post(Employee employee)
        {
            _context.Add(employee);
            var res = _context.SaveChanges();
            if(res==1)
                return Ok();
            return BadRequest();
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Put(Employee employee)
        {
            if (employee == null || employee.Id == 0)
                return BadRequest();

            var emp = _context.Employees.Find(employee.Id);
            if (emp == null)
                return NotFound();
            emp.Name = employee.Name;
            emp.Role = employee.Role;
            emp.Salary = employee.Salary;
            var res= _context.SaveChanges();
            if (res == 1)
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null) return NotFound();
            _context.Employees.Remove(emp);
            var res=_context.SaveChanges();
            if (res == 1)
                return Ok();
            return BadRequest();
        }
    }
}
