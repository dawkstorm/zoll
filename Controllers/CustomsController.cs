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
    /// Add a new country to the database
    /// </summary>
    /// <param name="A2Code">Country's A2 Isocode</param>
    /// <param name="isEUCU">Is country a part of the EUCU</param>
    [HttpPost]
    [Route("add-new-country")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddNewCountry(string A2Code, bool isEUCU)
    {
        var country = _customService.AddNewCountry(A2Code, isEUCU);
        if(country == default){
            return Ok("Country is already in the database");
        }
        return Ok(country);
    }

    /// <summary>
    /// Remove country from database
    /// </summary>
    /// <param name="A2Code">Country's A2 Isocode</param>
    [HttpDelete]
    [Route("remove-country")]
    public async Task<IActionResult> RemoveCountry(string A2Code)
    {
        var country = _customService.RemoveCountry(A2Code);
        if(country == null){
            return NotFound("Country wasn't found in the list");
        }
        return Ok(country);
    }

    /// <summary>
    /// Change boolean isEUCU
    /// </summary>
    /// <param name="A2Code">Country's A2 Isocode</param>
    [HttpPut]
    [Route("change-eucu")]
    public async Task<string> ChangeEUCU(string A2Code, bool val)
    {
        return _customService.ChangeEUCU(A2Code, val);
    }

    /// <summary>
    /// Check whether country is in EUCU or not
    /// </summary>
    /// <param name="A2Code">Country's A2 Isocode</param>
    [HttpGet]
    [Route("get-eucu")]
    public async Task<string> GetCountryEUCU(string A2Code)
    {
        return _customService.GetCountryEUCU(A2Code);
    }

    /// <summary>
    /// Returns all countries
    /// </summary>
    [HttpGet]
    [Route("get-all-countries")]
    public async Task<List<Country>> GetAllCountries()
    {
        return _customService.GetAllCountries();
    }

    /// <summary>
    /// Check whether there are customs control between 2 countries or not
    /// </summary>
    /// <param name="country1code">First country's A2 Isocode</param>
    /// <param name="country2code">Second country's A2 Isocode</param>
    [HttpGet]
    [Route("get-customs")]
    public async Task<bool> GetCustoms(string country1code, string country2code)
    {
        return _customService.GetCustoms(country1code, country2code);
    }
    
    /// <summary>
    /// Check whether there are customs between countries and postal codes
    /// </summary>
    /// <param name="c1">First country's isocode</param>
    /// <param name="p1">First postalCode</param>
    /// <param name="c2">Second country's isocode</param>
    /// <param name="p2">Second postalCode</param>
    [HttpGet]
    [Route("get-customs-between-districts")]
    public async Task<bool> GetCustomsBetweenDistricts(string c1, string p1, string c2, string p2)
    {
        return _customService.GetCustomsBetweenDistricts(c1, p1, c2, p2);
    }
}