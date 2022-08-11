using Microsoft.AspNetCore.Mvc;
using System;
using AlamedasAPI.Infraestructure.Alamedas;
using AlamedasAPI.Infraestructure.Alamedas.DTO;
using AlamedasAPI.Db.Models.Alamedas.Models;

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
    [HttpGet("Catalog/GetCondominumMontlyDeb")]
    public IActionResult GetCondominumMontlyDeb()
    {
        var response = _catalogservices.GetCondominumMontlyDeb(); 
        return Ok(response);
    }
    [HttpGet("Catalog/GetProductExpenseGrid")]
    public IActionResult GetProductExpenseGrid()
    {
        var response = _catalogservices.ProductExpenseGrid(); 
        return Ok(response);
    }
    [HttpGet("Catalog/GetGridDetailExpenses")]
    public IActionResult GetGridDetailExpenses(int consecutive)
    {
        var response = _catalogservices.GetGridDetailExpenses(consecutive); 
        return Ok(response);
    }
    [HttpGet("Catalog/GetGridDebtCondo")]
    public IActionResult GetGridDebtCondo(int idCondomino)
    {
        var response = _catalogservices.GetGridDebtCondo(idCondomino); 
        return Ok(response);
    }

    //CONSTRUIR GRID GCC
    [HttpGet("Catalog/ProdExpenseList")]
    public IActionResult GetListProdExpense()
    {
        var response = _catalogservices.GetListProdExpense(); 
        return Ok(response);
    }

    //CONSTRUIR GRID ICC
    [HttpGet("Catalog/ProdEntryList")]
    public IActionResult GetListProdEntry()
    {
        var response = _catalogservices.GetListProdEntry(); 
        return Ok(response);
    }

    //TOP VECINOS EN MORA
    [HttpGet("Catalog/TopDebtDashboard")]
    public IActionResult GetCondominiumDebtDashboard()
    {
        var response = _catalogservices.GetCondominiumDebtDashboard(); 
        return Ok(response);
    }

    //OBTENER DGCC
    [HttpGet("Catalog/DetGccList")]
    public IActionResult GetDetGCC(int IdConsecutive)
    {
        var response = _catalogservices.GetDetGCC(IdConsecutive); 
        return Ok(response);
    }

    //OBTENER DICC
    [HttpGet("Catalog/DetIccList")]
    public IActionResult GetDetICC(int IdConsecutive)
    {
        var response = _catalogservices.GetDetICC(IdConsecutive); 
        return Ok(response);
    }

    //OBTENER GCC
    [HttpGet("Catalog/GccList")]
    public IActionResult GetGCC(int IdConsecutive)
    {
        var response = _catalogservices.GetGCC(IdConsecutive); 
        return Ok(response);
    }

    //OBTENER ICC
    [HttpGet("Catalog/IccList")]
    public IActionResult GetICC(int IdConsecutive)
    {
        var response = _catalogservices.GetICC(IdConsecutive); 
        return Ok(response);
    }
    
    //BUSCAR TGCC
    [HttpGet("Catalog/TgccList")]
    public IActionResult GetTGCC(int IdTGCC)
    {
        var response = _catalogservices.GetTGCC(IdTGCC); 
        return Ok(response);
    }

    //SELECCIONAR TIPO INGRESOCAJA CHICA
    [HttpGet("Catalog/TiccList")]
    public IActionResult GetTICC(int IdTICC)
    {
        var response = _catalogservices.GetTICC(IdTICC); 
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
    [HttpDelete("Transactions/DeleteDetGCC")]
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

    //GRABAR DGCC
    [HttpPost("Transactions/InsertDetGCC")]
    public async Task<IActionResult> InsertDetGCC(DetalleGastoCajachica model)
    {
        var response = await _transactionservices.InsertDetGCC(model); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //ELIMINARDICC
    [HttpDelete("Transactions/DeleteDetICC")]
    public IActionResult DeleteDetICC(int IdConsecutive)
    {
        var response = _transactionservices.DeleteDetICC(IdConsecutive); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //GRABAR DGCC
    [HttpPost("Transactions/InsertDetICC")]
    public async Task<IActionResult> InsertDetICC(DetalleIngresoCajachica model)
    {
        var response = await _transactionservices.InsertDetICC(model); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    [HttpPut("Transactions/UpdateCondominium")]
    public async Task<IActionResult> UpdateCondominium(CondominoDTO condominoDTO )
    {
        var response = await _transactionservices.UpdateCondominium(condominoDTO); 
        return Ok(response);
    }
    [HttpPut("Transactions/UpdateDetailIncome")]
    public async Task<IActionResult> UpdateDetailIncome(DetailIncomeDTO detailIncomeDTO )
    {
        var response = await _transactionservices.UpdateDetailIncome(detailIncomeDTO); 
        return Ok(response);
    }

    //ELIMINARDGCC
    [HttpPut("Transactions/OverridGCC")]
    public async Task<IActionResult> OverridGCC(int IdConsecutive)
    {
        var response = await _transactionservices.OverridGCC(IdConsecutive); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //GRABAR GCC
    [HttpPost("Transactions/InsertGCC")]
    public async Task<IActionResult> InsertGCC(GastosCajaChica model)
    {
        var response = await _transactionservices.InsertGCC(model); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //ELIMINAR CC
    [HttpDelete("Transactions/DeleteICC")]
    public IActionResult DeleteICC(int IdConsecutive)
    {
        var response = _transactionservices.DeleteICC(IdConsecutive); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //ANULAR ICC
    [HttpPut("Transactions/OverridICC")]
    public async Task<IActionResult> OverridICC(int IdConsecutive)
    {
        var response = await _transactionservices.OverridICC(IdConsecutive); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }
    
    //INGRESAR ICC
    [HttpPost("Transactions/InsertICC")]
    public async Task<IActionResult> InsertICC(TblIngresosCajaChica model)
    {
        var response = await _transactionservices.InsertICC(model); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //ELIMINAR TGCC
    [HttpDelete("Transactions/DeleteTGCC")]
    public IActionResult DeleteTGCC(int IdTGCC)
    {
        var response = _transactionservices.DeleteTGCC(IdTGCC); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //GRABAR TGCC
    [HttpPost("Transactions/InsertTGCC")]
    public async Task<IActionResult> InsertTGCC(TblGastoCajaChica model)
    {
        var response = await _transactionservices.InsertTGCC(model); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //ELIMINAR TICC
    [HttpDelete("Transactions/DeleteTICC")]
    public IActionResult DeleteTICC(int IdTICC)
    {
        var response = _transactionservices.DeleteTICC(IdTICC); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //GRABAR TICC
    [HttpPost("Transactions/InsertTICC")]
    public async Task<IActionResult> InsertTICC(TipoIngresoCajaChica model)
    {
        var response = await _transactionservices.InsertTICC(model); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response.Message);
    }

    //*********** SECURITY ***********

    [HttpGet("Security/ValidateUser")]
    public async Task<IActionResult>  GetLogin(string Login)
    {
        var response = await _segurityservices.ValidateUser(Login); 
        if(response.Error)
            return BadRequest(response.Message);
        return Ok(response);
    }
}
