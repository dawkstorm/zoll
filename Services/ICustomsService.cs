namespace CustomsController.Services;

/// <summary>
/// Service for Customs
/// </summary>
public interface ICustomsService
{
    /// <summary>
    /// Add a new country to the database
    /// </summary>
    /// <param name="A2Code">Country's A2 isocode</param>
    /// <param name="isEUCU">is counntry a part of EUCU</param>
    public Country AddCountry(string A2Code, bool isEUCU);

    /// <summary>
    /// Remove country from the database
    /// </summary>
    /// <param name="A2Code">Country's A2 isocode</param>
    public Country RemoveCountry(string A2Code);

    /// <summary>
    /// Change boolean isEUCu
    /// </summary>
    /// <param name="A2Code">Country's A2 isocode</param>
    public string ChangeEUCU(string A2Code, bool val);

    /// <summary>
    /// Check whether country is in EUCU or not
    /// </summary>
    /// <param name="A2Code">Country's A2 isocode</param>
    /// <returns></returns>
    public string GetCountryEUCU(string A2Code);

    /// <summary>
    /// Return all countries
    /// </summary>
    public List<Country> GetAllCountries();

    /// <summary>
    /// Check whether there are customs between 2 countries
    /// </summary>
    /// <param name="country1code">Shipper country's isocode</param>
    /// <param name="country2code">Receiver country's isocode</param>
    /// <returns></returns>
    public CustomsResponse GetCustoms(string country1code, string country2code);

    /// <summary>
    /// Check whether there are customs between 2 postalCode
    /// </summary>
    /// <param name="shipperC1">Shipper country</param>
    /// <param name="shipperP1">Shipper postalCode</param>
    /// <param name="receiverC2">Receiver country</param>
    /// <param name="receiverP2">Receiver postalCode</param>
    /// <param name="city1">Shipper city</param>
    /// <param name="city2">Receiver city</param>
    public CustomsResponse GetCustomsBetweenDistricts(
        string shipperC1,
        string shipperP1,
        string receiverC2,
        string receiverP2,
        string city1 = "",
        string city2 = "");

    /// <summary>
    /// Get info about the country
    /// </summary>
    /// <param name="A2Code">Country's A2 Isocode</param>
    public Country? GetCountry(string A2Code);
}