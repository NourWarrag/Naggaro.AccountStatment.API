using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Application.AccountStatement;
public class AccountStatmentDto
{
}

public class GetTodoItemsWithPaginationQueryValidator : AbstractValidator<GetAccountStatmentnQuery>
{
    public GetTodoItemsWithPaginationQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");

        RuleFor(x => x.FromAmount)
            .GreaterThanOrEqualTo(0).WithMessage("FromAmount at least greater than or equal to 0.");

        RuleFor(x => x.ToAmount)
            .GreaterThanOrEqualTo(1).WithMessage("ToAmount at least greater than or equal to 1.");
    }
}


public class GetAccountStatmentnQuery : IRequest<AccountStatmentDto>
{
    public int AccountId { get; set; }
    public DateTime FromDate  { get; set; }
    public DateTime ToDate { get; set; }
    public decimal FromAmount { get; set; }
    public decimal ToAmount { get; set; }
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetAccountStatmentnQuery, AccountStatmentDto>
{
    private readonly IApplicationDbContext _context;
   

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context )
    {
        _context = context;
       
    }

    public async Task<AccountStatmentDto> Handle(GetAccountStatmentnQuery request, CancellationToken cancellationToken)
    { var result = await _context.Accounts
            .Include(i => i.AccountStatments)
            .Where(x => x.ID == request.AccountId).FirstOrDefaultAsync();
        return new AccountStatmentDto();
    }
}