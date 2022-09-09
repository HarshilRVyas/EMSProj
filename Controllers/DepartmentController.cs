using System;
using EMSProj.Data;
using EMSProj.Models;
using EMSProj.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSProj.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index(string sortExpression = "", string searchText = "", int pg = 1, int pageSize = 5)
        {
            IEnumerable<Department> objDepart = await _db.Departments.ToListAsync();
            DataSort dataSort = new DataSort();

            ViewData["SortParamName"] = "name";
            ViewData["SortParamDescr"] = "description";

            SortOrder sortOrder;
            string sortProperty;

            switch (sortExpression.ToLower())
            {
                case "name_desc":
                    sortOrder = SortOrder.Descending;
                    sortProperty = "name";
                    ViewData["SortParamName"] = "name";
                    break;

                case "description":
                    sortOrder = SortOrder.Ascending;
                    sortProperty = "description";
                    ViewData["SortParamDescr"] = "description_desc";
                    break;

                case "description_desc":
                    sortOrder = SortOrder.Descending;
                    sortProperty = "description_desc";
                    ViewData["SortParamDescr"] = "description";
                    break;

                default:
                    sortOrder = SortOrder.Ascending;
                    sortProperty = "name";
                    ViewData["SortParamName"] = "name_desc";
                    break;
            }

            objDepart = dataSort.GetDepartmentSearch(objDepart, searchText.ToLower());
            List<Department> depart = dataSort.GetSortedDepart(objDepart, sortProperty, sortOrder);

            var pager = new PageModel(depart.Count, pg, pageSize);
            pager.sortExpression = sortExpression;
            ViewBag.Pager = pager;

            depart = depart.Skip((pg-1)*pageSize).Take(pageSize).ToList();  

            return View(depart);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var objDepart = await _db.Departments.FindAsync(Id);

            if (objDepart == null)
            {
                return NotFound();
            }
            else
            {
                return View(objDepart);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department objDepart)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Add(objDepart);
                await _db.SaveChangesAsync();

                TempData["success"] = "Department created successfully";
                return RedirectToAction("Index");
            }

            return View(objDepart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Department objDepart)
        {
            if (ModelState.IsValid)
            {
                var departDB = await _db.Departments.FirstOrDefaultAsync(x => x.DepartmentId == objDepart.DepartmentId);

                if (departDB != null)
                {
                    departDB.Name = objDepart.Name;
                    departDB.Description = objDepart.Description;

                    await _db.SaveChangesAsync();

                    TempData["success"] = "Department updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(objDepart);
                }
            }
            else
            {
                return View(objDepart);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var objDepart = await _db.Departments.FindAsync(Id);

            if (objDepart == null)
            {
                return NotFound();
            }
            else
            {
                _db.Departments.Remove(objDepart);
                await _db.SaveChangesAsync();

                TempData["success"] = "Department Deleted successfully";
                return RedirectToAction("Index");
            }
        }
    }
}
