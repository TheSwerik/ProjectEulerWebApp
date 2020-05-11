using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectEulerWebApp.Models.Contexts;
using ProjectEulerWebApp.Models.Entities.EulerProblem;

namespace ProjectEulerWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EulerProblemController : Controller
    {
        private readonly ProjectEulerWebAppContext _context;

        public EulerProblemController(ProjectEulerWebAppContext context) => _context = context;


        [HttpGet]
        [Route("get-list")]
        public IActionResult Get() => Ok(_context.EulerProblem.ToList());

        [HttpPost]
        [Route("change")]
        public IActionResult Post(EulerProblem problem)
        {
            _context.Add(problem);
            _context.SaveChanges();
            return Ok(problem);
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult Remove(EulerProblem problem)
        {
            _context.Remove(_context.EulerProblem.Find(problem.Id));
            _context.SaveChanges();
            return Ok(problem);
        }
    }
}