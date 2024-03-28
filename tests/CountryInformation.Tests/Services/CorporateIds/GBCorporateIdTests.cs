using CountryInformation.Services.CorporateIds;
using FluentAssertions;

namespace CountryInformation.Tests.Services.CorporateIds;

public class GBCorporateIdTests
{
    [Theory]
    [InlineData("GB 815382334")]
    [InlineData("GB815382334")]
    [InlineData(null)]
    [InlineData("815382334")]
    public void Should_return_true(string? corporateId)
    {
        var result = GBCorporateId.Valid(corporateId);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("GB 815382335")]
    [InlineData("GB 81538233424242")]
    [InlineData("YHUWOCNYEX")]
    [InlineData("GH 815382334")]
    [InlineData("GB 81H382334")]
    public void Should_return_false(string? corporateId)
    {
        var result = GBCorporateId.Valid(corporateId);

        result.Should().BeFalse();
    }
}