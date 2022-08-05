using Microsoft.AspNetCore.Mvc;
using System;
using AlamedasAPI.Infraestructure.Alamedas;

namespace AlamedasAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class AlamedasController : ControllerBase
{
    private readonly ILogger<AlamedasController> _logger;
    private readonly ICatalogServices _catalogservices; 
    private readonly ISecurityServices _segurityservices;
    private readonly ITransactionServices _transactionservices;

    public AlamedasController(ILogger<AlamedasController> logger, ICatalogServices catalogservices,ISecurityServices segurityservices,ITransactionServices transactionservices)
    {
        _logger = logger;
        _catalogservices = catalogservices;
        _segurityservices = segurityservices;
        _transactionservices = transactionservices;
    }

    //catalogos
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
    //transacciones
    [HttpGet("Transactions/Test")]
    public async Task<IActionResult> GetTransaction()
    {
        var response = await _transactionservices.ProdExpense_PerryCash(); 
        return Ok(response);
    }

    //seguridad
    [HttpGet("Security/Test")]
    public async Task<IActionResult> GetLogin()
    {
        var result = await _segurityservices.Login(); 
        return Ok(result);
    }
}
