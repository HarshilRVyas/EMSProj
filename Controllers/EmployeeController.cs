using System;
using EMSProj.Data;
using EMSProj.Models;
using EMSProj.Util;
using EMSProj.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSProj.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sortExpression = "")
        {
            DataSort dataSort = new DataSort();

            IEnumerable<EmployeeViewModel> objEmp = from e in _db.EmpDetails
                                                    join d in _db.Departments on
                                                    e.DepartmentId equals d.DepartmentId
                                                    select new EmployeeViewModel
                                                    {
                                                        EmployeeId = e.EmployeeId,
                                                        FirstName = e.FirstName,
                                                        LastName = e.LastName,
                                                        Address = e.Address,
                                                        EmailAddress = e.EmailAddress,
                                                        Phone = e.Phone,
                                                        DateOfBirth = e.dateOfBirth,
                                                        DateOfJoin = e.dateOfJoin,
                                                        DepartmentId = e.DepartmentId,
                                                        DepartName = d.Name,
                                                        DepartDescrName = d.Description,
                                                        departmentList = _db.Departments.ToList()
                                                    };

            ViewData["SortParamFirstName"] = "firstname";
            ViewData["SortParamLastName"] = "lastname";

            SortOrder sortOrder;
            string sortProperty;

            switch (sortExpression.ToLower())
            {
                case "firstname_desc":
                    sortOrder = SortOrder.Descending;
                    sortProperty = "firstname";
                    ViewData["SortParamFirstName"] = "firstname";
                    break;

                case "lastname":
                    sortOrder = SortOrder.Ascending;
                    sortProperty = "description";
                    ViewData["SortParamLastName"] = "description_desc";
                    break;

                case "lastname_desc":
                    sortOrder = SortOrder.Descending;
                    sortProperty = "lastname_desc";
                    ViewData["SortParamLastName"] = "lastname";
                    break;

                default:
                    sortOrder = SortOrder.Ascending;
                    sortProperty = "firstname";
                    ViewData["SortParamFirstName"] = "firstname_desc";
                    break;
            }

            List<EmployeeViewModel> emp = dataSort.GetSortedEmp(objEmp, sortProperty, sortOrder);
            return View(emp);
        }

        public IActionResult Create()
        {
            //List<SelectListItem> depart = new List<SelectListItem> _db.Departments.ToList();
            ViewBag.Depart = _db.Departments.ToList();
            return View();
        }

        public async Task<ActionResult> Edit(int? Id)
        {
            ViewBag.Depart = _db.Departments.ToList();

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var objEmp = await _db.EmpDetails.FindAsync(Id);

            if (objEmp == null)
            {
                return NotFound();
            }
            else
            {
                return View(objEmp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee objEmp)
        {
            ViewBag.Depart = _db.Departments.ToList();

            if (ModelState.IsValid)
            {
                _db.EmpDetails.Add(objEmp);
                await _db.SaveChangesAsync();

                TempData["success"] = "New Employee created successfully";
                return RedirectToAction("Index");
            }

            return View(objEmp);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee objEmp)
        {
            ViewBag.Depart = _db.Departments.ToList();
            //TempData["msg"] = form["ddlDepartment"].ToStrin();

            if (ModelState.IsValid)
            {
                _db.EmpDetails.Update(objEmp);
                await _db.SaveChangesAsync();

                TempData["success"] = "Employee Details updated successfully";
                return RedirectToAction("Index");
            }

            return View(objEmp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var objEmp = await _db.EmpDetails.FindAsync(Id);

            if (objEmp == null)
            {
                return NotFound();
            }
            else
            {
                _db.EmpDetails.Remove(objEmp);
                await _db.SaveChangesAsync();

                TempData["success"] = "Employee Details Deleted successfully";
                return RedirectToAction("Index");
            }
        }
    }
}
