using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using parser.Models;

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
        public IActionResult Rubric(UploadRubricIdData rubricIdData)
        {
            var rubrics = _rubricService.ParseUploadFileToRubrics(rubricIdData);
            foreach(var rubric in rubrics) System.Console.WriteLine($"{rubric.Id} {rubric.Name}");
            return RedirectToAction("Success");
        }

        [HttpPost]
        public IActionResult CourseAndFaculty(UploadCourseAndFacultyData courseAndFacultyData)
        {
            var faculties = _courseAndFacultyService.ParseUploadFileToFaculty(courseAndFacultyData);
            foreach (var faculty in faculties) System.Console.WriteLine($"{faculty.Id} {faculty.FirstName} {faculty.LastName}");
            var courses = _courseAndFacultyService.ParseUploadFileToCourseSection(courseAndFacultyData);
            foreach (var course in courses) System.Console.WriteLine($"{course.Name} {course.CRN}");
            return RedirectToAction("Success");
        }

        [HttpPost]
        public IActionResult RubricAndFaculty(UploadRubricAndFacultyData rubricAndFacultyData)
        {
            var faculties = _rubricAndFacultyService.ParseUploadFileToFaculty(rubricAndFacultyData);
            foreach (var faculty in faculties) System.Console.WriteLine($"{faculty.Id} {faculty.FirstName} {faculty.LastName} {faculty.RubricId}");
            return RedirectToAction("Success");
        }
        [HttpPost]
        public IActionResult RubricContent(UploadRubricContent rubricContentData)
        {
            
            return RedirectToAction("Success");
        }


    }

}