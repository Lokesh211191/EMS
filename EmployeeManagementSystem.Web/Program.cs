using EmployeeManagementSystem.Application.Mappings;
using EmployeeManagementSystem.Application.Services;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystem.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
var environment = builder.Environment;

// Use in-memory database for development, SQL Server for production
if (environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("EmployeeSystemDb"));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Seed the database with sample data
using (var scope = app.Services.CreateScope())
{
    await EmployeeManagementSystem.Web.Data.DataSeeder.InitializeAsync(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
