using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Models
{
    /*A DbContext instance represents a combination of the Unit Of Work and Repository
        patterns such that it can be used to query from a database and group together changes
        that will then be written back to the store as a unit. 
    */
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Database path 
            optionsBuilder.UseSqlite(@"DataSource=database\\mydatabase.db;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed admin role
            string ADMIN_ID = "02174cf0–9412–4cfe - afbf - 59f706d72cf6";
            string ROLE_ID = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6";
            
            //create user
            var users = new User
            {
                id = 1,
                username= "matteoschirinzi01@gmail.com",
                type="W",
                isApproved=1,
                companyid=1

               
            };

            // create company
            var companies = new company
            {
                id = 1,
                name = "Company 1",
                water = 0
            };

            //Generate data
            builder.Entity<User>().HasData(users);
            builder.Entity<company>().HasData(companies);

        }

        public DbSet<crop>  crops { get; set; } 
        public DbSet<Typem> types { get; set; }  
        public DbSet<actuator>  actuators { get; set; } 
        public DbSet<Sensor>    sensors { get; set; }   
        public DbSet<water>     Water { get;set; }
        public  DbSet<company> companies { get; set; }
        public DbSet<waterrequest> waterrequests { get; set; }
        public DbSet<User> Users { get; set; }  
    }
}
