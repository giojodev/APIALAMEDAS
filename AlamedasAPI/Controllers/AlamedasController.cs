using Microsoft.AspNetCore.Mvc;
using AlamedasAPI.Models;
using System;

namespace AlamedasAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class AlamedasController : ControllerBase
{
    private readonly ILogger<AlamedasController> _logger;

    public AlamedasController(ILogger<AlamedasController> logger)
    {
        _logger = logger;
    }

     [HttpGet(Name = "ProductList")]
     public async Task<ActionResult> Foo()
    {
        return BadRequest();
    }
}
