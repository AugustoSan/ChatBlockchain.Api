using MediatR;
using ChatBlockchain.Core.Interfaces.Services;
using System.Security.Cryptography;
using ChatBlockchain.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using ChatBlockchain.Application.Users.Dtos;

namespace ChatBlockchain.Application.Users.Queries.GetContacts
{
    public class GetContactsQueryHandler(IContactsRepository contactsRepository, ILogger<GetContactsQueryHandler> logger) : IRequestHandler<GetContactsQuery, List<ContactDto>>
    {
        private readonly IContactsRepository _contactsRepository = contactsRepository;
        private readonly ILogger<GetContactsQueryHandler> _logger = logger;
        public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo desafío para dirección: {Address}", request.Address);
            if (string.IsNullOrEmpty(request.Address))
                throw new ArgumentException("La dirección no puede estar vacía", nameof(request.Address));

            var contacts = await _contactsRepository.ListContactsAsync(request.Address);
            return contacts.Select(c => new ContactDto
            {
                Address = c.Address,
                PublicKey = c.PublicKey,
                Name = c.Name
            }).ToList();
        }
    }
}