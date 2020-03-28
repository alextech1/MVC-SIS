using Exercises.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercises.Models.Repositories
{
    public static class CourseRepository
    {
        private static List<Course> _courses;
        
        static CourseRepository()
        {
            // sample data
            _courses = new List<Course>
            {
                new Course { CourseId=1, CourseName="Art History" },
                new Course { CourseId=2, CourseName="Accounting 101" },
                new Course { CourseId=3, CourseName="Biology 101" },
                new Course { CourseId=4, CourseName="Business Law" },
                new Course { CourseId=5, CourseName="C# Fundamentals" },
                new Course { CourseId=6, CourseName="Economics 101" },
                new Course { CourseId=7, CourseName="Java Fundamentals" },
                new Course { CourseId=8, CourseName="Photography" }
            };
        }

        public static IEnumerable<Course> GetAll()
        {
            return _courses;
        }

        public static Course Get(int courseId)
        {
            return _courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public static void Add(string courseName)
        {
            Course course = new Course();
            course.CourseName = courseName;
            course.CourseId = _courses.Max(c => c.CourseId) + 1;

            _courses.Add(course);
        }

        public static void Edit(Course course)
        {
            var selectedCourse = _courses.FirstOrDefault(c => c.CourseId == course.CourseId);

            selectedCourse.CourseName = course.CourseName;
        }

        public static void Delete(int courseId)
        {
            _courses.RemoveAll(c => c.CourseId == courseId);
        }
    }
}