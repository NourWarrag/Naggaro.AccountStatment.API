using FluentValidation;
using MediatR;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Application.Common.Exceptions;


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




public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<AuthenticationRequest>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task Handle(AuthenticationRequest request, CancellationToken cancellationToken)
    {
        var result = await _identityService.SignInAsync(request.UserName, request.Password);
        if (!result) throw new ValidationException("Wrong password or userName");
    }
}