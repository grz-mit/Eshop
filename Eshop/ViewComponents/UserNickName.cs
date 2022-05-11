using Eshop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eshop.ViewComponents
{
    public class UserNickName : ViewComponent
    {
        private readonly EshopDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserNickName(EshopDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return View("UserNickName", result);
        }
    }
}
