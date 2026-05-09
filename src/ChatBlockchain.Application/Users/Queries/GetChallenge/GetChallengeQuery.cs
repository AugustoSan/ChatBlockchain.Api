using ChatBlockchain.Application.Users.Dtos;
using MediatR;

namespace ChatBlockchain.Application.Users.Queries.GetChallenge
{
    public class GetChallengeQuery : IRequest<ChallengeDto>
    {
        public required string Address { get; set; } = string.Empty;
    }
}