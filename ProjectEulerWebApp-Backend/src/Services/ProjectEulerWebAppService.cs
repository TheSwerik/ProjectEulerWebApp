using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEulerWebApp.Models.Contexts;

namespace ProjectEulerWebApp.Services
{
    public abstract class ProjectEulerWebAppService
    {
        protected readonly ProjectEulerWebAppContext _context;
        protected  ProjectEulerWebAppService(ProjectEulerWebAppContext context) => _context = context;

        protected IActionResult TrySaveChanges()
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

        protected IActionResult TrySaveChanges(object o)
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