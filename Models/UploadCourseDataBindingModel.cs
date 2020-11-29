using Microsoft.AspNetCore.Http;

namespace oira.Models
{
    public class UploadCourseData
    {
        public IFormFile uploadFile { get; set; }
    }
}

