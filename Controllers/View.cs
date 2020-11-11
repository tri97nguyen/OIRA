using Microsoft.AspNetCore.Mvc;
using parser.Data;
using parser.Models;
using System.Collections.Generic;
using System.Linq;

namespace parser.Controllers
{
    public class View : Controller
    {
        private AppDbContext _appDbContext;
        public View(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public IActionResult FacultyAndCourse()
        {
            IList<Faculty> faculty =  this._appDbContext.Faculty.ToList();
            return View(faculty);
        }
        public string test()
        {
            return "view homepage";
        }
    }
}