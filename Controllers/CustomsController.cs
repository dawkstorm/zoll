using CustomsController.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CustomsController.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomsController : ControllerBase
{

    private readonly ICustomsService _customService;

    public CustomsController(ICustomsService customsService){
        _customService = customsService;
    }

    [HttpPost]
    [Route("AddNewCountry")]
    public async Task<Country> AddNewCountry(string A2Code, bool isEUCU)
    {
        return _customService.AddNewCountry(A2Code, isEUCU);
    }

    [HttpDelete]
    [Route("RemoveCountry")]
    public async Task<Country> RemoveCountry(string A2Code)
    {
        return _customService.RemoveCountry(A2Code);
    }
    [HttpPut]
    [Route("ChangeEUCU")]
    public async Task<string> ChangeEUCU(string A2Code, bool val)
    {
        return _customService.ChangeEUCU(A2Code, val);
    }
    [HttpGet]
    [Route("GetEUCU")]
    public async Task<string> GetCountryEUCU(string A2Code)
    {
        return _customService.GetCountryEUCU(A2Code);
    }
    [HttpGet]
    [Route("GetAllCountries")]
    public async Task<List<Country>> GetAllCountries(){
        return _customService.GetAllCountries();
    }
    [HttpGet]
    [Route("GetCustoms")]
    public async Task<bool> GetCustoms(string country1code, string country2code){
        return _customService.GetCustoms(country1code, country2code);
    }
}
