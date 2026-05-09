using ChatBlockchain.Core.Models;

namespace ChatBlockchain.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserModel user);
        Task<UserModel?> GetuserByIdAsync(int id);
        Task<UserModel?> GetuserByAddressAsync(string address);
        Task<UserModel?> GetuserByUsernameAsync(string username);
        Task<UserModel?> GetuserByPublicKeyAsync(string publicKey);
        Task<IEnumerable<UserModel>> ListUsersAsync();
        Task<bool> UserExistsAsync(int id);
    }
}