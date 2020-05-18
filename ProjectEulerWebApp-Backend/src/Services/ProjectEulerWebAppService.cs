using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectEulerWebApp.Models.Contexts;
using ProjectEulerWebApp.Pages;

namespace ProjectEulerWebApp.Services
{
    public abstract class ProjectEulerWebAppService
    {
        protected readonly ProjectEulerWebAppContext Context;
        private readonly int _logId;

        protected ProjectEulerWebAppService(ProjectEulerWebAppContext context,
                                            ILogger<ProjectEulerWebAppService> logger, int logId)
        {
            Context = context;
            Logger = logger;
            _logId = logId;
        }

        protected IActionResult TrySaveChanges(params object[] o)
        {
            try
            {
                Context.SaveChanges();
                return o.Length switch
                       {
                           0 => new OkResult(),
                           1 => new OkObjectResult(o[0]),
                           _ => new OkObjectResult(o)
                       };
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return new BadRequestObjectResult(o);
            }
        }

        private ILogger<ProjectEulerWebAppService> Logger { get; }

        public void Info(string msg) { Logger.LogInformation(_logId, msg); }
        public void Warn(string msg) { Logger.LogWarning(_logId, msg); }
    }
}