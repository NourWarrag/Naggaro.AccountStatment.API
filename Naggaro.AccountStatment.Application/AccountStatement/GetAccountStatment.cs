using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Naggaro.AccountStatment.Application.Common.Exceptions;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Domain.Constants;
using Naggaro.AccountStatment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Application.AccountStatement;

public class AccountStatmentDto
{
    public int AccountId { get; set; }
    public string AccountNumber { get; set; }
    public List<StatementDto> Statement { get; set; }
}

public class StatementDto
{
    public string Amount { get; set; }
    public string Date { get; set; }
}

public class GetAccountStatmentQueryValidator : AbstractValidator<GetAccountStatmentQuery>
{
    public GetAccountStatmentQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");

        RuleFor(x => x.FromAmount)
            .GreaterThanOrEqualTo(0).WithMessage("FromAmount at least greater than or equal to 0.");

        RuleFor(x => x.ToAmount)
            .GreaterThanOrEqualTo(1).WithMessage("ToAmount at least greater than or equal to 1.");

        RuleFor(x => x.FromAmount).NotNull().When(i => i.ToAmount is not null);

        RuleFor(x => x.FromDate).NotNull().When(i => i.ToDate is not null);

        RuleFor(x => x.ToAmount).NotNull().When(i => i.FromAmount is not null);

        RuleFor(x => x.ToDate).NotNull().When(i => i.FromDate is not null);

        RuleFor(x => x.ToDate).GreaterThan(x => x.FromDate);
        RuleFor(x => x.ToAmount).GreaterThan(x => x.FromAmount);

    }
}


public class GetAccountStatmentQuery : IRequest<AccountStatmentDto>
{
    public int AccountId { get; set; }
    public DateTime? FromDate  { get; set; }
    public DateTime? ToDate { get; set; }
    public decimal? FromAmount { get; set; }
    public decimal? ToAmount { get; set; }
}

public class GetAccountStatmentQueryHandler : IRequestHandler<GetAccountStatmentQuery, AccountStatmentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;
    private readonly IIdentityService _identityService;

    public GetAccountStatmentQueryHandler(IApplicationDbContext context, IUser currentUser, IIdentityService identityService)
    {
        _context = context;
        _currentUser = currentUser;
        _identityService = identityService;
    }

    public async Task<AccountStatmentDto> Handle(GetAccountStatmentQuery request, CancellationToken cancellationToken)
    {
        if( !await _identityService.IsInRoleAsync(_currentUser.Id,Roles.Admin) && AllParametersOtherThanAccountAreNull(request) )
        {
            throw new UnauthorizedAccessException();
        }
        var account = await _context.Accounts.FirstOrDefaultAsync(i => i.ID == request.AccountId, cancellationToken: cancellationToken);
        if (account is null)
        {
            throw new NotFoundException("Account Not Found");
        }
        var query =  _context.AccountStatments.Where(x => x.AccountID == request.AccountId);
        if (request.ToAmount is not null)
        {
            query = query.Where(i=> request.FromAmount <= i.Amount && i.Amount <= request.ToAmount);

        }
        if (request.ToDate is not null)
        {
            query = query.Where(i => request.FromDate <= i.DateField && i.DateField <= request.ToDate);
        }

        if(request.ToAmount is null && request.ToDate is null)
        {
            query = query.Where(i => i.DateField >= DateTime.Now.AddMonths(-3));
        }
        //no need to check from amount because the validation would have failed

        return new AccountStatmentDto
        {
            AccountId= account.ID,
            AccountNumber = HashAccountNumber(account.AccountNumber),
            Statement =await query.Select(i=> new StatementDto { Amount = i.Amount.ToString() , Date = i.DateField.ToString() }).ToListAsync(cancellationToken: cancellationToken)
        };
    }

    private static string HashAccountNumber(string accountNumber)
    {
        var stringBuilder = new StringBuilder();
       for(int i = 0; i< accountNumber.Length; i++)
        {
            if(i > accountNumber.Length - 3)
            {
                stringBuilder.Append(accountNumber[i]);
            }else
            {
                stringBuilder.Append('#');
            }
        }

        return stringBuilder.ToString();
    }

    static bool AllParametersOtherThanAccountAreNull(GetAccountStatmentQuery request)
    {
        if(request.FromDate is null &&
            request.ToDate is null &&
            request.ToAmount is null && 
            request.FromAmount is null) return true;
        return false;

    }
}