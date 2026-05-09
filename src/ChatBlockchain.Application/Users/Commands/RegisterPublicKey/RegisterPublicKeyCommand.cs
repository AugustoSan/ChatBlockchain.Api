using MediatR;

namespace ChatBlockchain.Application.Users.Command.RegisterPublicKey
{
    public class RegisterPublicKeyCommand : IRequest
    {
        public string PublicKey { get; set; } = string.Empty;
    }
}