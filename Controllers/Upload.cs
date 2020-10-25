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
        public Upload(RubricService rubricService)
        {
            _rubricService = rubricService;
        }
        public string Index()
        {
            return "Homepage for upload";
        }
        public string Success()
        {
            return "upload rubric success";
        }
        public IActionResult Rubric()
        {
            return View(new UploadRubricIdData());
        }
        [HttpPost]
        public IActionResult Rubric(UploadRubricIdData rubricIdData)
        {
            var rubrics = _rubricService.ParseUploadFileToRubrics(rubricIdData);
            foreach(var rubric in rubrics) System.Console.WriteLine($"{rubric.RubricId} {rubric.Name}");
            return RedirectToAction("Success");
        }

    }

}