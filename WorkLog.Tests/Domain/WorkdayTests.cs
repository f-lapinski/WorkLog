using WorkLog.Domain.Models;

namespace WorkLog.Tests.Domain;

public class WorkdayTests
{
    [Fact]
    public void Validate_EndDateTimeBeforeStartDateTime_ReturnsValidationError()
    {
        // Arrange
        var workday = new Workday
        {
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddHours(-1),
            Description = "Test"
        };

        // Act
        var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(workday);
        var validationResults = workday.Validate(validationContext).ToList();

        // Assert
        Assert.Single(validationResults);
        Assert.Contains("Date and Time validation failed: End Date", validationResults[0].ErrorMessage);
    }
    
    [Fact]
    public void Validate_ValidWorkday_ReturnsNoErrors()
    {
        // Arrange
        var workday = new Workday
        {
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddHours(8),
            Description = "Test"
        };

        // Act
        var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(workday);
        var validationResults = workday.Validate(validationContext).ToList();

        // Assert
        Assert.Empty(validationResults);
    }

    [Fact]
    public void Description_ExceedsMaxLength_ReturnsValidationError()
    {
        // Arrange
        var workday = new Workday
        {
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddHours(8),
            Description = new string('x', 501) // 501 znaków
        };

        // Act & Assert
        var exception = Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>(() =>
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(
                workday,
                new System.ComponentModel.DataAnnotations.ValidationContext(workday),
                true
            )
        );
    }
}