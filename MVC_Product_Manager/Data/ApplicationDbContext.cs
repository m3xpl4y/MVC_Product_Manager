using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Product_Manager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC_Product_Manager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
