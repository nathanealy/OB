using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<RetailUser> RetailUser { get; set; }
        public DbSet<Retail.Models.AccountActivity> AccountActivity { get; set; } = default!;
        public DbSet<Retail.Models.AccountInformation> AccountInformation { get; set; } = default!;
        public DbSet<Retail.Models.AssociatedAccount> AssociatedAccount { get; set; } = default!;
    }
}