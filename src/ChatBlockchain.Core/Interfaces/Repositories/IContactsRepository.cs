using ChatBlockchain.Core.Models;

namespace ChatBlockchain.Core.Interfaces.Repositories
{
    public interface IContactsRepository
    {        
        Task<IEnumerable<ContactModel>> ListContactsAsync(string address);
        Task AddContactAsync(string address, ContactModel contact);
        Task UpdateContactAsync(string address, ContactModel contact);
        Task DeleteContactAsync(string address, ContactModel contact);
    }
}