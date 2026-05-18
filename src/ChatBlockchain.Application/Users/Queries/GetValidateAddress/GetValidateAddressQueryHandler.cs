using MediatR;
using ChatBlockchain.Core.Interfaces.Services;
using System.Security.Cryptography;
using ChatBlockchain.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using ChatBlockchain.Application.Users.Dtos;

namespace ChatBlockchain.Application.Users.Queries.GetValidateAddress
{
    public class GetValidateAddressQueryHandler(
        IUserRepository userRepository, ILogger<GetValidateAddressQueryHandler> logger) : IRequestHandler<GetValidateAddressQuery, PublicKeyDto>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<GetValidateAddressQueryHandler> _logger = logger;
        public async Task<PublicKeyDto> Handle(GetValidateAddressQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo desafío para dirección: {Address}", request.Address);
            var user = await _userRepository.GetuserByAddressAsync(request.Address);
            if (user == null)
                throw new InvalidOperationException("Direccion no encontrada");

            return new PublicKeyDto { PublicKey = user.PublicKeyHex };
        }
    }
}