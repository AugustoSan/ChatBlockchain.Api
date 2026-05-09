using Microsoft.EntityFrameworkCore;
using ChatBlockchain.Core.Models;
using ChatBlockchain.Core.Interfaces.Repositories;
using ChatBlockchain.Infraestructure.Data;

namespace ChatBlockchain.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel?> GetuserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<UserModel?> GetuserByAddressAsync(string address)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Address == address);
        }

        public async Task<UserModel?> GetuserByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).ToListAsync();
            return user.FirstOrDefault();
        }

        public async Task<UserModel?> GetuserByPublicKeyAsync(string publicKey)
        {
            var user = _context.Users.Where(u => u.PublicKeyHex == publicKey);
            return user.FirstOrDefault();
        }

        public async Task<IEnumerable<UserModel>> ListUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}