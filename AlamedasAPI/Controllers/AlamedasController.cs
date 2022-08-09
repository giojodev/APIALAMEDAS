using Microsoft.AspNetCore.Mvc;
using System;
using AlamedasAPI.Infraestructure.Alamedas;
using AlamedasAPI.Infraestructure.Alamedas.DTO;

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
    [HttpGet("Catalog/GetDashboardDebt")]
    public IActionResult GetDashboardDebt()
    {
        var response = _catalogservices.GetDashboardDebt(); 
        return Ok(response);
    }
    [HttpGet("Catalog/GetDashboardBill")]
    public IActionResult GetDashboardBill()
    {
        var response = _catalogservices.GetDashboardBill(); 
        return Ok(response);
    }
    [HttpGet("Catalog/GetIncomesDashboard")]
    public IActionResult GetIncomesDashboard()
    {
        var response = _catalogservices.GetIncomesDashboard(); 
        return Ok(response);
    }

    //CONSTRUIR GRID P_GCC
    [HttpGet("Catalog/ProdExpenseList")]
    public IActionResult GetListProdExpense()
    {
        var response = _catalogservices.GetListProdExpense(); 
        return Ok(response);
    }
    [HttpPost("Transactions/ActualizarCondomino")]
    public async Task<IActionResult> UpdateCondominium(CondominoDTO condominoDTO )
    {
        var response = await _transactionservices.UpdateCondominium(condominoDTO); 
        return Ok(response);
    }
    [HttpPost("Transactions/ActualizarDetalleIngreso")]
    public async Task<IActionResult> UpdateDetailIncome(DetailIncomeDTO detailIncomeDTO )
    {
        var response = await _transactionservices.UpdateDetailIncome(detailIncomeDTO); 
        return Ok(response);
    }


    //CONSTRUIR GRID P_ICC
    [HttpGet("Catalog/ProdEntryList")]
    public IActionResult GetListProdEntry()
    {
        var response = _catalogservices.GetListProdEntry(); 
        return Ok(response);
    }

    //*********** TRANSACTIONS ***********

    //OBTENER ULTIMO CONSECUTIVO ICC
    [HttpGet("Transactions/ConsecutiveICC")]
    public IActionResult GetConsecutiveICC()
    {
        var response = _transactionservices.GetConsecutiveICC(); 
        if(response == -1)
            return BadRequest(response);
        return Ok(response);
    }

    //OBTENER ULTIMO CONSECUTIVO GCC
    [HttpGet("Transactions/ConsecutiveGCC")]
    public IActionResult GetConsecutiveGCC()
    {
        var response = _transactionservices.GetConsecutiveGCC(); 
        if(response == -1)
            return BadRequest(response);
        return Ok(response);
    }

    //ELIMINARDGCC
    [HttpPut("Transactions/DeleteDetGCC")]
    public IActionResult DeleteDetGCC(int IdConsecutive)
    {
        var response = _transactionservices.DeleteDetGCC(IdConsecutive); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }
    [HttpPut("Transactions/UpdateDayDebt")]
    public async Task<IActionResult> UpdateDayDebt()
    {
        var response = await _transactionservices.UpdateDayDebt(); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }
    [HttpPut("Transactions/UpdateBill")]
    public async Task<IActionResult> UpdateBill(BillsDTO billsDTO)
    {
        var response = await _transactionservices.UpdateBills(billsDTO); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }
    [HttpPut("Transactions/UpdateIncomes")]
    public async Task<IActionResult> UpdateIncomes(IncomesDTO incomesDTO)
    {
        var response = await _transactionservices.UpdateIncomes(incomesDTO); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }
    [HttpPut("Transactions/UpdateDebt")]
    public async Task<IActionResult> UpdateDebt(DebtDTO debtDTO)
    {
        var response = await _transactionservices.UpdateDebt(debtDTO); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }
    [HttpPut("Transactions/UpdateIncomeType")]
    public async Task<IActionResult> UpdateIncomeType(IncomeTypeDTO incomeTypeDTO)
    {
        var response = await _transactionservices.UpdateIncomeType(incomeTypeDTO); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //*********** SECURITY ***********

    [HttpGet("Security/Test")]
    public async Task<IActionResult> GetLogin()
    {
        var result = await _segurityservices.Login(); 
        return Ok(result);
    }
}
