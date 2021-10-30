using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        // GET: /<controller>/
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();

            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }

        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
            {
                string name = addEmployerViewModel.Name;
                string location = addEmployerViewModel.Location;

                List<Employer> existingItems = context.Employers //no duplicates
                    .Where(el => el.Name == name)
                    .Where(el => el.Location == location)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    Employer newEmployer = new Employer
                    {
                        Name = addEmployerViewModel.Name,
                        Location = addEmployerViewModel.Location
                    };
                    context.Employers.Add(newEmployer);
                    context.SaveChanges();
                    return Redirect("Index");

                }
                ViewBag.error = "The employer with same location already exist.";// if record exist,show error msg
                return View("Add", addEmployerViewModel);
            }

                return View("Add", addEmployerViewModel);
            }


        public IActionResult About(int id)
        {
            Employer theEmployer = context.Employers
                                 .Single(e => e.Id == id);
          return View(theEmployer);
        }
    }
}
