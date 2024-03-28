using CountryInformation.Exceptions;
using CountryInformation.Services.CorporateIds;
using CountryInformation.Services.SocialSecurityNumbers;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CountryInformation.Tests.Services.CorporateIds;

public class SwedishCorporateIdTests
{
    [Theory]
    [InlineData("556056-6258")]
    [InlineData("165560160680")]
    public void Should_be_correct_id(string id)
    {
        var valid = SwedishCorporateId.Valid(id);

        valid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("556056-6259")]
    public void Should_be_incorrect_id(string id)
    {
        var valid = SwedishCorporateId.Valid(id);

        valid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("556056-6258")]
    public void Parse_should_return_correct_object(string id)
    {
        var result = SwedishCorporateId.Parse(id);

        result.Should().BeOfType<SwedishCorporateId>();
    }
    
    [Theory]
    [InlineData("556056-6258")]
    public void TryParse_should_return_true(string id)
    {
        var result = SwedishCorporateId.TryParse(id, out var obj);

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            obj.Should().NotBeNull();
        }
    }
    
    [Theory]
    [InlineData("556056-6259")]
    public void TryParse_should_return_false(string id)
    {
        var result = SwedishCorporateId.TryParse(id, out var obj);

        using (new AssertionScope())
        {
            result.Should().BeFalse();
            obj.Should().BeNull();
        }
    }
    
    [Theory]
    [InlineData("556056-6258")]
    public void VatNumber_should_return_correct_format(string id)
    {
        var obj = new SwedishCorporateId(id);

        var result = obj.VatNumber;

        result.Should().Be("SE556056625801");
    }
    
    [Theory]
    [InlineData("7101263924")]
    public void IsSwedishSocialSecurityNumber_should_be_true(string id)
    {
        var obj = new SwedishCorporateId(id);

        var result = obj.IsSwedishSocialSecurityNumber;

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            var ssn = obj.SwedishSocialSecurityNumber;
            ssn.Should().BeOfType<SwedishSocialSecurityNumber>();
        }
    }
    
    [Theory]
    [InlineData("5560160680")]
    public void IsSwedishSocialSecurityNumber_should_be_false(string id)
    {
        var obj = new SwedishCorporateId(id);

        var result = obj.IsSwedishSocialSecurityNumber;

        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("5560160680", "Aktiebolag")]
    public void Type_should_be_correct(string id, string expected)
    {
        var obj = new SwedishCorporateId(id);

        var result = obj.Type;

        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("5560160680", "556016-0680")]
    [InlineData("7101263924", "710126-3924")]
    public void Format_should_return_correct_format(string id, string expected)
    {
        var obj = new SwedishCorporateId(id);

        var result = obj.Format();

        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("55601606800")]
    [InlineData("556016068")]
    public void Too_long_or_too_short_should_throw_exception(string id)
    {
        var action = () => SwedishCorporateId.Parse(id);

        action.Should().ThrowExactly<SwedishCorporateIdException>();
    }
    
    [Theory]
    [InlineData("175560160680")]
    public void Invalid_should_throw_exception(string id)
    {
        var action = () => SwedishCorporateId.Parse(id);

        action.Should().ThrowExactly<SwedishCorporateIdException>();
    }
}