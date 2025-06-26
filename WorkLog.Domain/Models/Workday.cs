using System.ComponentModel.DataAnnotations;

namespace WorkLog.Domain.Models;

public class Workday : AuditableEntity, IValidatableObject
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Start Date and Time is required")]
    public DateTime StartDateTime { get; set; }

    [Required(ErrorMessage = "End Date and Time is required")]
    public DateTime EndDateTime { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;
    
    public TimeSpan? Duration => EndDateTime - StartDateTime;
    
    public required string ApplicationUserId { get; set; }
    public required ApplicationUser ApplicationUser { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDateTime <= StartDateTime)
        {
            yield return new ValidationResult(
                "Date and Time validation failed: End Date and Time must be after Start Date and Time.",
                [nameof(StartDateTime), nameof(EndDateTime)]
            );
        }
    }
}