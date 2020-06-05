using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectEulerWebApp.Models.Contexts;
using ProjectEulerWebApp.Models.Entities.EulerProblem;
using ProjectEulerWebApp.Util;

namespace ProjectEulerWebApp.Services
{
    public class EulerProblemService : ProjectEulerWebAppService
    {
        public EulerProblemService(ProjectEulerWebAppContext context, ILogger<ProjectEulerWebAppService> logger) :
            base(context, logger, 101)
        {
        }

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
            Info("Created Problem: " + problem);
            return TrySaveChanges(problem);
        }

        public IActionResult RemoveProblem(int id)
        {
            var foundProblem = Context.EulerProblems.Find(id);
            if (foundProblem == null) return new NotFoundObjectResult($"EulerProblem with number {id} not found.");
            Context.Remove(foundProblem);
            Warn("Removed Problem: " + foundProblem);
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
            Warn("Updated Problem: " + problem);
            return TrySaveChanges(problem);
        }

        public IActionResult RefreshAll(bool shouldOverride)
        {
            var ping = new Ping();
            if (!ProjectEulerScraper.IsAvailable("https://projecteuler.net/").Result)
                return new NotFoundObjectResult("projecteuler.net is not reachable.");
            Warn("Refreshing all problems...");
            for (var i = 1;; i++)
            {
                if (!ProjectEulerScraper.ProblemExists("https://projecteuler.net/problem=" + i)) break;

                var data = ProjectEulerScraper.GetAll(i);

                var problem = Context.EulerProblems.Find(i);
                if (problem == null) Context.Add(problem = new EulerProblem(i, null, null));

                problem.Title = data[EulerProblemPart.Title];
                problem.Description = data[EulerProblemPart.Description];
                problem.PublishDate = DateParser.ParseEulerDate(data[EulerProblemPart.PublishDate]);
                problem.Difficulty = data[EulerProblemPart.Difficulty] == null
                                         ? null
                                         : (int?) int.Parse(
                                             Regex.Replace(data[EulerProblemPart.Difficulty], @"[^\d]", ""));
                Info($"Problem {i} created: " + problem);
            }

            Info("Finished refreshing all problems.");
            return TrySaveChanges(Context.EulerProblems);
        }

        public IActionResult Solve(EulerProblem problem)
        {
            var times = ProjectEulerAnswerGetter.Solve(problem.Id);
            if (times.Count == 0) return new BadRequestObjectResult($"The problem {problem.Id} is not solved yet.");
            if (problem.IsSolved) return TrySaveChanges(times);

            problem.Times = new long[4];
            if (!times.TryGetValue("C#", out problem.Times[0])) problem.Times[0] = -1;
            if (!times.TryGetValue("Java", out problem.Times[1])) problem.Times[1] = -1;
            if (!times.TryGetValue("Cpp", out problem.Times[2])) problem.Times[2] = -1;
            if (!times.TryGetValue("Python", out problem.Times[3])) problem.Times[3] = -1;
            
            problem.IsSolved = true;
            problem.SolveDate = new DateTime();
            return TrySaveChanges(times);
        }
    }
}