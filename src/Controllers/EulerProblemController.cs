using System;
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

        public EulerProblemController(ProjectEulerWebAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.EulerProblems.ToList());
        }

        [HttpPost]
        public IActionResult Post(EulerProblem problem)
        {
            // var newProblem = new EulerProblem(
                // problem.Id,
                // problem.Title,
                // problem.Description,
                // problem.SolveDate,
                // problem.Solution
            // );
            _context.Add(problem);
            _context.SaveChanges();
            return Ok(problem);
        }
    }
}