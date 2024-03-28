namespace CountryInformation.Services.CorporateIds;

public class GBCorporateId
{
    public static bool Valid(string? corporateId)
    {
        if (string.IsNullOrWhiteSpace(corporateId))
            return true;
        
        corporateId = corporateId.Replace(" ", "").ToUpperInvariant();
        
        if (corporateId.Length == 11)
        {
            if (corporateId[0] == 'G' && corporateId[1] == 'B')
            {
                corporateId = corporateId[2..];
            }
            else
            {
                return false;
            }
        }

        if (corporateId.Length != 9)
        {
            return false;
        }

        var runningTotal = 0;
        var multipliersByIndex = new[] {8, 7, 6, 5, 4, 3, 2};
        
        for (var i = 0; i < 7; i++)
        {
            if (!int.TryParse(corporateId[i].ToString(), out var currentDigitValue))
            {
                return false;
            }
            
            runningTotal += currentDigitValue * multipliersByIndex[i];
        }

        while (runningTotal >= 0)
        {
            runningTotal -= 97;
        }

        var checkSum = (runningTotal * -1).ToString("00");

        return checkSum[0] == corporateId[7] && 
               checkSum[1] == corporateId[8];
    }
}