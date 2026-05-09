using ChatBlockchain.Core.Interfaces.Repositories;
using ChatBlockchain.Core.Interfaces.Services;
using ChatBlockchain.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChatBlockchain.Application.Users.Command.RegisterPublicKey
{
    public class RegisterPublicKeyCommandHandler(IUserRepository userRepository, ICryptoService cryptoService, ILogger<RegisterPublicKeyCommandHandler> logger) : IRequestHandler<RegisterPublicKeyCommand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICryptoService _cryptoService = cryptoService;
        private readonly ILogger<RegisterPublicKeyCommandHandler> _logger = logger;
        public async Task Handle(RegisterPublicKeyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando registro de clave pública: {PublicKey}", request.PublicKey);
            var exist = await _userRepository.GetuserByPublicKeyAsync(request.PublicKey);
            if (exist != null)
            {
                return;
            }
            var address = _cryptoService.GetAddressFromPublicKey(request.PublicKey);
            await _userRepository.AddUserAsync(new UserModel { Address = address, PublicKeyHex = request.PublicKey });
            return;

        }
    }
}