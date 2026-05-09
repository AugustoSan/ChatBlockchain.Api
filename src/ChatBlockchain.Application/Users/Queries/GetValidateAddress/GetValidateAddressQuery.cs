using ChatBlockchain.Application.Users.Dtos;
using MediatR;

namespace ChatBlockchain.Application.Users.Queries.GetValidateAddress
{
    public class GetValidateAddressQuery : IRequest<PublicKeyDto>
    {
        public required string Address { get; set; } = string.Empty;
    }
}