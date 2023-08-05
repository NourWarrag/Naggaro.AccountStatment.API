using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Domain.Entities;
public class AccountStatement
{
    public int ID { get; set; }
    public int AccountID { get; set; }
    public string DateField { get; set; }
    public decimal Amount { get; set; }
}
