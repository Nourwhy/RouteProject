using MailKit;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RouteProject.BLL.Interfaces;
using RouteProject.BLL.Repositories;
using RouteProject.DAL.Data.Contexts;
using RouteProject.DAL.Models;
using RouteProject.PL.Helper;
using RouteProject.PL.Mapping;
using RouteProject.PL.Services;
using RouteProject.PL.Settings;

namespace RouteProject.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); // Allow DI For
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>(); 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContext>(options => {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            
            }/*,ServiceLifetime.Singleton*/);

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M=>M.AddProfile(new EmployeeProfile()));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(config =>
            {

                config.LoginPath = "/Account/SignIn";
                
            
            
            }
                );
          
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddGoogle(o =>
            {
                o.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

            builder.Services.AddAuthentication(f =>
            {
                f.DefaultAuthenticateScheme = FacebookDefaults.AuthenticationScheme;
                f.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
            })
            .AddFacebook(f =>
            {
                f.ClientId = builder.Configuration["Authentication:Facebook:ClientId"];
                f.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"];
            });

            //Life Time
            //builder.Services.AddScoped(); //Create Object Life Time Per Request -Unreachable Object
            //builder.Services.AddTransient(); //Create Object Life Time per Operation
            //builder.Services.AddSingleton();//Create Object Life Time per App

            builder.Services.AddScoped<IScopedService, Scoped>();//Per Request
            builder.Services.AddTransient<ITransetService, Transet>();//Per Operation
            builder.Services.AddSingleton<ISingletonService,Singleton>();//PerApp
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.AddScoped<IMailServices, MailServices>();

            var app = builder.Build();

            app.UseHttpsRedirection();  
            app.UseHsts();             

            app.UseAuthentication();
            app.UseAuthorization();


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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{code?}");

            app.Run();
        }




        }
    }

