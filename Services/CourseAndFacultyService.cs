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
    public class CourseAndFacultyService
    {
        private readonly AppDbContext _appDbContext;
        public CourseAndFacultyService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private Faculty MapLineToFaculty(string line)
        {
            string[] column = line.Split(',');
            CourseSection course = new CourseSection { CRN = Convert.ToInt32(column[4]), 
                                                        Name = column[3], 
                                                        FacultyId = Convert.ToInt32(column[0]) };
            ICollection<CourseSection> courseList = new List<CourseSection>() { course };

            return new Faculty {Id = Convert.ToInt32(column[0]), FirstName = column[1], LastName = column[2], CourseSections = courseList};
        }
        //private CourseSection MapLineToCourseSection(string line)
        //{
        //    string[] column = line.Split(',');
        //    return new CourseSection { FacultyId = Convert.ToInt32(column[0]), Name = column[3], CRN = Convert.ToInt32(column[4])};
        //}
        

        public void ParseUploadFileToFacultyAndCourse(UploadCourseAndFacultyData facultyData)
        {
            IEnumerable<string> content = Upload.ReadAsList(facultyData.uploadFile);
            var faculties = content.Skip(1).Where(line => line.Length > 0).Select(line => MapLineToFaculty(line)).ToList();
            foreach (var faculty in faculties)
            {
                var matchedFaculty = _appDbContext.Faculty.Include(f => f.CourseSections).Where(f => f.Id == faculty.Id).FirstOrDefault();
                
                if (matchedFaculty != null)
                {
                    matchedFaculty.FirstName = faculty.FirstName;
                    matchedFaculty.LastName = faculty.LastName;
                    matchedFaculty.CourseSections = faculty.CourseSections;
                }
                else
                {
                    _appDbContext.Add(faculty);
                    
                }
            }
            _appDbContext.SaveChanges();
            
        }

        /**
         * parse course individually. Now handled in ParseUploadFileToFacultyAndCourse
        * 
        */

        //public void ParseUploadFileToCourseSection(UploadCourseAndFacultyData courseSectionData)
        //{
        //    IEnumerable<string> content = Upload.ReadAsList(courseSectionData.uploadFile);
        //    var courseSections = content.Skip(1).Where(line => line.Length > 0).Select(line => MapLineToCourseSection(line)).ToList();
        //    foreach (var courseSection in courseSections)
        //    {
        //        if(_appDbContext.CourseSections.Find(courseSection.CRN) != null)
        //        {

        //        }
        //        else
        //        {
        //            _appDbContext.Add(courseSection);
        //        }

        //    }
        //    _appDbContext.SaveChanges();

        //}
    }
}
