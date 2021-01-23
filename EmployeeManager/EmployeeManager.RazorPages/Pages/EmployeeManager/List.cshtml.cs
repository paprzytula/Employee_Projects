using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeManager.RazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.RazorPages.Pages.EmployeeManager
{
    [Authorize(Roles ="Manager")]
    public class ListModel : PageModel
    {
        private readonly AppDbContext context;
        public List<Employee> Employees { get; set; }
        public ListModel(AppDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            Employees = (from e in context.Employees orderby e.EmployeeID select e).ToList();
        }
    }
}
