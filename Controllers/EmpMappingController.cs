using System;
using EMSProj.Data;
using EMSProj.Models;
using EMSProj.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EMSProj.Controllers
{
    public class EmpMappingController:Controller
    {
        private readonly ApplicationDbContext _db;

        public EmpMappingController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objEmp = from m in _db.EmpDepMapping
                         join e in _db.EmpDetails on m.EmployeeId equals e.EmployeeId
                         join d in _db.Departments on m.DepartmentId equals d.DepartmentId
                         select new EmpDepartMap
                         {
                             MapId = m.MapId,
                             EmployeeId = m.EmployeeId,
                             EmployeeName = e.FirstName + ' ' + e.LastName,
                             DepartmentId = m.DepartmentId,
                             DepartName = d.Name
                         };
            return View(objEmp);
        }

        public IActionResult Create()
        {
            //List<SelectListItem> depart = new List<SelectListItem> _db.Departments.ToList();
            ViewBag.Depart = _db.Departments.ToList();
            ViewBag.Employee = _db.EmpDetails.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmpDepartmentMap objEmp)
        {
            ViewBag.Depart = _db.Departments.ToList();
            ViewBag.Employee = _db.EmpDetails.ToList();

            if (ModelState.IsValid)
            {
                _db.EmpDepMapping.Add(objEmp);
                await _db.SaveChangesAsync();

                TempData["success"] = "New Mapping has been created successfully";
                return RedirectToAction("Index");
            }

            return View(objEmp);
        }

        public async Task<ActionResult> Edit(int? Id)
        {
            ViewBag.Depart = _db.Departments.ToList();
            ViewBag.Employee = _db.EmpDetails.ToList();

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var objEmp = await _db.EmpDepMapping.FindAsync(Id);

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
        public async Task<ActionResult> Edit(EmpDepartmentMap objEmp)
        {
            ViewBag.Depart = _db.Departments.ToList();
            ViewBag.Employee = _db.EmpDetails.ToList();

            if (ModelState.IsValid)
            {
                _db.EmpDepMapping.Update(objEmp);
                await _db.SaveChangesAsync();

                TempData["success"] = "Employee Mapping have been updated successfully";
                return RedirectToAction("Index");
            }

            return View(objEmp);
        }
    }
}
