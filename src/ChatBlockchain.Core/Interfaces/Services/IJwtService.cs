namespace ChatBlockchain.Core.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId);
        bool ValidateToken(string token, out string address);
    }
}