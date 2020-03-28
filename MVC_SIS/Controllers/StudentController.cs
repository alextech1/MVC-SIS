using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;
using AutoMapper;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            //var config = new MapperConfiguration(c => c.CreateMap<Student, StudentVM>());
            //var mapper = config.CreateMapper();

            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            if (string.IsNullOrEmpty(studentVM.Student.FirstName))
            {
                ModelState.AddModelError("Student.FirstName", "Please enter your first name.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.LastName))
            {
                ModelState.AddModelError("Student.LastName", "Please enter your last name.");
            }

            if ((studentVM.Student.Major.MajorId).Equals(null))
            {
                ModelState.AddModelError("Student.Major.MajorId", "Please enter Major.");
            }

            if ((studentVM.Student.GPA).Equals(null))
            {
                ModelState.AddModelError("Student.GPA", "Please enter your GPA.");
            }

            if ((studentVM.SelectedCourseIds).Equals(null))
            {
                ModelState.AddModelError("SelectedCourseIds", "Please enter Course.");
            }

            if (ModelState.IsValid)
            {
                studentVM.Student.Courses = new List<Course>();

                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));

                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

                StudentRepository.Add(studentVM.Student);

                return RedirectToAction("List");
            }
            else
            {
                var viewModel = new StudentVM();
                viewModel.SetCourseItems(CourseRepository.GetAll());
                viewModel.SetMajorItems(MajorRepository.GetAll());
                return View(viewModel);
            }


        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            StudentVM studentVM = new StudentVM();
            studentVM.Student = new Student();
            studentVM.Student = StudentRepository.Get(id);
            studentVM.SelectedCourseIds = studentVM.Student.Courses.Select(x => x.CourseId).ToList();
            studentVM.SetCourseItems(CourseRepository.GetAll());
            studentVM.SetMajorItems(MajorRepository.GetAll());
            studentVM.SetStateItems(StateRepository.GetAll());
            return View(studentVM);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentVM studentVM)
        {
            if (string.IsNullOrEmpty(studentVM.Student.FirstName))
            {
                ModelState.AddModelError("Student.FirstName", "Please enter your first name.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.LastName))
            {
                ModelState.AddModelError("Student.LastName", "Please enter your last name.");
            }

            if ((studentVM.Student.Major.MajorId).Equals(null))
            {
                ModelState.AddModelError("Student.Major.MajorId", "Please enter Major.");
            }

            if ((studentVM.Student.GPA).Equals(null))
            {
                ModelState.AddModelError("Student.GPA", "Please enter your GPA.");
            }

            if ((studentVM.SelectedCourseIds).Equals(null))
            {
                ModelState.AddModelError("SelectedCourseIds", "Please enter Course.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.Street1))
            {
                ModelState.AddModelError("Student.Address.Street1", "Please enter your Address Street1.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.City))
            {
                ModelState.AddModelError("Stuent.Address.City", "Please enter your Address City.");
            }

            if ((studentVM.Student.Address.State.StateAbbreviation).Equals(null))
            {
                ModelState.AddModelError("Student.Major.State.StateAbbreviation", "Please enter State.");
            }

            if (ModelState.IsValid)
            {
                studentVM.Student.Courses = new List<Course>();


                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));

                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
                studentVM.Student.Address.State = StateRepository.GetStateAbbr(studentVM.Student.Address.State.StateAbbreviation);
               
                StudentRepository.Edit(studentVM.Student);
                return RedirectToAction("List");
            }

            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                studentVM.SetStateItems(StateRepository.GetAll());
                return View(studentVM);
            }

        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            var student = StudentRepository.Get(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult DeleteStudent(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}