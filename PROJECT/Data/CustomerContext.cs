using PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PROJECT.Data
{
    public class CustomerContext : IdentityDbContext<Admin>
    {
        public CustomerContext(DbContextOptions<CustomerContext> opts) : base(opts) { }

        public DbSet<Customer>      Customers     => Set<Customer>();
        public DbSet<Projects>      Projects      => Set<Projects>();
        public DbSet<ProjectImages> ProjectImages => Set<ProjectImages>();
        public DbSet<Admin>         Admin         => Set<Admin>();
    }
}
