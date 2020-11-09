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
        public Upload(RubricService rubricService, CourseAndFacultyService courseAndFacultyService)
        {
            _rubricService = rubricService;
            _courseAndFacultyService = courseAndFacultyService;
        }
        public string Index()
        {
            return "Homepage for upload";
        }
        public string SuccessRubric()
        {
            return "upload rubric success";
        }
        public string SuccessFaculty()
        {
            return "upload faculty success";
        }
        public IActionResult Rubric()
        {
            return View(new UploadRubricIdData());
        }
        public IActionResult CourseAndFaculty()
        {
            return View(new UploadCourseAndFacultyData());
        }
        [HttpPost]
        public IActionResult Rubric(UploadRubricIdData rubricIdData)
        {
            var rubrics = _rubricService.ParseUploadFileToRubrics(rubricIdData);
            foreach(var rubric in rubrics) System.Console.WriteLine($"{rubric.Id} {rubric.Name}");
            return RedirectToAction("SuccessRubric");
        }

        [HttpPost]
        public IActionResult CourseAndFaculty(UploadCourseAndFacultyData courseAndFacultyData)
        {
            var faculties = _courseAndFacultyService.ParseUploadFileToFaculty(courseAndFacultyData);
            foreach (var faculty in faculties) System.Console.WriteLine($"{faculty.Id} {faculty.FirstName} {faculty.LastName}");
            var courses = _courseAndFacultyService.ParseUploadFileToCourseSection(courseAndFacultyData);
            foreach (var course in courses) System.Console.WriteLine($"{course.Name} {course.CRN}");
            return RedirectToAction("SuccessFaculty");
        }


    }

}