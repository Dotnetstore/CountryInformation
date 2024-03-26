using CountryInformation.Services;

namespace CountryInformation.Enums;

public sealed class Country : Enumeration<Country>
{
    public static List<Country> List = new();
    
    public static readonly Country Sweden = new(
        Guid.Parse("CE661D9B-5B0E-4F03-BCC7-3F5E000B01B7"),
        "Sweden",
        "Sverige",
        "sv-SE",
        "+46");
    
    public static readonly Country Germany = new(
        Guid.Parse("BE860349-FDFE-4AFD-82C9-A6C0AE421FC9"),
        "Germany",
        "Deutschland",
        "de-DE",
        "+49");
    
    public string NativeName { get; }
    
    public string Culture { get; }
    
    public string CountryCode { get; }

    private Country(
        Guid value, 
        string name, 
        string nativeName,
        string culture,
        string countryCode) : base(value, name)
    {
        NativeName = nativeName;
        Culture = culture;
        CountryCode = countryCode;
        
        List.Add(this);
    }
}