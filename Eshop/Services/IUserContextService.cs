using System.Security.Claims;

namespace Eshop.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        string UserId { get; }
    }
}