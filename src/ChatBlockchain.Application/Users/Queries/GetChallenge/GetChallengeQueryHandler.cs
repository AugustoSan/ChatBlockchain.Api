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
            var stringMsjRandom = GenerarCadenaAleatoria(20);
            var challenge = stringMsjRandom + nonce;
            _nounceStore.Add(request.Address, nonce, challenge, TimeSpan.FromMinutes(5));
            return Task.FromResult(new ChallengeDto { Challenge = challenge });
        }

        private static string GenerarCadenaAleatoria(int longitud)
        {
            Random random = new Random();
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            
            return new string(Enumerable.Repeat(caracteres, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}