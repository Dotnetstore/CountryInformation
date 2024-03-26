using CountryInformation.Enums;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CountryInformation.Tests.Enums;

public class CountryTests
{
    [Fact]
    public void Country_List_should_contain_all_countries()
    {
        var list = Country.List;

        using (new AssertionScope())
        {
            list.Should().HaveCount(2);
            list.Should().Contain(Country.Sweden);
            list.Should().Contain(Country.Germany);
        }
    }
    
    [Fact]
    public void Sweden_should_contain_correct_information()
    {
        var country = Country.Sweden;

        using (new AssertionScope())
        {
            country.Name.Should().Be("Sweden");
            country.NativeName.Should().Be("Sverige");
            country.Value.Should().Be(Guid.Parse("CE661D9B-5B0E-4F03-BCC7-3F5E000B01B7"));
            country.Culture.Should().Be("sv-SE");
            country.CountryCode.Should().Be("+46");
        }
    }
    
    [Fact]
    public void Germany_should_contain_correct_information()
    {
        var country = Country.Germany;

        using (new AssertionScope())
        {
            country.Name.Should().Be("Germany");
            country.NativeName.Should().Be("Deutschland");
            country.Value.Should().Be(Guid.Parse("BE860349-FDFE-4AFD-82C9-A6C0AE421FC9"));
            country.Culture.Should().Be("de-DE");
            country.CountryCode.Should().Be("+49");
        }
    }
}