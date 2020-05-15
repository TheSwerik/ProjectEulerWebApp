using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

// TODO Find Publish date
// TODO Find Difficulty
// TODO Async Load all existing Problems

// ReSharper disable InconsistentNaming
namespace ProjectEulerWebApp.Util
{
    public static class ProjectEulerScraper
    {
        private static readonly HttpClient Client = new HttpClient();
        private const string EulerProblemURL = "https://projecteuler.net/problem=";
        private const string EulerDescriptionURL = "https://projecteuler.net/minimal=";

        private static async Task<HtmlDocument> GetDocument(string url)
        {
            using var response = await Client.GetAsync(url);
            using var content = response.Content;
            var result = await content.ReadAsStringAsync();

            var document = new HtmlDocument();
            document.LoadHtml(result);
            return document;
        }

        public static string GetTitle(int id)
        {
            var document = GetDocument(EulerProblemURL + id).Result;
            return document.DocumentNode.ChildNodes.FindFirst("h2").InnerHtml;
        }

        public static string GetDescription(int id)
        {
            var document = GetDocument(EulerDescriptionURL + id).Result.Text;
            document = Regex.Replace(document, "font-size:.*;", "").Trim();

            foreach (var match in Regex.Matches(document, "<p>.*</p>"))
            {
                var matchedString = match.ToString() ?? "";
                document = document.Replace(matchedString, matchedString.Substring(3, matchedString.Length - 7));
            }

            return document.Trim().Insert(0, "<p>") + "</p>";
        }
    }
}