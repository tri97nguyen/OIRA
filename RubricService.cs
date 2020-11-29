using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using parser.Data;
using parser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace parser
{
    public class RubricService
    {
        private readonly AppDbContext _appDbContext;
        public RubricService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        private Rubric MapLineToRubric(string line)
        {
            string[] column = line.Split(',');
            return new Rubric { Id = column[0], Name = column[1] };
        }
        public string ParseUploadFileToRubrics(UploadRubricIdData rubricIdData)
        {
            IEnumerable<string> content = Upload.ReadAsList(rubricIdData.uploadFile);
            var rubrics = content.Skip(1).Where(line => line.Length > 0).Select(line => MapLineToRubric(line)).ToList();
            foreach (var rubric in rubrics)
                if (_appDbContext.Rubrics.Find(rubric.Id) == null)
                    _appDbContext.Add(rubric);
            _appDbContext.SaveChanges();
            return null;
        }
    }
}
