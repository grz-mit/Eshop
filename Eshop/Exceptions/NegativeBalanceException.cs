using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Exceptions
{
    public class NegativeBalanceException : Exception
    {
        public NegativeBalanceException()
        {

        }
        public NegativeBalanceException(string message) : base(message)
        {

        }
    }
}
