using Exercises.Models.Data;
using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.ViewModels;
using AutoMapper;

namespace Exercises.Controllers
{
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult States() //View name must match
        {
            var state = StateRepository.GetAll();
            return View(state.ToList());
        }

        [HttpGet]
        public ActionResult Courses()
        {
            var model = CourseRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new Major());
        }

        [HttpGet]
        public ActionResult AddState()
        {
            return View(new State());
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View(new Course());
        }

        [HttpPost]
        public ActionResult AddMajor(Major major)
        {
            if (string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter Major Name");
            }

            if (ModelState.IsValid)
            {
                MajorRepository.Add(major.MajorName);
                return RedirectToAction("Majors");
            }
            else
            {
                return View(new Major());
            }
        }

        [HttpPost]
        public ActionResult AddState(State state)
        {
            if(string.IsNullOrEmpty(state.StateAbbreviation))
            {
                ModelState.AddModelError("StateAbbreviation", "Please enter State Abbr.");
            }

            if (string.IsNullOrEmpty(state.StateName))
            {
                ModelState.AddModelError("StateName", "Please enter State");
            }

            if (ModelState.IsValid)
            {
                StateRepository.Add(state);
                return RedirectToAction("States");
            }
            else
            {
                return View(new State());
            }            
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            if (string.IsNullOrEmpty(course.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please enter Course Name");
            }

            if (ModelState.IsValid)
            {
                CourseRepository.Add(course.CourseName);
                return RedirectToAction("Courses");
            }
            else
            {
                return View(new Course());
            }
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpGet]
        public ActionResult EditState(int stateId)
        {
            State state = StateRepository.Get(stateId);

            return View(state);
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult EditMajor(Major major)
        {
            if (string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter Major Name");
            }

            if (ModelState.IsValid)
            {
                MajorRepository.Edit(major);
                return RedirectToAction("Majors");
            }

            else
            {
                return View("EditMajor", major);
            }
        }

        [HttpPost]
        public ActionResult EditState(State state)
        {
            if (string.IsNullOrEmpty(state.StateAbbreviation))
            {
                ModelState.AddModelError("StateAbbreviation", "Please enter Abbr.");
            }

            if (ModelState.IsValid)
            {
                StateRepository.Edit(state);
                return RedirectToAction("States");
            }

            else
            {
                return View("EditState", state);
            }
        }

        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            if (string.IsNullOrEmpty(course.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please enter Course Name");
            }

            if (ModelState.IsValid)
            {
                CourseRepository.Edit(course);
                return RedirectToAction("Courses");
            }

            else
            {
                return View("EditCourse", course);
            }
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpGet]
        public ActionResult DeleteState(int stateId)
        {
            var state = StateRepository.Get(stateId);
            return View(state);
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult DeleteMajor(Major major)
        {
            MajorRepository.Delete(major.MajorId);
            return RedirectToAction("Majors");
        }

        [HttpPost]
        public ActionResult DeleteState(State state)
        {
            StateRepository.Delete(state.Id);
            return RedirectToAction("States");
        }

        [HttpPost]
        public ActionResult DeleteCourse(Course course)
        {
            CourseRepository.Delete(course.CourseId);
            return RedirectToAction("Courses");
        }

    }
}