using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.Exceptions;
using jiraF.Goal.API.ValueObjects;
using System.Collections.Generic;
using Xunit;

namespace jiraF.Goal.UnitTests.Domain;

public class GoalModelTests
{
    [Theory]
    [InlineData("Test value1")]
    [InlineData("Test value2")]
    public void EditLabel_CanEditLabelIfLabelExist_NewLabel(string newLabel)
    {
        // Arrange
        GoalModel goal = new(
            new Title("Test value"),
            new Description("Test value"),
            new User(),
            new User(),
            new LabelModel(new Title("Test value")));
        LabelModel firstLabel = new(new Title("Test value1"));
        LabelModel secondLabel = new(new Title("Test value2"));

        // Act
        goal.EditLabel(new List<LabelModel>() { firstLabel, secondLabel }, newLabel);

        // Assert
        Assert.Equal(newLabel, goal.Label.Title.Value);
    }

    [Theory]
    [InlineData("Invalid value1")]
    [InlineData("Invalid value2")]
    public void EditLabel_CannotEditLabelIfLabelNotExist_DomainException(string newInvalidLabel)
    {
        // Arrange
        GoalModel goal = new(
            new Title("Test value"),
            new Description("Test value"),
            new User(),
            new User(),
            new LabelModel(new Title("Test value")));
        LabelModel firstLabel = new(new Title("Test value1"));
        LabelModel secondLabel = new(new Title("Test value2"));

        // Act, Assert
        Assert.Throws<DomainException>(() => goal.EditLabel(new List<LabelModel>() { firstLabel, secondLabel }, newInvalidLabel));
    }
}
