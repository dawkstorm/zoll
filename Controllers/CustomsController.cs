using CustomsController.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomsController.Controllers;

/// <summary>
/// Controller for Customs
/// </summary>
[ApiController]
[Route("[controller]")]
public class CustomsController : ControllerBase
{
    private readonly ICustomsService _customService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="customsService">Reference to Customs Service class</param>
    public CustomsController(ICustomsService customsService)
    {
        _customService = customsService;
    }

    /// <summary>
    /// Check whether there are customs control between 2 countries or not
    /// </summary>
    /// <param name="country1code">First country's A2 Isocode</param>
    /// <param name="country2code">Second country's A2 Isocode</param>
    [HttpGet]
    [Route("get-customs-with-countries")]
    public async Task<CustomsResponse> GetCustomsWithCountries(string country1code, string country2code)
    {
        var customs = _customService.GetCustoms(country1code, country2code);
        return customs;
    }

    /// <summary>
    /// Check whether there are customs between countries and postal codes
    /// </summary>
    /// <param name="country1">Shipper country's isocode</param>
    /// <param name="p1">Shipper postalCode</param>
    /// <param name="country2">Receiver country's isocode</param>
    /// <param name="p2">Receiver postalCode</param>
    /// <param name="city1">Shipper's city</param>
    /// <param name="city2">Receiver's city</param>
    [HttpGet]
    [Route("get-customs-with-zipcodes")]
    public async Task<CustomsResponse> GetCustomsWithZipcodes(
        string country1,
        string country2,
        string p1,
        string p2,
        string city1 = "",
        string city2 = "")
    {
        return _customService.GetCustomsBetweenDistricts(country1, p1, country2, p2, city1, city2);
    }
}