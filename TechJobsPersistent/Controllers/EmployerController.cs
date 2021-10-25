using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

                List<Employer> existingItems = context.Employers
                    .Where(el => el.Name == name)
                    .Where(el => el.Location == location)
                    .ToList();

                if(existingItems.Count == 0)
                {
                    Employer newEmployer = new Employer
                    {
                        Name = addEmployerViewModel.Name,
                        Location = addEmployerViewModel.Location
                    };
                    context.Employers.Add(newEmployer);
                    context.SaveChanges();
                    }
                return Redirect("/Employers");

            }
            else
            {
                ViewBag.error = "The location you entered is already exists.";
                return View(addEmployerViewModel);
            }
        }
            

        public IActionResult About(int id)
        {
           // Employer theEmployer = context.employers
               // .Include(e => e.Location)
                //.Single(e => e.Id == id);
            //AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel(theEmployer);


            return View();
        }
    }
}
