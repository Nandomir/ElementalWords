using ElementalWords.Services;
using FluentAssertions;

namespace ElementalWords.Tests;

public class ElementWordFinderTests
{
    [Fact]
    public void ElementalForms_WithEmptyString_ShouldReturnEmptyArray()
    {
        // Arrange
        var word = string.Empty;

        // Act
        var result = ElementWordFinder.ElementalForms(word);

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Yes")]
    [InlineData("yes")]
    [InlineData("YES")]
    [InlineData("yEs")]
    public void ElementalForms_WithYesInVariousCases_ShouldReturnSingleFormWithTwoElements(string word)
    {
        // Act
        var result = ElementWordFinder.ElementalForms(word);

        // Assert
        result.Should().HaveCount(1);
        result[0].Should().HaveCount(2);
        result[0][0].Should().Be("Yttrium (Y)");
        result[0][1].Should().Be("Einsteinium (Es)");
    }

    [Fact]
    public void ElementalForms_WithSnack_ShouldReturnThreeDistinctForms()
    {
        // Arrange
        const string word = "snack";

        // Act
        var result = ElementWordFinder.ElementalForms(word);

        // Assert
        result.Should().HaveCount(3);

        result.Should().Contain(form =>
            form.Length == 4 &&
            form[0] == "Sulfur (S)" &&
            form[1] == "Nitrogen (N)" &&
            form[2] == "Actinium (Ac)" &&
            form[3] == "Potassium (K)");

        result.Should().Contain(form =>
            form.Length == 4 &&
            form[0] == "Sulfur (S)" &&
            form[1] == "Sodium (Na)" &&
            form[2] == "Carbon (C)" &&
            form[3] == "Potassium (K)");

        result.Should().Contain(form =>
            form.Length == 3 &&
            form[0] == "Tin (Sn)" &&
            form[1] == "Actinium (Ac)" &&
            form[2] == "Potassium (K)");
    }

    [Theory]
    [InlineData("xyz")]
    [InlineData("qq")]
    [InlineData("abc")]
    [InlineData("test")]
    [InlineData("1234")]
    [InlineData("a")]
    [InlineData("aaa")]
    [InlineData("zzz")]
    public void ElementalForms_WithImpossibleWords_ShouldReturnEmptyArray(string word)
    {
        // Act
        var result = ElementWordFinder.ElementalForms(word);

        // Assert
        result.Should().BeEmpty($"'{word}' cannot be formed using element symbols");
    }

    [Theory]
    [InlineData("hello world")]
    [InlineData("test-case")]
    [InlineData("word_test")]
    public void ElementalForms_WithSpecialCharacters_ShouldReturnEmptyArray(string word)
    {
        // Act
        var result = ElementWordFinder.ElementalForms(word);

        // Assert
        result.Should().BeEmpty($"'{word}' contains special characters that are not valid in element symbols");
    }

    [Fact]
    public void ElementalForms_WithMultipleValidPaths_ShouldReturnAllPossibleCombinations()
    {
        // Arrange
        const string word = "BaNaNa";

        // Act
        var result = ElementWordFinder.ElementalForms(word);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().OnlyContain(form => form.Length > 0);
        result.Should().OnlyContain(form => string.Join("", form.Select(e =>
            e.Substring(e.LastIndexOf('(') + 1).TrimEnd(')'))).Equals(word, StringComparison.OrdinalIgnoreCase));
    }
}