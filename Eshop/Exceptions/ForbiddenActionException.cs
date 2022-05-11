using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Exceptions
{
    public class ForbiddenActionException : Exception
    {
        public ForbiddenActionException() : base()
        {

        }
        public ForbiddenActionException(string message) : base(message)
        {

        }
    }
}
