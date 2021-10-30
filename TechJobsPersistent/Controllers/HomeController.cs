using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers,skills);
            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {

            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            if (ModelState.IsValid)
            {
                Employer theEmployer = employers.Find(e => e.Id == addJobViewModel.EmployerId);
                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = theEmployer,
                    JobSkills = new List<JobSkill>()
                };

                  foreach (var jobskill in selectedSkills)
                {
                    Skill skill = skills.Where(s => s.Id == int.Parse(jobskill)).First();//first match
                    JobSkill jobSkill = new JobSkill();
                    jobSkill.Skill = skill;
                    newJob.JobSkills.Add(jobSkill);
                }

                context.Jobs.Add(newJob);
                context.SaveChanges();
                return Redirect("Index");
            }
            AddJobViewModel addJobViewModel1 = new AddJobViewModel(employers, skills);

            return View("AddJob",addJobViewModel1);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
