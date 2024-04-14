using Microsoft.AspNetCore.Mvc;
using PROJECT.Models;
using PROJECT.Services;
using PROJECT.Data;
using Microsoft.AspNetCore.Authorization;

namespace PROJECT.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerContext _dbContext;

        public CustomerController(CustomerContext dbContext)
        {
            // injecting dependency
            _dbContext = dbContext;
        }


        //helper function to find customer or return null
        Customer? GetCustomer(int id) => _dbContext.Customers.ToList().FirstOrDefault(c => c.Id.Equals(id));

        // cretes Customer object puts it in bag and returns view with the data in the bag
        public ViewResult Info()
        {
            Customer cust = new Customer() { FirstName = "john", LastName = "mike" };

            ViewBag.cust = cust;
            return View();
        }

        [AllowAnonymous]
        public IActionResult ShowDetails(int id) 
        {
            Customer? cust = GetCustomer(id);

            var proj = from p in _dbContext.Projects
                       where  p.CustomerId == id
                       select p;
            double balance = 0;
            foreach(var p in proj)
            {
                balance += p.Cost;
            }
            ViewBag.balance = balance;  
            return View(cust);
        }


        // search string will search fname, lname, addr, pnumber fields of customer until a list of size > 0 is found 
        // otherwise will state no matching resauts 

        [HttpGet] [AllowAnonymous]
        public IActionResult ListAll(string searchString) 
        {
            ViewBag.searchString = searchString;
            var cust = _dbContext.Customers.AsEnumerable();
            Dictionary<int, double> balances = new();

            if (searchString != null)
            {
                //select * from customer where fname, lname, addr, phone contains search string 
                cust = cust.Where(
                    c => c.FirstName.Contains(searchString.ToLower())
                    ||   c.LastName.Contains(searchString.ToLower())
                    ||   c.Address.Contains(searchString.ToLower())
                    ||   c.PhoneNumber.Contains(searchString.ToLower())
                );

                

                
                cust = cust.Distinct(); // remove duplicates
            }
                foreach (var c in cust)
                {
                    var proj = from p in _dbContext.Projects
                               where p.CustomerId == c.Id
                               select p;

                    double balance = 0.0; 
                    foreach(var p in proj)
                    {
                        balance += p.Cost;
                    }
                    balances[c.Id] = balance;
                }

                ViewBag.balances = balances;
            return View(cust);
        }


        [HttpGet][Authorize]
        public IActionResult Add() => View(); 

        [HttpPost]
        [Authorize] // add customer than redirect to list
        public IActionResult Add(Customer cust) 
        {
            _dbContext.Customers.Add(cust);
            _dbContext.SaveChanges();
            return RedirectToAction("ListAll");
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id) => View( GetCustomer(id) );

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Customer cust)
        {
            

            var customer = GetCustomer( cust.Id );
            // view will not allow us to reach this point if customer is null
            // will instead ask to search or will go to EditSearchNullResualt()
            if ( cust != null)
            {
                customer.Address = cust.Address;
                customer.PhoneNumber = cust.PhoneNumber;
                _dbContext.SaveChanges();
            }
            return RedirectToAction("ListAll");
        }

        [Authorize]
        public ViewResult Delete() => View(_dbContext.Customers.ToList());

        [HttpGet]
        [Authorize]
        public IActionResult DeleteAction(int id) 
        {
            var cust = GetCustomer(id);
            if (cust != null)
            {
                // if customer is removed then we need to remove projects
                var proj = from p in _dbContext.Projects
                           where p.CustomerId == id
                           select p;

                // if we remove projects then we need to delete child images
                foreach(var p in proj)
                {
                    var img = from i in _dbContext.ProjectImages
                              where i.ProjectId == p.Id
                              select i;

                    _dbContext.ProjectImages.RemoveRange(img); // remove all proj images
                    _dbContext.Projects.Remove(p); // remove project
                }

                _dbContext.Customers.Remove(cust); // remove customer
                _dbContext.SaveChanges();
            }
            return RedirectToAction("ListAll");
        }

    }
}
