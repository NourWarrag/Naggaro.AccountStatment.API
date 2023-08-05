using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Application.Common.Exceptions;
public class UserAlreadyLogedInException : Exception
{
    public UserAlreadyLogedInException(string message) : base(message) { }
}
