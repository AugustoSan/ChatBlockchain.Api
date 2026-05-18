using Microsoft.EntityFrameworkCore;
using ChatBlockchain.Core.Models;
using ChatBlockchain.Core.Interfaces.Repositories;
using ChatBlockchain.Infraestructure.Data;

namespace ChatBlockchain.Infraestructure.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly AppDbContext _context;

        public ContactsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactModel>> ListContactsAsync(string address)
        {
            return await _context.Contacts.Where(c => c.Address == address).ToListAsync();
        }

        public async Task AddContactAsync(string address, ContactModel contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(string address, ContactModel contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(string address, ContactModel contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}