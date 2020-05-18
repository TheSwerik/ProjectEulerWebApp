using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using ProjectEulerWebApp.Models.Contexts;
using ProjectEulerWebApp.Models.Entities.EulerProblem;
using ProjectEulerWebApp.Util;

namespace ProjectEulerWebApp.Services
{
    public class EulerProblemService : ProjectEulerWebAppService
    {
        public EulerProblemService(ProjectEulerWebAppContext context) : base(context) { }

        public IActionResult GetList() { return new OkObjectResult(Context.EulerProblems.ToList()); }

        public IActionResult Get(int id)
        {
            var problem = Context.EulerProblems.Find(id);
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
            var foundProblem = Context.EulerProblems.Find(id);
            if (foundProblem == null) return new NotFoundObjectResult($"EulerProblem with number {id} not found.");
            Context.Remove(foundProblem);
            return TrySaveChanges(foundProblem);
        }

        public IActionResult Refresh(string idString)
        {
            if (string.IsNullOrWhiteSpace(idString)) return new BadRequestObjectResult("The ID is null or Empty.");
            var id = int.Parse(idString);
            var problem = Context.EulerProblems.Find(id);
            if (problem == null) return new BadRequestObjectResult("No Problem with ID {id} exists yet.");

            var data = ProjectEulerScraper.GetAll(id);
            problem.Title = data[EulerProblemPart.Title];
            problem.Description = data[EulerProblemPart.Description];
            problem.PublishDate = DateParser.ParseEulerDate(data[EulerProblemPart.PublishDate]);
            problem.Difficulty = int.Parse(Regex.Replace(data[EulerProblemPart.Difficulty], @"[^\d]", ""));
            return TrySaveChanges(problem);
        }

        public IActionResult RefreshAll(bool shouldOverride)
        {
            var ping = new Ping();
            if (!ProjectEulerScraper.IsAvailable("https://projecteuler.net/").Result)
                return new NotFoundObjectResult("projecteuler.net is not reachable.");
            Console.WriteLine("euler reached");
            for (var i = 1;; i++)
            {
                if (!ProjectEulerScraper.ProblemExists("https://projecteuler.net/problem=" + i)) break;

                var data = ProjectEulerScraper.GetAll(i);

                var problem = Context.EulerProblems.Find(i);
                if (problem == null) Context.Add(problem = new EulerProblem(i, null, null, null, null, null, null));

                problem.Title = data[EulerProblemPart.Title];
                problem.Description = data[EulerProblemPart.Description];
                problem.PublishDate = DateParser.ParseEulerDate(data[EulerProblemPart.PublishDate]);
                problem.Difficulty = data[EulerProblemPart.Difficulty] == null
                                         ? null
                                         : (int?) int.Parse(Regex.Replace(data[EulerProblemPart.Difficulty], @"[^\d]", ""));
                Console.WriteLine($"Problem {i} created.");
            }

            Console.WriteLine($"finishing...");

            return TrySaveChanges(Context.EulerProblems);
        }

        //TODO LOG STUFF
    }
}