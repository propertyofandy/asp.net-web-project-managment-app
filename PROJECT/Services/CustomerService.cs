using PROJECT.Models;
namespace PROJECT.Services
{
    public class CustomerService:ICustomerService
    {
        public List<Customer> customers { get; set; }

        public CustomerService() 
        { // constructing list with five members for service
            customers = new List<Customer>();

            customers.Add(new Customer() 
            {
                FirstName = "Austin",
                LastName = "Anderson", 
                Address = "0 st se",
                PhoneNumber = "000-000-0000" 
            
            });
            customers.Add(new Customer()
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "1 st se",
                PhoneNumber = "000-000-0001"
            });
            customers.Add(new Customer() { 
                FirstName = "Mark",
                LastName = "Twain",
                Address = "2 st se",
                PhoneNumber = "000-000-0002" 
            });
            customers.Add(new Customer() { 
                FirstName = "Frier", 
                LastName = "John",
                Address = "3 st se", 
                PhoneNumber = "000-000-0003" 
            });
            customers.Add(new Customer() { 
                FirstName = "Jane", 
                LastName = "Doe", 
                Address = "4 st se", 
                PhoneNumber = "000-000-0004"
            });
        }
    }
}
