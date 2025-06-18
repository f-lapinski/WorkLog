using System.ComponentModel.DataAnnotations;

namespace WorkLog.Domain.Models;

public class Workday : IValidatableObject
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Data i godzina rozpoczęcia jest wymagana")]
    public DateTime StartDateTime { get; set; }

    [Required(ErrorMessage = "Data i godzina zakończenia jest wymagana")]
    public DateTime EndDateTime { get; set; }

    [StringLength(500, ErrorMessage = "Opis nie może przekraczać 500 znaków")]
    public string Description { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDateTime <= StartDateTime)
        {
            yield return new ValidationResult(
                "Data i godzina zakończenia musi być późniejsza niż data i godzina rozpoczęcia",
                [nameof(StartDateTime), nameof(EndDateTime)]
            );
        }
    }
}