using Eshop.Data;
using Eshop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Eshop.Controllers
{
    public class OffersEndedController : Controller
    {
        private readonly IOffersEndedService _offersEndedService;

        public OffersEndedController(IOffersEndedService offersEndedService)
        {
            _offersEndedService = offersEndedService;
        }

        public async Task<IActionResult> BoughtItems()
        {
            var boughtItems = await _offersEndedService.GetBoughtItemsByUserId();

            return View(boughtItems);
        }

        public async Task<IActionResult> SoldItems()
        {
            var soldItems = await _offersEndedService.GetSoldItemsByUserId();

            return View(soldItems);
        }
    }
}
