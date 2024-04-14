using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECT.Data;
using PROJECT.Models;

namespace PROJECT.Controllers
{
    public class ProjectController : Controller
    {

        private CustomerContext _dbContext;

        public ProjectController(CustomerContext dbContext)
        {
            // injecting dependency
            _dbContext = dbContext;
        }

        Projects? GetProject(int id) => _dbContext.Projects.ToList().FirstOrDefault(p => p.Id.Equals(id));
        Customer? GetCustomer(int id) => _dbContext.Customers.ToList().FirstOrDefault(c => c.Id.Equals(id));
        
        [Authorize]
        public IActionResult ListAll() => View(_dbContext.Projects.ToList());

        [Authorize]
        public IActionResult Create(int id) 
        {
            if(GetCustomer(id) == null) return RedirectToAction("ListAll");

            ViewBag.Id = id;
            ViewBag.Customer = GetCustomer(id);

            return View();
        }

        [HttpPost]
        [Authorize]// add Project than redirect to list
        public IActionResult Create(Projects proj)
        {
            var project = new Projects();
      
            project.OrderedDate = proj.OrderedDate;
            project.Cost = proj.Cost;
            project.IsComplete = proj.IsComplete;
            project.CustomerId = proj.CustomerId;
            project.Customer = GetCustomer(proj.CustomerId);

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return RedirectToAction("ListAll");
        }


        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id) => View(GetProject(id));

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Projects proj)
        {
            
            if(proj == null) return RedirectToAction("ListAll");

            var project = GetProject(proj.Id); 
            // view will not allow us to reach this point if customer is null
            // will instead ask to search or will go to EditSearchNullResualt()
            if (project != null)
            {
                project.ProjType = proj.ProjType;
                project.IsComplete = proj.IsComplete;
                project.OrderedDate = proj.OrderedDate;
                project.Cost = (proj.Cost < 0)? project.Cost : proj.Cost;
                _dbContext.SaveChanges();
            }
            
            return RedirectToAction("ListAll");
        }

        [Authorize]
        public ViewResult Delete() => View(_dbContext.Projects.ToList());

        [HttpGet]
        [Authorize]
        public IActionResult DeleteAction(int id)
        {
            var proj = GetProject(id);
            if (proj != null)
            {
                var img = from i in _dbContext.ProjectImages
                          where i.ProjectId == proj.Id
                          select i;
                _dbContext.ProjectImages.RemoveRange(img); // delete images associated with proj 
                _dbContext.Projects.Remove(proj);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("ListAll");
        }

        [HttpGet]
        [Authorize]
        public IActionResult CustomerProjects(int id)
        {
            var proj = from p in _dbContext.Projects
                       where p.CustomerId == id
                       select p;
            ViewBag.Customer = GetCustomer(id);
            return View(proj);
        }

    }
}
