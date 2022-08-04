using Microsoft.AspNetCore.Mvc;
using System;
using AlamedasAPI.Infraestructure.Services.Alamedas.Services;

namespace AlamedasAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class AlamedasController : ControllerBase
{
    private readonly ILogger<AlamedasController> _logger;
    private readonly ICatalogServices _catalogservices; 

    public AlamedasController(ILogger<AlamedasController> logger, ICatalogServices catalogservices)
    {
        _logger = logger;
        _catalogservices = catalogservices;
    }


    [HttpGet("Catalog/UserList")]
    public IActionResult GetListUsers()
    {
        var response = _catalogservices.GetListUsers(); 
        return Ok(response);
    }

    [HttpGet("Catalog/CondominiumList")]
    public IActionResult GetListCondomino()
    {
        var response = _catalogservices.GetListCondomino(); 
        return Ok(response);
    }
}
