using ElementalWords.Data;
using FluentAssertions;

namespace ElementalWords.Tests;

public class ElementsDataTests
{
    [Fact]
    public void Elements_ShouldContain118Elements()
    {
        // Assert
        ElementsData.Elements.Should().HaveCountGreaterOrEqualTo(118,
            "The periodic table should contain 118 known elements");
    }

    [Theory]
    [InlineData("H", "Hydrogen")]
    [InlineData("He", "Helium")]
    [InlineData("Na", "Sodium")]
    [InlineData("Uut", "Ununtrium")]
    [InlineData("Uup", "Ununpentium")]
    [InlineData("Uus", "Ununseptium")]
    [InlineData("Uuo", "Ununoctium")]
    public void Elements_WithKnownSymbol_ShouldReturnCorrectElementName(string symbol, string expectedName)
    {
        // Assert
        ElementsData.Elements.Should().ContainKey(symbol);
        ElementsData.Elements[symbol].Should().Be(expectedName);
    }

    [Fact]
    public void Elements_AllSymbols_ShouldHaveValidLength()
    {
        // Assert
        ElementsData.Elements.Keys.Should().OnlyContain(symbol =>
            symbol.Length >= 1 && symbol.Length <= 3,
            "Element symbols must be between 1 and 3 characters long");
    }

    [Fact]
    public void Elements_AllSymbols_ShouldStartWithCapitalLetter()
    {
        // Assert
        ElementsData.Elements.Keys.Should().OnlyContain(symbol =>
            char.IsUpper(symbol[0]),
            "All element symbols must start with a capital letter");
    }

    [Fact]
    public void Elements_ShouldBeReadOnlyDictionary()
    {
        // Assert
        ElementsData.Elements.Should().BeAssignableTo<IReadOnlyDictionary<string, string>>(
            "The elements dictionary should be immutable");
    }

    [Fact]
    public void Elements_ShouldNotContainDuplicateSymbols()
    {
        // Arrange
        var symbols = ElementsData.Elements.Keys.ToList();

        // Assert
        symbols.Should().OnlyHaveUniqueItems(
            "Each element symbol should be unique");
    }
}
