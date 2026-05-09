using ChatBlockchain.Core.Interfaces.Services;

namespace ChatBlockchain.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public string? GetCurrentUserAddress()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("address")?.Value;
        }
    }
}