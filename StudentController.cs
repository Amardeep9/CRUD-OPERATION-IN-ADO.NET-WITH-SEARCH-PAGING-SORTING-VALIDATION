using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterviewPracticeforCRUDinADO.Models;

namespace InterviewPracticeforCRUDinADO.Controllers
{
    public class StudentController : Controller
    {
        StudentDataAccessLayer objstudent = new StudentDataAccessLayer();
        Student stud = new Student();
        
        public ActionResult Index(string search, string SortColumn="RollNo" ,string IconClass="fa-sort-asc", int PageNo = 1)
        {
            List<Student> lstStudent = new List<Student>();
            lstStudent = objstudent.GetAllStudent().ToList();
            
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "RollNo")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    lstStudent = lstStudent.OrderBy(temp => temp.RollNo).ToList();
                else
                    lstStudent = lstStudent.OrderByDescending(temp => temp.RollNo).ToList();
            }

            if (search != "" && search != null)

            {


                return View(lstStudent.Where(x => x.Name.StartsWith(search) || search == null).ToList());



            }
            /* Paging */
            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lstStudent.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            lstStudent = lstStudent.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();

            return View(lstStudent);
        }


        [HttpPost]

        public ActionResult Search(string search)

        {

            List<Student> lstStu = new List<Student>();

            lstStu = objstudent.GetAllStudent().ToList();

            if (search != "")

            {


                {

                    return View(lstStu.Where(x => x.Name.StartsWith(search) || search == null).ToList());

                }

            }



            return View("Index", lstStu);

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Student student)
        {
            if (ModelState.IsValid)
            {
                objstudent.AddStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Not Found");
            }
            Student student = objstudent.GetStudentData(id);

            if (student == null)
            {
                return new HttpNotFoundResult("Not Found");
            }
            return View(student);
        }

        [HttpPost]// submit operation
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind]Student student)
        {
            if (id != student.RollNo)
            {
                return new HttpNotFoundResult("Not Found");
            }
            if (ModelState.IsValid)
            {
                objstudent.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Not Found");
            }
            Student student = objstudent.GetStudentData(id);

            if (student == null)
            {
                return new HttpNotFoundResult("Not Found");
            }
            return View(student);
        }



        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Not Found");
            }
            Student student = objstudent.GetStudentData(id);

            if (student == null)
            {
                return new HttpNotFoundResult("Not Found");
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            objstudent.DeleteStudent(id);
            return RedirectToAction("Index");
        }







    }
}