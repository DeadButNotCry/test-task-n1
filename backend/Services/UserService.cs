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

        public async Task CreateUsers(List<User> users)
        {
            foreach (var user in users)
            {
                // if (user.LastActivity < user.Registration || user.Registration > DateTime.Now)
                // {
                //     throw new ArgumentException("Проверьте правильность введенных дат.");
                // }
                if ((await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) != null))
                {
                    throw new ArgumentException($"Пользователь с Id:{user.Id} уже существует");
                }
                await _context.Users.AddAsync(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllUsers()
        {
            _context.Users.RemoveRange(_context.Users.ToList());
            await _context.SaveChangesAsync();
        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetHistogramData()
        {
            var histogramData = new Dictionary<int, int>();
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                if (histogramData.ContainsKey(user.LifeTime))
                {
                    histogramData[user.LifeTime]++;
                }
                else
                {
                    histogramData[user.LifeTime] = 1;
                }
            }
            return histogramData;
        }

        public double GetRollingRetention(int x = 7)
        {
            var countOfReturnedUsers = _context.Users.Where(u => u.LastActivity - u.Registration >= TimeSpan.FromDays(x)).Count();
            var countOfRegUsers = _context.Users.Where(u => DateTime.Now - u.Registration >= TimeSpan.FromDays(x)).Count();
            if (countOfRegUsers == 0)
            {
                return 0.0;
            }
            var result = ((double)countOfReturnedUsers / countOfRegUsers) * 100;

            return Math.Round(result, 3);
        }
    }
}