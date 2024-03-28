using CountryInformation.Exceptions;
using CountryValidation;

namespace CountryInformation.Services.CorporateIds;

public record struct CorporateId
{
    public string? Id { get; }

    private CorporateId(string? id)
    {
        Id = id;
    }

    public static CorporateId? Create(string? id)
    {
        CorporateId? corporateId = null;
        
        if (string.IsNullOrWhiteSpace(id))
            return corporateId;

        if (SwedishCorporateId.Valid(id))
        {
            var swedishCorporateId = new SwedishCorporateId(id);
            return new CorporateId(swedishCorporateId.VatNumber);
        }

        var validator = new CountryValidator();

        var result = validator.ValidateVAT(id, Country.GB);
        if (result.IsValid)
        {
            return new CorporateId(id);
        }

        result = validator.ValidateVAT(id, Country.DE);
        if(result.IsValid)
        {
            return new CorporateId(id);
        }

        result = validator.ValidateVAT(id, Country.NL);
        if(result.IsValid)
        {
            return new CorporateId(id);
        }

        result = validator.ValidateVAT(id, Country.DK);
        if(result.IsValid)
        {
            return new CorporateId(id);
        }

        throw new CorporateIdException("Invalid corporate id");
    }
}