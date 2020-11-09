using Microsoft.AspNetCore.Http;

namespace parser.Models
{
    public class UploadRubricIdData
    {
        public IFormFile uploadFile { get; set; }
    }
}