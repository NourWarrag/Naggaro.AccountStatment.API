using FluentValidation;
using MediatR;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Application.Common.Exceptions;
using FluentValidation.Results;

namespace Naggaro.AccountStatment.Application.Authentication;

public class AuthenticationRequest : IRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("AccountId is required.");


    }
}




public class AuthenticationRequesHandler : IRequestHandler<AuthenticationRequest>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IUser _CurrentUser;

    public AuthenticationRequesHandler(IApplicationDbContext context, IIdentityService identityService, IUser currentUser)
    {
        _context = context;
        _identityService = identityService;
        _CurrentUser = currentUser;
    }

    public async Task Handle(AuthenticationRequest request, CancellationToken cancellationToken)
    { 
        if (_CurrentUser.Id is not null) throw new UserAlreadyLogedInException($"{_CurrentUser.Name} is signed in please sign out");
        var result = await _identityService.SignInAsync(request.UserName, request.Password);
        if (!result) throw new ValidationException("Wrong password or userName",new List<ValidationFailure>{ new ValidationFailure("password or userName", "Wrong password or userName") });
    }
}