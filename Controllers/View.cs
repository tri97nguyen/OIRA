using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oira.Data;
using oira.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace oira.Controllers
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
            IList<Faculty> faculty =  _appDbContext.Faculty.Include(i => i.CourseSections).ToList();
            return View(faculty);
        }
        public IActionResult FacultyAndRubric()
        {
            IList<Faculty> faculty = _appDbContext.Faculty.OrderByDescending(f => f.RubricId).ToList();
            return View(faculty);
        }
        public IActionResult RubricContent()
        {
            IList<RubricCriteria> rubricContents = _appDbContext.RubricCriteria.Include(r => r.RubricCriteriaElements).ToList();
            return View(rubricContents);
        }
    }
}