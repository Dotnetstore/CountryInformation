using CountryInformation.Services;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CountryInformation.Tests.Services;

public class EnumerationTests
{
    [Fact]
    public void A_should_contain_correct_values()
    {
        var a = TestClassForEnumeration.A;

        using (new AssertionScope())
        {
            a.Name.Should().Be("TestA");
            a.Value.Should().Be(Guid.Parse("1DDA7310-E088-4193-B1DA-9A0E110D7191"));
        }
    }
    
    [Fact]
    public void B_should_contain_correct_values()
    {
        var b = TestClassForEnumeration.B;

        using (new AssertionScope())
        {
            b.Name.Should().Be("TestB");
            b.Value.Should().Be(Guid.Parse("D214C20A-690B-4A5F-ACA3-F7184D1A2A04"));
        }
    }

    [Fact]
    public void FromName_should_return_correct_object()
    {
        var result = TestClassForEnumeration.FromName("TestB");

        result?.Should().Be(TestClassForEnumeration.B);
    }

    [Fact]
    public void FromValue_should_return_correct_object()
    {
        var result = TestClassForEnumeration.FromValue(Guid.Parse("1DDA7310-E088-4193-B1DA-9A0E110D7191"));

        result?.Should().Be(TestClassForEnumeration.A);
    }

    [Fact]
    public void GetHashCode_should_not_be_min_value()
    {
        var result = TestClassForEnumeration.A.GetHashCode();

        result.Should().BeGreaterThan(int.MinValue);
    }

    [Fact]
    public void Equals_should_return_false_if_null()
    {
        var result = TestClassForEnumeration.A.Equals(null);

        result.Should().BeFalse();
    }
}

internal class TestClassForEnumeration(Guid value, string name) : Enumeration<TestClassForEnumeration>(value, name)
{
    public static readonly TestClassForEnumeration A = new(
        Guid.Parse("1DDA7310-E088-4193-B1DA-9A0E110D7191"), "TestA");
    
    public static readonly TestClassForEnumeration B = new(
        Guid.Parse("D214C20A-690B-4A5F-ACA3-F7184D1A2A04"), "TestB");
}