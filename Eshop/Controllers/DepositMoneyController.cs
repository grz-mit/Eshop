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
    public class DepositMoneyController : Controller
    {
        private readonly IUserService _userService;

        public DepositMoneyController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddMoney(decimal money)
        {
            await _userService.DepositMoney(money);

            return RedirectToAction(nameof(Index));
        }
    }
}
