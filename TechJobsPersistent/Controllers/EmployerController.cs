using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;


namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }



        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> emps = context.Employers.ToList();
            return View(emps);
        }

        // GET: /<controller>/
        public IActionResult Add() //retrieves the add form
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }
        
        [HttpPost("/employer/add")]
        public IActionResult Add(AddEmployerViewModel addEmployerViewModel)
            //n.b. The examples use an overloaded Add() method, so I'm following that instead of ProcessAddEmployerForm.
            //ensure that only valid Employer objects are being saved to the database.
        {

            if (ModelState.IsValid)
            {
                Employer newEmployer = new Employer
                {
                    Name = addEmployerViewModel.Name,
                    Location = addEmployerViewModel.Location
                };
                context.Employers.Add(newEmployer);
                context.SaveChanges();
                return Redirect("/employer/add");
            };

            return View(addEmployerViewModel);
        }

        public IActionResult About(int id=1)
        {
            //Pass an Employer object to the view for display
            ViewBag.theEmp = context.Employers.Find(id);
            return View();
        }
    }
}
