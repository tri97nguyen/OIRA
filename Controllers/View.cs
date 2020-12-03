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
            IList<Faculty> faculty =  this._appDbContext.Faculty.Where(f => f.CourseSections.Count > 0).Include(i => i.CourseSections).ToList();
            return View(faculty);
        }
        public IActionResult FacultyAndRubric()
        {
            IList<Faculty> faculty = this._appDbContext.Faculty.Where(f => f.RubricId != null).OrderByDescending(f => f.RubricId).ToList();
            return View(faculty);
        }
        public IActionResult RubricContent()
        {
            IList<RubricCriteria> rubricContent = _appDbContext.RubricCriteria.Include(r => r.RubricCriteriaElements)
                                                                                //.Include(r => r.RubricId)                                     
                                                                                .ToList();
            foreach (var crit in rubricContent)
                crit.RubricCriteriaElements = crit.RubricCriteriaElements.OrderByDescending(ele => ele.ScoreValue).ToList();
            return View(rubricContent);
        }
        public IActionResult RubricMetadata()
        {
            IList<Rubric> rubricMetadata = _appDbContext.Rubrics.ToList();
            return View(rubricMetadata);
        }
    }
}