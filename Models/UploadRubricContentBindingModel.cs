using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oira.Models
{
    public class UploadRubricContent
    {
        public IFormFile Upload { get; set; }
    }
}
