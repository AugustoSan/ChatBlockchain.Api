using MediatR;

namespace ChatBlockchain.Application.Users.Queries.GetValidateAddress
{
    public class GetValidateAddressQuery : IRequest<string>
    {
        public required string Address { get; set; } = string.Empty;
    }
}