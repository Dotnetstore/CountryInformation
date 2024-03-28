using System.Text.RegularExpressions;
using CountryInformation.Exceptions;
using CountryInformation.Services.SocialSecurityNumbers;

namespace CountryInformation.Services.CorporateIds;

public class SwedishCorporateId
{
    private static readonly string[] FirmaTypes =
    {
        // String to map different company types.
        // Will only pick 0-9, but we use 10 to be EF as we want it constant.
        "Okänt", // 0
        "Dödsbon", // 1
        "Stat, landsting, kommun eller församling", // 2
        "Utländska företag som bedriver näringsverksamhet eller äger fastigheter i Sverige", // 3
        "Okänt", // 4
        "Aktiebolag", // 5
        "Enkelt bolag", // 6
        "Ekonomisk förening eller bostadsrättsförening", // 7
        "Ideella förening och stiftelse", // 8
        "Handelsbolag, kommanditbolag och enkelt bolag", // 9
        "Enskild firma", // 10
    };

    private static readonly Regex RegexPattern = new(@"^(\d{2}){0,1}(\d{2})(\d{2})(\d{2})([-+]?)?(\d{4})$");

    private static bool LuhnCheck(string value)
    {
        var t = value.ToCharArray().Select(d => d - 48).ToArray();
        var sum = 0;
        
        for (var i = 0; i < t.Length; i++)
        {
            var temp = t[i];
            temp *= 2 - (i % 2);
            if (temp > 9)
            {
                temp -= 9;
            }

            sum += temp;
        }

        return sum % 10 == 0;
    }

    public static SwedishCorporateId Parse(string number) => new(number);

    public static bool TryParse(string number, out SwedishCorporateId? result)
    {
        try
        {
            result = new SwedishCorporateId(number);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    public static bool Valid(string number)
    {
        try
        {
            Parse(number);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private string? _number;
    private SwedishSocialSecurityNumber? _swedishSocialSecurityNumber;

    public string VatNumber => $"SE{ShortString}01";

    public bool IsSwedishSocialSecurityNumber => _swedishSocialSecurityNumber != null;

    public SwedishSocialSecurityNumber? SwedishSocialSecurityNumber => _swedishSocialSecurityNumber;

    public string Type =>
        IsSwedishSocialSecurityNumber
            ? FirmaTypes[10]
            : FirmaTypes[int.Parse(_number![..1])];

    public SwedishCorporateId(string number)
    {
        InnerParse(number);
    }

    public string Format(bool separator = true)
    {
        if (IsSwedishSocialSecurityNumber)
        {
            return _swedishSocialSecurityNumber!.Format(false, !separator);
        }

        var num = ShortString;
        return separator ? $"{num[..6]}-{num[6..]}" : num;
    }

    private string ShortString => (IsSwedishSocialSecurityNumber ? _swedishSocialSecurityNumber!.Format() : _number)!
        .Replace("-", "")
        .Replace("+", "");

    private void InnerParse(string input)
    {
        var originalInput = input;
        if (input.Length is > 13 or < 10)
        {
            var state = input.Length > 13 ? "long" : "short";
            throw new SwedishCorporateIdException($"Input value too ${state}");
        }

        try
        {
            var matches = RegexPattern.Matches(input);
            if (matches.Count < 1 || matches[0].Groups.Count < 7)
            {
                throw new SwedishCorporateIdException();
            }

            input = input.Replace("-", "")
                .Replace("+", "");
            // Get regexp match
            //var matches = regex.Matches(input);
            var groups = matches[0].Groups;

            // if [1] is set, it may only be prefixed with 16.
            if (!string.IsNullOrEmpty(groups[1].Value))
            {
                if (int.Parse(groups[1].Value) != 16)
                {
                    throw new SwedishCorporateIdException();
                }

                input = input[2..];
            }

            // Third digit must be more than 20.
            // Second digit must be more than 10.
            if (int.Parse(groups[3].Value) < 20 ||
                int.Parse(groups[2].Value) < 10 ||
                !LuhnCheck(input))
            {
                throw new SwedishCorporateIdException();
            }

            _number = input;
        }
        catch (Exception ex)
        {
            try
            {
                _swedishSocialSecurityNumber = SwedishSocialSecurityNumber.Parse(originalInput);
            }
            catch
            {
                throw ex;
            }
        }
    }
}