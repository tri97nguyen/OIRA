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

        private IEnumerable<string> ReadAsList(IFormFile file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0) result.Add(reader.ReadLine());
            }
            return result;
        }
        private Rubric MapLineToRubric(string line)
        {
            string[] column = line.Split(',');
            return new Rubric { Id = column[0], Name = column[1] };
        }
        public IEnumerable<Rubric> ParseUploadFileToRubrics(UploadRubricIdData rubricIdData)
        {
            IEnumerable<string> content = ReadAsList(rubricIdData.uploadFile);
            var rubrics = content.Skip(1).Where(line => line.Length > 0).Select(line => MapLineToRubric(line)).ToList();
            foreach (var rubric in rubrics)
            {
                if (_appDbContext.Rubrics.Find(rubric.Id) != null)
                {

                }
                else
                {
                    _appDbContext.Add(rubric);
                }
            }
            _appDbContext.SaveChanges();
            return rubrics;
        }
        private void ForeignConstraintTemporaryHelper()
        {

        }
    }
}
