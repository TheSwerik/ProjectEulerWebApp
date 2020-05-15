using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEulerWebApp.Models.Contexts;

namespace ProjectEulerWebApp.Services
{
    public abstract class ProjectEulerWebAppService
    {
        protected readonly ProjectEulerWebAppContext Context;
        protected ProjectEulerWebAppService(ProjectEulerWebAppContext context) { Context = context; }

        protected IActionResult TrySaveChanges(params object[] o)
        {
            try
            {
                Context.SaveChanges();
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