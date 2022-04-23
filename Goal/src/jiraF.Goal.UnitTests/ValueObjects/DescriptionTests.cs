using jiraF.Goal.API.Exceptions;
using jiraF.Goal.API.ValueObjects;
using Xunit;

namespace jiraF.Goal.UnitTests.ValueObjects;

public class DescriptionTests
{
    private const string VALUE_GREATER_THAN_50_CHARACTERS = "123456789112345678921234567893123456789412345678951";

    [Fact]
    public void Ctor_CanCreateValidDescription_Description()
    {
        // Arrange
        string validValue = "Test description value";

        // Act
        Description result = new(validValue);

        // Assert
        Assert.Equal(validValue, result.Value);
    }

    [Fact]
    public void Ctor_CannotCreateValidDescriptionIfValueIsSpace_ValueObjectException()
    {
        // Arrange
        string invalidValue = " ";

        // Act, Assert
        Assert.Throws<ValueObjectException>(() => new Description(invalidValue));
    }

    [Theory]
    [InlineData("1")]
    [InlineData(VALUE_GREATER_THAN_50_CHARACTERS)]
    public void Ctor_CannotCreateValidDescriptionIfLengthIsInvalie_ValueObjectException(string invalidValue)
    {
        // Act, Assert
        Assert.Throws<ValueObjectException>(() => new Description(invalidValue));
    }
}
