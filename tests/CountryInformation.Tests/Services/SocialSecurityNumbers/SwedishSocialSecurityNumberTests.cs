using CountryInformation.Exceptions;
using CountryInformation.Services.SocialSecurityNumbers;
using FluentAssertions;

namespace CountryInformation.Tests.Services.SocialSecurityNumbers;

public class SwedishSocialSecurityNumberTests
{
    [Theory]
    [InlineData("20000101T220")]
    [InlineData("000101T220")]
    [InlineData("000101-T220")]
    [InlineData("20000101-T220")]
    public void Interim_number_should_be_valid_when_options_is_set_on_true(string ssn)
    {
        var result = new SwedishSocialSecurityNumber(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true,
            AllowInterimNumber = true
        });

        result.Should().BeOfType<SwedishSocialSecurityNumber>();
    }
    
    [Theory]
    [InlineData("20000101T220")]
    [InlineData("000101T220")]
    [InlineData("000101-T220")]
    [InlineData("20000101-T220")]
    public void Interim_number_should_not_be_valid_when_options_is_not_set(string ssn)
    {
        var action = () => new SwedishSocialSecurityNumber(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        action.Should().ThrowExactly<SwedishSocialSecurityNumberException>();
    }
    
    [Theory]
    [InlineData("20000101T220")]
    [InlineData("000101T220")]
    [InlineData("000101-T220")]
    [InlineData("20000101-T220")]
    public void AllowCoordinationNumber_fale_should_throw_exception(string ssn)
    {
        var action = () => new SwedishSocialSecurityNumber(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = false
        });

        action.Should().ThrowExactly<SwedishSocialSecurityNumberException>();
    }
    
    [Theory]
    [InlineData("197101263924")]
    [InlineData("7101263924")]
    [InlineData("710126-3924")]
    [InlineData("19710126-3924")]
    public void Valid_ssn_in_parse_should_return_correct_object(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        result.Should().BeOfType<SwedishSocialSecurityNumber>();
    }
    
    [Theory]
    [InlineData("197101263925")]
    [InlineData("7101263925")]
    [InlineData("710126-3925")]
    [InlineData("19710126-3925")]
    public void Invalid_ssn_in_parse_should_throw_exception(string ssn)
    {
        var action = () => SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        action.Should().ThrowExactly<SwedishSocialSecurityNumberException>();
    }
    
    [Theory]
    [InlineData("197101263924")]
    [InlineData("7101263924")]
    [InlineData("710126-3924")]
    [InlineData("19710126-3924")]
    public void Valid_ssn_in_parse_should_return_correct_date(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        result.Date.Should().Be(DateTime.Parse("1971-01-26"));
    }
    
    [Theory]
    [InlineData("197101013923")]
    [InlineData("7101013923")]
    [InlineData("710101-3923")]
    [InlineData("19710101-3923")]
    public void Valid_ssn_in_parse_should_return_correct_age(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        result.Age.Should().Be(DateTime.Now.Year - result.Date.Year);
    }
    
    [Theory]
    [InlineData("197101013923")]
    [InlineData("7101013923")]
    [InlineData("710101-3923")]
    [InlineData("19710101-3923")]
    public void Valid_ssn_in_parse_should_return_correct_separator(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        result.Separator.Should().Be("-");
    }
    
    [Theory]
    [InlineData("197101013923")]
    [InlineData("7101013923")]
    [InlineData("710101-3923")]
    [InlineData("19710101-3923")]
    public void Valid_ssn_in_parse_should_return_correct_century(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        result.Century.Should().Be("19");
    }
    
    [Theory]
    [InlineData("197101013923")]
    [InlineData("7101013923")]
    [InlineData("710101-3923")]
    [InlineData("19710101-3923")]
    public void Valid_ssn_in_parse_should_return_correct_is_male(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        result.IsMale.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("19710101392")]
    [InlineData("710101392")]
    [InlineData("710101-392")]
    [InlineData("19710101-392")]
    public void Too_short_ssn_should_throw_exception(string ssn)
    {
        var action = () => SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        action.Should().ThrowExactly<SwedishSocialSecurityNumberException>();
    }
    
    [Theory]
    [InlineData("1971010139233")]
    [InlineData("71010139233")]
    [InlineData("710101-39233")]
    [InlineData("19710101-39233")]
    public void Too_long_ssn_should_throw_exception(string ssn)
    {
        var action = () => SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        action.Should().ThrowExactly<SwedishSocialSecurityNumberException>();
    }
    
    [Theory]
    [InlineData("190001013929")]
    [InlineData("19000101-3929")]
    [InlineData("19000101+3929")]
    [InlineData("000101+3929")]
    public void Older_than_100_should_return_correct_separator(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        result.Separator.Should().Be("+");
    }
    
    [Theory]
    [InlineData("194205669899")]
    [InlineData("4205669899")]
    [InlineData("420566-9899")]
    [InlineData("19420566-9899")]
    public void Coordination_number_day_greater_than_60_should_throw_exception(string ssn)
    {
        var action = () => SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = true
        });

        action.Should().ThrowExactly<SwedishSocialSecurityNumberException>();
    }
    
    [Theory]
    [InlineData("197302889931")]
    [InlineData("7302889931")]
    [InlineData("730288-9931")]
    [InlineData("19730288-9931")]
    public void Coordination_number_set_to_false_should_throw_exception1(string ssn)
    {
        var action = () => SwedishSocialSecurityNumber.Parse(ssn, new SwedishSocialSecurityNumber.Options
        {
            AllowCoordinationNumber = false
        });

        action.Should().ThrowExactly<SwedishSocialSecurityNumberException>();
    }
    
    [Theory]
    [InlineData("197101013923")]
    [InlineData("7101013923")]
    [InlineData("710101-3923")]
    [InlineData("19710101-3923")]
    public void Format_should_return_correct_format(string ssn)
    {
        var obj = new SwedishSocialSecurityNumber(ssn);

        var result = obj.Format();

        result.Should().Be("710101-3923");
    }
    
    [Theory]
    [InlineData("197101013923")]
    [InlineData("7101013923")]
    [InlineData("710101-3923")]
    [InlineData("19710101-3923")]
    public void Valid_should_return_true(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Valid(ssn);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("197101013922")]
    [InlineData("7101013922")]
    [InlineData("710101-3922")]
    [InlineData("19710101-3922")]
    public void Valid_should_return_false(string ssn)
    {
        var result = SwedishSocialSecurityNumber.Valid(ssn);

        result.Should().BeFalse();
    }
}