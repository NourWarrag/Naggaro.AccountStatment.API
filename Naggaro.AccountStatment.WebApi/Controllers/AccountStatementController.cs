
using Microsoft.AspNetCore.Mvc;
using Naggaro.AccountStatment.Application.AccountStatement;

namespace Naggaro.AccountStatment.WebApi.Controllers;

public class AccountStatementController : ApiControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GerAccountStatement([FromQuery] GetAccountStatmentQuery getAccountStatmentQuery)
    {
        var result = await Mediator.Send(getAccountStatmentQuery);
        return Ok(result);
    }
}
