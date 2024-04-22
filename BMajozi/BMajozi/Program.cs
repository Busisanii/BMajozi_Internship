using BMajozi.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BMajozi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppbbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DataConnection")));

            builder.Services.AddRouting(opt =>
            {
                opt.AppendTrailingSlash = true;
                opt.LowercaseUrls = true;
            });

            var app = builder.Build();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseStaticFiles();
            app.UseRouting();

            Seed.Populate(app); //Populate database without running migration
            app.Run();

        }
    }
}
