using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Controllers
{
    [Authorize(Roles ="Manager")]
    public class EmployeeManagerController : Controller
    {
        private AppDbContext context;
        public EmployeeManagerController(AppDbContext context) => this.context = context;

        public IActionResult List()
        {
            List<Employee> model = (from m in context.Employees orderby m.EmployeeID select m).ToList();
            //List<Employee> model = context.Employees.OrderBy(e => e.EmployeeID).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                context.Employees.Add(model);
                context.SaveChanges();
                ViewBag.Message = "Employee added successfully";

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            FillCountries();
            var model = context.Employees.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                context.Employees.Update(model);
                context.SaveChanges();
                ViewBag.Message = "Employee updated successfully";
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var model = context.Employees.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var model = context.Employees.Find(id);
            context.Employees.Remove(model);
            context.SaveChanges();
            TempData["Message"] = "Employee deleted successfully";
            return RedirectToAction("List");
        }


            private void FillCountries()
        {
            List<SelectListItem> listOfCountries = (from c in context.Employees
                                                    select new SelectListItem()
                                                    {
                                                        Text = c.Country,
                                                        Value = c.Country
                                                    }).Distinct().ToList();
            ViewBag.Countries = listOfCountries;
        }
    }
}
