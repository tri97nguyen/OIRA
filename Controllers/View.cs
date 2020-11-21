using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parser.Data;
using parser.Models;
using System;
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
            IList<Faculty> faculty =  this._appDbContext.Faculty.Include(i => i.CourseSections).ToList();
            return View(faculty);
        }
        public IActionResult FacultyAndRubric()
        {
            IList<Faculty> faculty = this._appDbContext.Faculty.OrderByDescending(f => f.RubricId).ToList();
            return View(faculty);
        }
    }
}