using ChatBlockchain.Application.Users.Dtos;
using MediatR;

namespace ChatBlockchain.Application.Users.Queries.GetContacts
{
    public class GetContactsQuery : IRequest<List<ContactDto>>
    {
        public required string Address { get; set; } = string.Empty;
    }
}