using PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using PROJECT.Data;
using PROJECT.Services;
namespace PROJECT.Models
{
    public static class DBSeed
    {
        public static void SeedDatabase(CustomerContext context)
        {
            context.Database.Migrate();

            if(context.Customers.ToList<Customer>().Count == 0)
            {
                List<Customer> customers = new List<Customer>();
                List<Projects> projects = new List<Projects>();
                customers.Add( new Customer() 
                { 
                    FirstName = "billy",
                    LastName  = "bob",
                    Address   = "123 a st",
                    PhoneNumber = "1234567890",

                });

                customers.Add(new Customer()
                {
                    FirstName = "jim",
                    LastName = "bob",
                    Address = "123 b st",
                    PhoneNumber = "1234567891",

                });

                customers.Add(new Customer()
                {
                    FirstName = "john",
                    LastName = "doe",
                    Address = "123 c st",
                    PhoneNumber = "1234567892",

                });

                customers.Add(new Customer()
                {
                    FirstName = "jane",
                    LastName = "doe",
                    Address = "123 d st",
                    PhoneNumber = "1234567893",

                });

                customers.Add(new Customer()
                {
                    FirstName = "joel",
                    LastName = "carhart",
                    Address = "123 f st",
                    PhoneNumber = "1234567899",

                });

                projects.Add(new Projects()
                {
                    ProjType = ProjectType.Bathroom,
                    IsComplete = true,
                    OrderedDate = DateTime.Now,
                    Cost = 100,
                    CustomerId = 0,
                    Customer = customers[0]
                });

                projects.Add(new Projects()
                {
                    ProjType = ProjectType.Deck,
                    IsComplete = true,
                    OrderedDate = DateTime.Now.AddDays(-154),
                    Cost = 200,
                    CustomerId = 1,
                    Customer = customers[1]
                });

                projects.Add(new Projects()
                {
                    ProjType = ProjectType.Kitchen,
                    IsComplete = false,
                    OrderedDate = DateTime.Now.AddMonths(-2),
                    Cost = 300,
                    CustomerId = 2,
                    Customer = customers[2]
                });

                projects.Add(new Projects()
                {
                    ProjType = ProjectType.Bathroom,
                    IsComplete = true,
                    OrderedDate = DateTime.Now.AddDays(-3),
                    Cost = 400,
                    CustomerId = 3,
                    Customer = customers[3]
                });

                projects.Add(new Projects()
                {
                    ProjType = ProjectType.AddOn,
                    IsComplete = true,
                    OrderedDate = DateTime.Now,
                    Cost = 500,
                    CustomerId = 4,
                    Customer = customers[4]
                });

                context.Projects.AddRange(projects);
                context.Customers.AddRange(customers);
            }

            context.SaveChanges();
        }
    }
}
