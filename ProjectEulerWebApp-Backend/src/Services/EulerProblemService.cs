using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ProjectEulerWebApp.Models.Contexts;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectEulerWebApp.Services
{
    public class EulerProblemService
    {
        private readonly ProjectEulerWebAppContext _context;
        public EulerProblemService(ProjectEulerWebAppContext context) => _context = context;
        public IActionResult GetDescription()
        {
            const string url = "https://projecteuler.net/minimal=11";
            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK) return null;
            var receiveStream = response.GetResponseStream() ?? throw new ArgumentNullException();
            StreamReader readStream = null;

            readStream = string.IsNullOrWhiteSpace(response.CharacterSet)
                             ? new StreamReader(receiveStream)
                             : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

            var data = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            data = data.Replace("<br />", "");
            
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\html.html", data);
            
            return new OkObjectResult(data);
        }
    }
}