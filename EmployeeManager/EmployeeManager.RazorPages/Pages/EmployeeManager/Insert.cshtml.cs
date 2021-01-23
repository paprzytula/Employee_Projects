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
    public class InsertModel : PageModel
    {
        private readonly AppDbContext context;
        [BindProperty]
        public Employee Employee { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public string Message { get; set; }

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
        public InsertModel(AppDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            FillCountries();
        }
        public void OnPost()
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                try
                {
                    context.Employees.Add(Employee);
                    context.SaveChanges();
                    Message = "Employee inserted successfully";
                }
                catch (DbUpdateException ex1)
                {

                    Message = ex1.Message;
                    if (ex1.InnerException!=null)
                    {
                        Message+=": " +ex1.InnerException.Message;
                    }
                }
                catch(Exception ex2)
                {
                    Message = ex2.Message;
                }
            }
        }
    }
}
