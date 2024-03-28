using System.Text;
using System.Text.RegularExpressions;
using CountryInformation.Exceptions;

namespace CountryInformation.Services.CorporateIds;

public static class GermanCorporateId
{
    public static bool Valid(string? corporateId)
    {
        if (string.IsNullOrWhiteSpace(corporateId))
            return true;
        
        corporateId = RemoveSpecialCharacters(corporateId);
        corporateId = corporateId.Replace("DE", string.Empty).Replace("de", string.Empty);
        
        if (!Regex.IsMatch(corporateId, @"^[1-9]\d{8}$"))
        {
            return false;
        }
        
        var product = 10;
        
        for (var index = 0; index < 8; index++)
        {
            var sum = (int.Parse(corporateId[index].ToString()) + product) % 10;
            if (sum == 0)
            {
                sum = 10;
            }

            product = 2 * sum % 11;
        }
        
        var val = 11 - product;
        var checkDigit = val == 10
            ? 0
            : val;
        
        return checkDigit == int.Parse(corporateId[8].ToString());
    }
    
    private static string RemoveSpecialCharacters(string value)
    {
        var sb = new StringBuilder();

        for (var i = 0; i < value?.Length; i++)
        {
            if (char.IsLetterOrDigit(value[i]))
            {
                sb.Append(value[i]);
            }
        }
        return sb.ToString();
    }
}