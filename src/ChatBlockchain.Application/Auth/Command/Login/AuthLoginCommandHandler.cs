using ChatBlockchain.Application.Auth.Dtos;
using ChatBlockchain.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChatBlockchain.Application.Auth.Command.Login
{
    public class AuthLoginCommandHandler(
        ICryptoService cryptoService,
        INonceStore nonceStore,
        IJwtService jwtService,
        ILogger<AuthLoginCommandHandler> logger
    ) : IRequestHandler<AuthLoginCommand, AuthLoginDto>
    {
        private readonly ILogger<AuthLoginCommandHandler> _logger = logger;
        private readonly ICryptoService _crypto = cryptoService;
        private readonly IJwtService _jwtService = jwtService;
        private readonly INonceStore _nonceStore = nonceStore;
        public async Task<AuthLoginDto> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Procesando solicitud de login para dirección: {Address}", request.Address);
            if (!_nonceStore.TryGet(request.Address, out var stored) || 
                stored.challenge != request.OriginalChallenge || 
                stored.expiry < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Nonce inválido o expirado");
            }

            _logger.LogInformation("Verificando firma para dirección: {Address}", request.Address);
            if (!_crypto.VerifySignature(request.OriginalChallenge, request.Signature, request.Address))
                throw new UnauthorizedAccessException("Firma inválida");
            
            _nonceStore.Remove(request.Address);
            var token = _jwtService.GenerateToken(request.Address);
            return new AuthLoginDto()
            {
                Token = token,
                Address = request.Address
            };
        }
    }
}