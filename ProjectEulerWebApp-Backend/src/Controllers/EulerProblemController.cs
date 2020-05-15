using Microsoft.AspNetCore.Mvc;
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
        [HttpGet] [Route("get")] public IActionResult GetList() => _service.GetList();
        [HttpGet] [Route("get/{id}")] public IActionResult Get(int id) => _service.Get(id);
        [HttpPost] [Route("create")] public IActionResult Post(EulerProblem problem) => _service.CreateProblem(problem);
        [HttpDelete] [Route("remove/{id}")] public IActionResult Delete(int id) => _service.RemoveProblem(id);
        [HttpPut] [Route("refresh")] public IActionResult Refresh(object id) => _service.Refresh(id.ToString());
        [HttpPut] [Route("refresh-all")] public IActionResult RefreshAll(object body) => _service.RefreshAll(bool.Parse(body.ToString()!));
    }
}