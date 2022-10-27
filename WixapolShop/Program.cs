using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Wixapol_DataAccess.MappingHelpers;
using Wixapol_DataAccess.UnitOfWork.Implementation;
using Wixapol_DataAccess.UnitOfWork.Interface;
using WixapolShop.Areas.Identity.Models.Domain;
using WixapolShop.AuthenticationRepository.Implementation;
using WixapolShop.IdentityRepository.Interfaces;

namespace WixapolShop
{


    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));

            });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            //IDENTITY
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDatabaseContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/UserAuthentication/Login";

            });

            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();


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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                 name: "default",
                 pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
