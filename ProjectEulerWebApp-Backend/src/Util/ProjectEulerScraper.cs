using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

// TODO Async Load all existing Problems

// ReSharper disable InconsistentNaming
namespace ProjectEulerWebApp.Util
{
    public enum EulerProblemPart
    {
        Title,
        Description,
        PublishDate,
        Difficulty
    }

    public static class ProjectEulerScraper
    {
        private const string EulerProblemURL = "https://projecteuler.net/problem=";
        private const string EulerDescriptionURL = "https://projecteuler.net/minimal=";
        private static readonly HttpClient Client = new HttpClient();

        private static async Task<HtmlDocument> GetDocument(string url)
        {
            using var response = await Client.GetAsync(url);
            using var content = response.Content;
            var result = await content.ReadAsStringAsync();

            var document = new HtmlDocument();
            document.LoadHtml(result);
            return document;
        }

        public static Dictionary<EulerProblemPart, string> GetAll(int id)
        {
            var document = GetDocument(EulerProblemURL + id).Result;
            var info = document.DocumentNode.SelectSingleNode("(//span[@class='info']/span)[2]").InnerHtml.Split("<br>");
            return new Dictionary<EulerProblemPart, string>
                   {
                       {EulerProblemPart.Title, document.DocumentNode.ChildNodes.FindFirst("h2").InnerHtml},
                       {EulerProblemPart.Description, GetDescription(id)},
                       {EulerProblemPart.PublishDate, info[0].Substring(13,info[0].IndexOf(';') - 13)},
                       {EulerProblemPart.Difficulty, info[1]}
                   };
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

        public static async Task<bool> IsAvailable(string url)
        {
            using var response = await Client.GetAsync(url);
            return response.IsSuccessStatusCode;
        }

        public static bool ProblemExists(string url)
        {
            var document = GetDocument(url).Result;
            return !document.Text.Contains("Problems Archives");
        }
    }
}