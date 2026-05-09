namespace ChatBlockchain.Application.Auth.Dtos
{
    public class AuthLoginDto
    {
        public required string Token { get; set; } = string.Empty;
        public required string Address { get; set; } = string.Empty;
    }
}