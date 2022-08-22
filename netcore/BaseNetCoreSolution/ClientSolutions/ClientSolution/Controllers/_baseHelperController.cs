using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClientSolution.Controllers
{
    public class _baseHelperController : Controller
    {
        protected void _addModelError(string error)
        {
            ModelState.AddModelError("",error);
        }
        protected void _addModelError(List<string> errors)
        {
            errors.ToList().ForEach(i =>
            {
                ModelState.AddModelError("",i);
            });
        }
    }
}
