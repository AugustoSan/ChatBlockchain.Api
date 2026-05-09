using MediatR;
using ChatBlockchain.Core.Interfaces.Services;
using System.Security.Cryptography;
using ChatBlockchain.Application.Users.Dtos;

namespace ChatBlockchain.Application.Users.Queries.GetChallenge
{
    public class GetChallengeQueryHandler(INonceStore nounceStore) : IRequestHandler<GetChallengeQuery, ChallengeDto>
    {
        private readonly INonceStore _nounceStore = nounceStore;
        public Task<ChallengeDto> Handle(GetChallengeQuery request, CancellationToken cancellationToken)
        {
            var nonce = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
            var challengeMsg = $"Inicia sesión en ChatBlockchain con nonce: {nonce}";
            _nounceStore.Add(request.Address, nonce, challengeMsg, TimeSpan.FromMinutes(5));
            return Task.FromResult(new ChallengeDto { Challenge = challengeMsg });
        }
    }
}