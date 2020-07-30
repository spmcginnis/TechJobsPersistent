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
        public IActionResult Add()
            // formerly AddJob
        {
            List<Employer> emps = context.Employers.ToList();
            //queries the database for employers
            List<Skill> skills = context.Skills.ToList();

            AddJobViewModel addJobViewModel = new AddJobViewModel(emps, skills);
            return View(addJobViewModel);
        }

        [HttpPost("/Add")]
        public IActionResult Add(AddJobViewModel addJobViewModel, List<int> selectedSkills)
            // formerly ProcessAddJobForm
        {
            if (ModelState.IsValid)
            {
                Employer emp = context.Employers.Find(addJobViewModel.EmployerId);
                // ^ Is this correct??

                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    EmployerId = emp.Id
                    //JobSkills
                };
                context.Jobs.Add(newJob);

                foreach (int skillId in selectedSkills)
                {
                    JobSkill newJobSkill = new JobSkill
                    {
                        Job = newJob, //Job type
                        JobId = newJob.Id, //int
                        Skill = context.Skills.Find(skillId), //Skill type
                        SkillId = skillId //int
                    };
                    context.JobSkills.Add(newJobSkill);
                }


                
                context.SaveChanges();

                return Redirect("/add");
                // For some reason, when I enter invalid data, the form select dropdown menu loses its values (fixed, but see below)
            }

            List<Employer> emps = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            addJobViewModel = new AddJobViewModel(emps, skills);
            // I had to add these lines in order to repopulate the dropdown when invalid data was entered.  It seems clunky to me.


            return View(addJobViewModel);
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
