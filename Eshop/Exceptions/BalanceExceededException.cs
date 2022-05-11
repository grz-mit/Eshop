using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Exceptions
{
    public class BalanceExceededException : Exception
    {
        public BalanceExceededException() : base()
        {

        }
        public BalanceExceededException(string message) : base(message)
        {

        }
    }
}
