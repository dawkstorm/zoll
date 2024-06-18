using CustomsController.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomsController.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomsController : ControllerBase
{

    private readonly ICustomsService _customService;

    public CustomsController(ICustomsService customsService){
        _customService = customsService;
    }

    [HttpGet(Name = "GetCustoms")]
    public async Task<string> Get()
    {
        return _customService.Get();
    }


    [HttpPost(Name = "PostCustoms")]
    public async Task<bool> Post(string country)
    {
        return _customService.Post(country);
    }
}