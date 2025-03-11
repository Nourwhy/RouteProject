using Microsoft.EntityFrameworkCore;
using RouteProject.BLL.Interfaces;
using RouteProject.BLL.Repositories;
using RouteProject.DAL.Data.Contexts;

namespace RouteProject.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); // Allow DI For
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>(); 
            builder.Services.AddDbContext<CompanyDbContext>(options => {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            
            });
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{code?}");

            app.Run();
        }
    }
}
