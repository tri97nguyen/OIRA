using Microsoft.AspNetCore.Http;

namespace oira.Models
{
    public class UploadRubricIdData
    {
        public IFormFile uploadFile { get; set; }
    }
}