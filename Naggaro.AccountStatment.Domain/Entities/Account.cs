using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Domain.Entities;

public class Account
{
    public int ID { get; set; }
    public string AccountType { get; set; }
    public string AccountNumber { get; set; }
    public IList<AccountStatement> AccountStatments { get; set; } = new List<AccountStatement>();
}
