using Microsoft.AspNetCore.Identity;

namespace WorkLog.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Workday> Workdays { get; set; } = new List<Workday>();
}