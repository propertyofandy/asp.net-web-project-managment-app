using PROJECT.Services;
using PROJECT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PROJECT.Models;


/*
 this time around added layout in shared folder 
 along with updating to Entity frame work
 
-- needed to remove Customer ctor as it causes issues with Entity along with all ctor calls made
 */
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); 
builder.Services.AddSingleton<ICustomerService, CustomerService>();

builder.Services.AddDbContext<CustomerContext>(opts => opts.UseSqlite(
    builder.Configuration.GetConnectionString("CustomerConnectionString")
));

builder.Services.AddIdentity<Admin, IdentityRole>(
    options => {
        options.SignIn.RequireConfirmedEmail = false;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 9;
        options.Password.RequiredUniqueChars = 2;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.User.RequireUniqueEmail = true;

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    }
    ).AddEntityFrameworkStores<CustomerContext>();

var app = builder.Build();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<CustomerContext>();
context.Database.EnsureCreated();
DBSeed.SeedDatabase(context);

app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomeView}/{id?}"
);


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else app.UseDeveloperExceptionPage();

app.Run();
