using FluentValidation;
using MediatR;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Application.Common.Exceptions;
using FluentValidation.Results;

namespace Naggaro.AccountStatment.Application.Authentication;

public class LogoutRequest : IRequest
{
  
}

 



public class LogoutRequestHandler : IRequestHandler<LogoutRequest>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IUser _CurrentUser;

    public LogoutRequestHandler(IApplicationDbContext context, IIdentityService identityService, IUser currentUser)
    {
        _context = context;
        _identityService = identityService;
        _CurrentUser = currentUser;
    }

    public async Task Handle(LogoutRequest request, CancellationToken cancellationToken)
    { 
        if (_CurrentUser.Id is  null) throw new UnauthorizedAccessException();

         await _identityService.SignOutAsync();
    }
}