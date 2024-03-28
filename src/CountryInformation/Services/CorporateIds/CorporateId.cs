using CountryInformation.Exceptions;

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

        if (GBCorporateId.Valid(id))
        {
            return new CorporateId(id);
        }

        throw new CorporateIdException("Invalid corporate id");
    }
}