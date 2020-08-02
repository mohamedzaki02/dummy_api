using System;
using System.Threading.Tasks;
using DatingApp.Data;
using DatingApp.Helpers;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            if (user == null) return null;
            return PasswordHelper.Verify(password, user.PasswordHash , user.PasswordSalt)? user : null;
        }

        public async Task<User> Register(User newuser, string password)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHelper.Compute(password, out passwordHash, out passwordSalt);
            newuser.PasswordHash = passwordHash;
            newuser.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(newuser);
            await _context.SaveChangesAsync();
            return newuser;
        }

    
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }
}