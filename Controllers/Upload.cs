using System.Collections.Generic;
using System.IO;
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
            rubricIdData.uploadFile.ReadAsList();
            return RedirectToAction("Success");
        }
        
    }
    public static class IFormFileExtention
    {
        public static List<string> ReadAsList(this IFormFile file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0) result.Add(reader.ReadLine());
            }
            foreach (var line in result) System.Console.WriteLine(line);
            return result;
        }
    }
}