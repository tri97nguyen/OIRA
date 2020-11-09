using Microsoft.AspNetCore.Http;

namespace parser.Models
{
    public class UploadCourseData
    {
        public IFormFile uploadFile { get; set; }
    }
}

