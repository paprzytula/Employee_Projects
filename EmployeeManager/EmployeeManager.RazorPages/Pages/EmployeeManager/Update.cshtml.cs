using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.RazorPages.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.RazorPages.Pages.EmployeeManager
{
    [Authorize(Roles = "Manager")]
    public class UpdateModel : PageModel
    {
        private readonly AppDbContext context;

        public UpdateModel(AppDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public string Message { get; set; }
        public bool DataFound { get; set; } = true;

        public void FillCountries()
        {
            List<SelectListItem> listOfCountries = (from c in context.Employees
                                                    select new SelectListItem()
                                                    {
                                                        Text = c.Country,
                                                        Value = c.Country
                                                    }).Distinct().ToList();
            Countries = listOfCountries;
        }
        public void OnGet(int id)
        {
            FillCountries();
            Employee = context.Employees.Find(id);
            if (Employee == null)
            {
                DataFound = false;
                Message = "Employee ID wasn't found";
            }
            else
            {
                DataFound = true;
                Message = "";
            }
        }
        public void OnPost()
        {
            FillCountries();

            if (ModelState.IsValid)
            {
                try
                {
                    context.Employees.Update(Employee);
                    context.SaveChanges();
                    Message = "Employee updated successfully";
                }
                catch (DbUpdateException ex1)
                {

                    Message = ex1.Message;
                    if (ex1.InnerException != null)
                    {
                        Message += ": " + ex1.InnerException.Message;
                    }
                }
                catch (Exception ex2)
                {
                    Message = ex2.Message;
                }
            }
        }
    }
}
