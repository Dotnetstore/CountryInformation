using CountryInformation.Exceptions;
using CountryInformation.Services.CorporateIds;
using FluentAssertions;

namespace CountryInformation.Tests.Services.CorporateIds;

public class GermanCorporateIdTests
{
    [Theory]
    [InlineData("DE 136,695 976")]
    [InlineData("DE136695976")]
    [InlineData(null)]
    public void Should_return_true(string? corporateId)
    {
        var result = GermanCorporateId.Valid(corporateId);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("136695978")]
    [InlineData("13669597H")]
    [InlineData("13G69597H")]
    [InlineData("DE036695976")]
    [InlineData("DE13669597H")]
    public void Should_return_false(string? corporateId)
    {
        var result = GermanCorporateId.Valid(corporateId);

        result.Should().BeFalse();
    }
}