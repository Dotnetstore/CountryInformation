namespace CountryInformation.Exceptions;

public class SwedishCorporateIdException(
    string message = "Invalid Swedish organization number",
    Exception? inner = null)
    : Exception(message: message, innerException: inner);