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

            IEnumerable<string> content = rubricIdData.uploadFile.ReadAsList();
            var mapping = content.Skip(1).Where(line => line.Length > 0).Select(line => Service.MapLineToRubric(line)).ToList();
            foreach(var rubric in mapping) System.Console.WriteLine($"{rubric.RubricId} {rubric.Name}");
            return RedirectToAction("Success");
        }

    }
    public static class Service
    {
        public static IEnumerable<string> ReadAsList(this IFormFile file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0) result.Add(reader.ReadLine());
            }
            return result;
        }
        public static Rubric MapLineToRubric(string line) 
        {
            string[] column = line.Split(',');
            return new Rubric { RubricId=column[0], Name=column[1]};
        }
    }
}