using CustomsController.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomsController.Controllers;

/// <summary>
/// Controller for Countries
/// </summary>
[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICustomsService _customService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="customsService">Reference to Customs Service class</param>
    public CountryController(ICustomsService customsService)
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
        if (country == default)
        {
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
        if (country == null)
        {
            return NotFound("Country wasn't found in the list");
        }
        return Ok(country);
    }

    /// <summary>
    /// Change boolean isEUCU
    /// </summary>
    /// <param name="A2Code">Country's A2 Isocode</param>
    [HttpPut]
    [Route("change-country-eucu")]
    public async Task<string> ChangeCountryEUCU(string A2Code, bool val)
    {
        return _customService.ChangeEUCU(A2Code, val);
    }

    /// <summary>
    /// Check whether country is in EUCU or not
    /// </summary>
    /// <param name="A2Code">Country's A2 Isocode</param>
    [HttpGet]
    [Route("get-country-eucu")]
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

    [HttpGet]
    [Route("get-country-information")]
    public async Task<IActionResult> GetCountryInformation(string A2Code)
    {
        var country = _customService.GetCountry(A2Code);
        if (country == default) return NotFound("Country wasn't found in the list");
        return Ok(country);
    }
}