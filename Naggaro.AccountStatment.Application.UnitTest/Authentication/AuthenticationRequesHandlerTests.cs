using FluentValidation;
using Moq;
using Naggaro.AccountStatment.Application.Authentication;
using Naggaro.AccountStatment.Application.Common.Exceptions;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Application.UnitTest.Authentication;
public class AuthenticationRequesHandlerTests
{
	private readonly AuthenticationRequesHandler _sut;
    private readonly Mock<IUser> _currentUser = new();
    private readonly Mock<IIdentityService> _identityService = new();

    public AuthenticationRequesHandlerTests()
	{
		_sut = new ( _identityService.Object,_currentUser.Object);

    }
    [Fact]
   public async Task AuthenticationRequesHandler_ShouldThrowUserAlreadyLogedInException_IfUserIsAlreadyLogedIn()
    {
        var userId = Guid.NewGuid().ToString();
        _currentUser.Setup(
           x => x.Id).Returns(userId);

        await Assert.ThrowsAsync<UserAlreadyLogedInException>(async () =>await _sut.Handle(new AuthenticationRequest(), It.IsAny<CancellationToken>()));


    }

    [Fact]
    public async Task AuthenticationRequesHandler_ShouldThrowValidationException_IfUserDataIsWrong()
    {
        var userId = Guid.NewGuid().ToString();
        _identityService.Setup(
            x => x.SignInAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        await Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(new AuthenticationRequest(), It.IsAny<CancellationToken>()));


    }
}
