﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
               
        }
        public NotFoundException(string message): base(message)
        {

        }
    }
}
