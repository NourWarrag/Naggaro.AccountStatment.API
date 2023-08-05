using Microsoft.AspNetCore.Mvc;
using Naggaro.AccountStatment.Application.AccountStatement;
using Naggaro.AccountStatment.Application.Common.Security;

namespace Naggaro.AccountStatment.WebApi.Controllers;

[Authorize]
public class AccountStatementConntroller: ApiControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GerAccountStatement([FromQuery] GetAccountStatmentQuery getAccountStatmentQuery)
    {
        var result = await Mediator.Send(getAccountStatmentQuery);
        return Ok(result);
    }
}
