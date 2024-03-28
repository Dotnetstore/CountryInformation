using CountryInformation.Exceptions;
using CountryInformation.Services.CorporateIds;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CountryInformation.Tests.Services.CorporateIds;

public class CorporateIdTests
{
    [Theory]
    [InlineData("556056-6258", "SE556056625801")]
    [InlineData("GB 815382334", "GB 815382334")]
    [InlineData("GB815382334", "GB815382334")]
    [InlineData("815382334", "815382334")]
    [InlineData("DE 136,695 976", "DE 136,695 976")]
    [InlineData("DE136695976", "DE136695976")]
    public void Should_return_expected_value(string corporateId, string expected)
    {
        var result = CorporateId.Create(corporateId);

        using (new AssertionScope())
        {
            result.Should().NotBeNull();
            result!.Value.Id.Should().Be(expected);
        }
    }

    [Theory]
    [InlineData(null)]
    public void Should_return_null(string? corporateId)
    {
        var result = CorporateId.Create(corporateId);

        result.Should().BeNull();
    }

    [Theory]
    [InlineData("I am testing")]
    [InlineData("GB 815382335")]
    [InlineData("GB 81538233424242")]
    [InlineData("YHUWOCNYEX")]
    [InlineData("GH 815382334")]
    [InlineData("GB 81H382334")]
    [InlineData("DE036695976")]
    [InlineData("DE13669597H")]
    public void Should_throw_exception(string corporateId)
    {
        var action = () => CorporateId.Create(corporateId);

        action.Should().ThrowExactly<CorporateIdException>("Invalid corporate id");
    }
}