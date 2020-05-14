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

namespace ProjectEulerWebApp.Services
{
    public class EulerProblemService
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly ProjectEulerWebAppContext _context;
        public EulerProblemService(ProjectEulerWebAppContext context) => _context = context;

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


        public IActionResult GetDescription(string url)
        {
            var document = GetDocument(url).Result.Text;
            document = Regex.Replace(document, "font-size:.*;", "")
                            .Replace("<br />", "")
                            .Trim();

            foreach (var match in Regex.Matches(document, "<p>.*</p>"))
            {
                var matchedString = match.ToString() ?? "";
                document = document.Replace(matchedString, matchedString.Substring(3, matchedString.Length - 7));
            }

            return new OkObjectResult(document.Trim().Insert(0, "<p>") + "</p>");
        }

        private static async Task<HtmlDocument> GetDocument(string url)
        {
            Console.WriteLine(url);
            url = "https://projecteuler.net/minimal=11";
            using var response = await Client.GetAsync(url);
            using var content = response.Content;
            var result = await content.ReadAsStringAsync();

            var document = new HtmlDocument();
            document.LoadHtml(result);
            return document;
        }

        private IActionResult TrySaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return new OkResult();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return new BadRequestResult();
            }
        }

        private IActionResult TrySaveChanges(object o)
        {
            try
            {
                _context.SaveChanges();
                return new OkObjectResult(o);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return new BadRequestObjectResult(o);
            }
        }
    }
}