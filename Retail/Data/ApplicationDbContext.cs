﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Retail.Areas.Identity.Data;
using Retail.Models;

namespace Retail.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Retail.Models.CustomerRecord> CustomerRecord { get; set; } = default!;
        public DbSet<RetailUser> RetailUser { get; set; }
    }
}