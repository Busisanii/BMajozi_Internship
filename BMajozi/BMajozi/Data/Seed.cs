using Microsoft.EntityFrameworkCore;
using System;

namespace BMajozi.Data
{
    //Populates data to database, used in Program.cs
    public static class Seed
    {
        public static void Populate(IApplicationBuilder app)
        {
            AppbbContext context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<AppbbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
