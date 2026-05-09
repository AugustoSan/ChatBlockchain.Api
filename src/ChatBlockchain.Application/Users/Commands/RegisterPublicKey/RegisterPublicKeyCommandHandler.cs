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
            _logger.LogInformation("Iniciando registro de clave pública");
            string address = _cryptoService.GetAddressFromPublicKey(request.PublicKey);
            _logger.LogInformation("Registrando clave pública para dirección: {Address}", address);
            _logger.LogInformation("Verificando clave pública: {PublicKey}", request.PublicKey);
            var exist = await _userRepository.GetuserByPublicKeyAsync(address);
            _logger.LogInformation("Verificando si el usuario ya existe para la dirección: {Address}", address);
            if (exist != null)
            {
                throw new InvalidOperationException("User already exists");
            }
            await _userRepository.AddUserAsync(new UserModel { Address = address, PublicKeyHex = request.PublicKey });
            return;

        }
    }
}