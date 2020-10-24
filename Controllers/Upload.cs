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
            return RedirectToAction("Success");
        }
        
    }
}