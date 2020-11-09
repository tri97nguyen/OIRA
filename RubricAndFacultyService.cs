using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using parser.Data;
using parser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace parser
{
    public class RubricAndFacultyService
    {
        private readonly AppDbContext _appDbContext;
        public RubricAndFacultyService(AppDbContext appDbContext)
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
        private Faculty MapLineToFaculty(string line)
        {
            string[] column = line.Split(',');
            return new Faculty { Id = Convert.ToInt32(column[0]), FirstName = column[1], LastName = column[2], RubricId = column[3] };
        }

        public IEnumerable<Faculty> ParseUploadFileToFaculty(UploadRubricAndFacultyData facultyData)
        {
            IEnumerable<string> content = ReadAsList(facultyData.uploadFile);
            var faculties = content.Skip(1).Where(line => line.Length > 0).Select(line => MapLineToFaculty(line)).ToList();
            foreach (var faculty in faculties)
            {
                if (_appDbContext.Faculty.Find(faculty.Id) != null)
                {
                    _appDbContext.Update(faculty);
                }
                else
                {
                    _appDbContext.Add(faculty);

                }
            }
            _appDbContext.SaveChanges();
            return faculties;
        }

        private void ForeignConstraintTemporaryHelper()
        {

        }
    }
}
