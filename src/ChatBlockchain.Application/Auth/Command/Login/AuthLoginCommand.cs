using ChatBlockchain.Application.Auth.Dtos;
using MediatR;

namespace ChatBlockchain.Application.Auth.Command.Login
{
    public class AuthLoginCommand() : IRequest<AuthLoginDto>
    {
        public required string Address { get; set; } = string.Empty;
        public required string Signature { get; set; } = string.Empty;
        public required string OriginalChallenge { get; set; } = string.Empty;
    }
}