﻿using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.ViewModels
{
    public class BuyViewModel
    {
        public PostModel Post { get; set; }
        public SoldPostModel SoldPost { get; set; }
        public decimal BuyerWallet { get; set; }
        public decimal WalletAfterBuy { get; set; }
    }
}
