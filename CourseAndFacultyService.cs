﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using oira.Data;
using oira.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace oira
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
            return new Faculty {Id = Convert.ToInt32(column[0]), FirstName = column[1], LastName = column[2]};
        }
        private CourseSection MapLineToCourseSection(string line)
        {
            string[] column = line.Split(',');
            return new CourseSection { FacultyId = Convert.ToInt32(column[0]), Name = column[3], CRN = Convert.ToInt32(column[4])};
        }
        
        public IEnumerable<Faculty> ParseUploadFileToFaculty(UploadCourseAndFacultyData facultyData)
        {
            IEnumerable<string> content = Upload.ReadAsList(facultyData.uploadFile);
            var faculties = content.Skip(1).Where(line => line.Length > 0).Select(line => MapLineToFaculty(line)).ToList();
            foreach (var faculty in faculties)
            {
                if (_appDbContext.Faculty.Find(faculty.Id) != null)
                {
                    
                }
                else
                {
                    _appDbContext.Add(faculty);
                    
                }
            }
            _appDbContext.SaveChanges();
            return faculties;
        }

        public IEnumerable<CourseSection> ParseUploadFileToCourseSection(UploadCourseAndFacultyData courseSectionData)
        {
            IEnumerable<string> content = Upload.ReadAsList(courseSectionData.uploadFile);
            var courseSections = content.Skip(1).Where(line => line.Length > 0).Select(line => MapLineToCourseSection(line)).ToList();
            foreach (var courseSection in courseSections)
            {
                if(_appDbContext.CourseSections.Find(courseSection.CRN) != null)
                {
                    
                }
                else
                {
                    _appDbContext.Add(courseSection);
                }
                
            }
            _appDbContext.SaveChanges();
            return courseSections;
        }

        private void ForeignConstraintTemporaryHelper()
        {

        }
    }
}
