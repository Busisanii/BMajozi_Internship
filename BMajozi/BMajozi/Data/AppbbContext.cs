using BMajozi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BMajozi.Data
{
    public class AppbbContext: DbContext
    {
        public AppbbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<User> User{ get; set; }
        public DbSet<Food> Food{ get; set; }
        public DbSet<Activity> Activity { get; set; }
    }
}
