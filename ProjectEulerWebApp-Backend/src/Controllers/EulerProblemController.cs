using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectEulerWebApp.Models.Contexts;
using ProjectEulerWebApp.Models.Entities.EulerProblem;
using ProjectEulerWebApp.Services;

namespace ProjectEulerWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EulerProblemController : Controller
    {
        private readonly EulerProblemService _service;
        public EulerProblemController(EulerProblemService service) => _service = service;

        [HttpGet]
        [Route("get")]
        public IActionResult GetList() => _service.GetList();

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult Get(int id) => _service.Get(id);

        [HttpPost]
        [Route("create")]
        public IActionResult Post(EulerProblem problem) => _service.CreateProblem(problem);

        [HttpPost]
        [Route("remove")]
        public IActionResult Remove(EulerProblem problem) => _service.RemoveProblem(problem);

        [HttpPost]
        [Route("html")]
        public IActionResult Html(string url) => _service.GetDescription(url);
    }
}