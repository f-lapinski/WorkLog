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
            Description = "Test",
            ApplicationUserId = Guid.NewGuid().ToString(),
            ApplicationUser = new ApplicationUser()
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
            Description = "Test",
            ApplicationUserId = Guid.NewGuid().ToString(),
            ApplicationUser = new ApplicationUser()
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
            Description = new string('x', 501), // 501 znaków
            ApplicationUserId = Guid.NewGuid().ToString(),
            ApplicationUser = new ApplicationUser()
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
    
    [Fact]
    public void ApplicationUser_WorkdayRelation_IsCorrectlyEstablished()
    {
        // Arrange
        var user = new ApplicationUser 
        { 
            Id = Guid.NewGuid().ToString(),
            UserName = "testuser@example.com"
        };

        var workday1 = new Workday
        {
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddHours(8),
            Description = "Test workday 1",
            ApplicationUserId = user.Id,
            ApplicationUser = user
        };

        var workday2 = new Workday
        {
            StartDateTime = DateTime.Now.AddDays(1),
            EndDateTime = DateTime.Now.AddDays(1).AddHours(8),
            Description = "Test workday 2",
            ApplicationUserId = user.Id,
            ApplicationUser = user
        };

        user.Workdays = new List<Workday> { workday1, workday2 };

        // Assert
        Assert.Equal(2, user.Workdays.Count);
        Assert.All(user.Workdays, w => Assert.Equal(user.Id, w.ApplicationUserId));
        Assert.All(user.Workdays, w => Assert.Same(user, w.ApplicationUser));
    }
}