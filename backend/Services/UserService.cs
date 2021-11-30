using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task createUsers(List<User> users)
        {
            foreach (var user in users)
            {
                if (user.LastActivity < user.Registration || user.Registration > DateTime.Now)
                {
                    throw new ArgumentException("Проверьте правильность введенных дат.");
                }
                if ((await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) != null))
                if ((await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) != null))
                {
                    throw new ArgumentException($"Пользователь с Id:{user.Id} уже существует");
                }
                await _context.Users.AddAsync(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task deleteAllUsers()
        {
            _context.Users.RemoveRange(_context.Users.ToList());
            await _context.SaveChangesAsync();
        }


        public async Task<List<User>> getAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public Task<Dictionary<int, int>> getHistogramData()
        {
            throw new NotImplementedException();
        }

        public Task<int> getRollingRetention(int x = 7)
        {
            throw new NotImplementedException();
        }
    }
}