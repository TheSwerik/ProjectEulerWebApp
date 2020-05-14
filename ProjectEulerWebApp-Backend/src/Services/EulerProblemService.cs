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
using ProjectEulerWebApp.Exceptions;

namespace ProjectEulerWebApp.Services
{
    public class EulerProblemService
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly ProjectEulerWebAppContext _context;
        public EulerProblemService(ProjectEulerWebAppContext context) => _context = context;

        public IActionResult GetDescription(string url)
        {
            var document = GetDocument(url).Result.Text;
            return new OkObjectResult(Regex.Replace(document, "font-size:.*;", "")
                                           .Replace("<br />", ""));
        }

        private static async Task<HtmlDocument> GetDocument(string url)
        {
            using var response = await Client.GetAsync(url);
            using var content = response.Content;
            var result = await content.ReadAsStringAsync();

            var document = new HtmlDocument();
            document.LoadHtml(result);
            return document;
        }
    }
}