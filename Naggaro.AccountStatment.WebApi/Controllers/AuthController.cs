using Microsoft.AspNetCore.Mvc;
using Naggaro.AccountStatment.Application.Authentication;

namespace Naggaro.AccountStatment.WebApi.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        await Mediator.Send(authenticationRequest);
        return Ok();
    }
}
