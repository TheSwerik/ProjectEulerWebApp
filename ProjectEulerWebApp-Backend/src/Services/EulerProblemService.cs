using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ProjectEulerWebApp.Models.Contexts;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using ProjectEulerWebApp.Exceptions;
using ProjectEulerWebApp.Models.Entities.EulerProblem;
using ProjectEulerWebApp.Util;

namespace ProjectEulerWebApp.Services
{
    public class EulerProblemService : ProjectEulerWebAppService
    {
        public EulerProblemService(ProjectEulerWebAppContext context) : base(context) { }

        public IActionResult GetList() => new OkObjectResult(Context.EulerProblem.ToList());

        public IActionResult Get(int id)
        {
            var problem = Context.EulerProblem.Find(id);
            if (problem == null) return new NotFoundObjectResult($"EulerProblem with number {id} not found.");
            return new OkObjectResult(problem);
        }

        public IActionResult CreateProblem(EulerProblem problem)
        {
            Context.Add(problem);
            return TrySaveChanges(problem);
        }

        public IActionResult RemoveProblem(int id)
        {
            var foundProblem = Context.EulerProblem.Find(id);
            if (foundProblem == null) return new NotFoundObjectResult($"EulerProblem with number {id} not found.");
            Context.Remove(foundProblem);
            return TrySaveChanges(foundProblem);
        }

        public IActionResult Refresh(string idString)
        {
            if (string.IsNullOrWhiteSpace(idString)) return new BadRequestObjectResult("The ID is null or Empty.");
            var id = int.Parse(idString);
            var problem = Context.EulerProblem.Find(id);
            if (problem == null) return new BadRequestObjectResult("No Problem with ID {id} exists yet.");
            
            problem.Title = ProjectEulerScraper.GetTitle(id);
            problem.Description = ProjectEulerScraper.GetDescription(id);
            // problem.PublishDate = ProjectEulerScraper.GetDescription(id);
            // problem.Difficulty = ProjectEulerScraper.GetDescription(id);
            return TrySaveChanges(problem);
        }
    }
}