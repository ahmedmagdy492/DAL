using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FormView> FormViews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<RoleForms> RoleForms { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Logging> Loggings { get; set; }
        public DbSet<Image> Images { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }
    }
}
