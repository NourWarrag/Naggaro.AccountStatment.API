using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Naggaro.AccountStatment.Application.AccountStatement;
using Naggaro.AccountStatment.Application.Common.Exceptions;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Domain.Constants;
using Naggaro.AccountStatment.Domain.Entities;

namespace Naggaro.AccountStatment.Application.UnitTest.AccountStatement;
public class GetAccountStatmentQueryHandlerTest
{
    private readonly GetAccountStatmentQueryHandler _sut;
    private readonly Mock<IApplicationDbContext> _context = new();
    private readonly Mock<IUser> _currentUser = new();
    private readonly Mock<IIdentityService> _identityService = new();

    public GetAccountStatmentQueryHandlerTest()
    {
        _sut = new GetAccountStatmentQueryHandler(_context.Object, _currentUser.Object, _identityService.Object);
    }
    [Fact]
    public async Task GetAccountStatmentQueryHandler_ShouldReturnStatement_WhenAdmin()
    {
        //Arange
        var userId = Guid.NewGuid().ToString();
        var accountId = It.IsAny<int>();
        var account = new Account { ID = accountId };

        _context.Setup(x => x.Accounts)
            .ReturnsDbSet(FackeData.FakeAccounts());

        _context.Setup(x => x.AccountStatments)
            .ReturnsDbSet(FackeData.FakeAccounts().SelectMany(i=>i.AccountStatments));
        _currentUser.Setup(
            x => x.Id).Returns(userId);

        _identityService.Setup(
           x => x.IsInRoleAsync(userId, Roles.Admin)).ReturnsAsync(true);
        //Act
        var statement = await _sut.Handle(new GetAccountStatmentQuery { AccountId = accountId }, CancellationToken.None);

        //Assert
        Assert.IsType<AccountStatmentDto>(statement);
    }

    [Fact]
    public async Task GetAccountStatmentQueryHandler_ShouldShouldThrowNotFountException_WhenAccountNotExist()
    {
        //Arange
        var userId = Guid.NewGuid().ToString();
        var accountId = It.IsAny<int>();
        var account = new Account { ID = accountId };

        _context.Setup(x => x.Accounts)
            .ReturnsDbSet(Enumerable.Empty<Account>());

        _context.Setup(x => x.AccountStatments)
            .ReturnsDbSet(FackeData.FakeAccounts().SelectMany(i => i.AccountStatments));
        _currentUser.Setup(
            x => x.Id).Returns(userId);

        _identityService.Setup(
           x => x.IsInRoleAsync(userId, Roles.Admin)).ReturnsAsync(true);
        
        
        await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.Handle(new GetAccountStatmentQuery { AccountId = accountId }, CancellationToken.None));

    }
    [Fact]
    public async Task GetAccountStatmentQueryHandler_ShouldShouldUnAuthorizedException_WhenOnlyAccountIdAndUSer()
    {
        //Arange
        var userId = Guid.NewGuid().ToString();
        var accountId = It.IsAny<int>();
        var account = new Account { ID = accountId };

        _context.Setup(x => x.Accounts)
            .ReturnsDbSet(FackeData.FakeAccounts());

        _context.Setup(x => x.AccountStatments)
            .ReturnsDbSet(FackeData.FakeAccounts().SelectMany(i => i.AccountStatments));
        _currentUser.Setup(
            x => x.Id).Returns(userId);

        _identityService.Setup(
           x => x.IsInRoleAsync(userId, Roles.User)).ReturnsAsync(true);


        await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _sut.Handle(new GetAccountStatmentQuery {AccountId = accountId }, CancellationToken.None));

    }
}
