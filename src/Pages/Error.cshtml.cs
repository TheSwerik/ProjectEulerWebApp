using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ProjectEulerWebApp.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            Logger = logger;
        }

        private ILogger<ErrorModel> Logger { get; }

        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        public void Log(string msg)
        {
            Logger.LogInformation(msg);
        }
    }
}