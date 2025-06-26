using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkLog.Domain.Models;

namespace WorkLog.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(u => u.Workdays)
            .WithOne(w => w.ApplicationUser)
            .HasForeignKey(w => w.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}