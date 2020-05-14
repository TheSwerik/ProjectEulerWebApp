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

        public IActionResult GetList() => new OkObjectResult(_context.EulerProblem.ToList());

        public IActionResult Get(int id)
        {
            var problem = _context.EulerProblem.Find(id);
            if (problem == null) return new NotFoundObjectResult($"EulerProblem with number {id} not found.");
            return new OkObjectResult(problem);
        }

        public IActionResult CreateProblem(EulerProblem problem)
        {
            _context.Add(problem);
            return TrySaveChanges(problem);
        }

        public IActionResult RemoveProblem(int id)
        {
            var foundProblem = _context.EulerProblem.Find(id);
            if (foundProblem == null) return new NotFoundObjectResult($"EulerProblem with number {id} not found.");
            _context.Remove(foundProblem);
            return TrySaveChanges(foundProblem);
        }

        //TODO remove this method
        public IActionResult GetDescription(int id) => new OkObjectResult(ProjectEulerScraper.GetDescription(id));
    }
}