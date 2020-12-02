using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using parser;
using parser.Data;
using parser.Fitlers;
using parser.Models;
using static parser.Upload;

namespace parser.Controllers
{
    public class Upload : Controller
    {
        private readonly RubricService _rubricService;
        private readonly CourseAndFacultyService _courseAndFacultyService;
        private readonly RubricAndFacultyService _rubricAndFacultyService;
        public Upload(RubricService rubricService, CourseAndFacultyService courseAndFacultyService, RubricAndFacultyService rubricAndFacultyService)
        {
            _rubricService = rubricService;
            _courseAndFacultyService = courseAndFacultyService;
            _rubricAndFacultyService = rubricAndFacultyService;
        }
        public string Index()
        {
            return "Homepage for upload";
        }
        public IActionResult Success()
        {
            return View();
        }      
        public IActionResult Rubric()
        {
            return View(new UploadRubricIdData());
        }
        public IActionResult CourseAndFaculty()
        {
            return View(new UploadCourseAndFacultyData());
        }
        public IActionResult RubricAndFaculty()
        {
            return View(new UploadRubricAndFacultyData());
        }
        public IActionResult RubricContent()
        {
            return View(new UploadRubricContent());
        }
        [HttpPost]
        [UploadExceptionFilter]
        public IActionResult Rubric(UploadRubricIdData rubricIdData)
        {
            
            _rubricService.ParseUploadFileToRubrics(rubricIdData);
            return RedirectToAction(nameof(Success));
        }

        [HttpPost]
        [UploadExceptionFilter]
        public IActionResult CourseAndFaculty(UploadCourseAndFacultyData courseAndFacultyData)
        {
            _courseAndFacultyService.ParseUploadFileToFacultyAndCourse(courseAndFacultyData);
            //_courseAndFacultyService.ParseUploadFileToCourseSection(courseAndFacultyData); // legacy code: parse course individually
            return RedirectToAction("Success");
        }

        [HttpPost]
        [UploadExceptionFilter]
        public IActionResult RubricAndFaculty(UploadRubricAndFacultyData rubricAndFacultyData)
        {
            _rubricAndFacultyService.ParseUploadFileToFaculty(rubricAndFacultyData);
            return RedirectToAction("Success");
        }
        [HttpPost]
        //[UploadExceptionFilter]
        public IActionResult RubricContent(UploadRubricContent rubricContentData, [FromServices] AppDbContext context)
        {
            RubricContentService.ParseRubricContent(rubricContentData, context);
            return RedirectToAction("Success");
        }


    }

}