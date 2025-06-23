using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkLog.Domain.Models;

namespace WorkLog.Infrastructure;

public class Context : IdentityDbContext<ApplicationUser>
{
    public DbSet<Workday> WorkDays { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}