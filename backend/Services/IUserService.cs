using System.Xml.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {

        /// <summary>
        /// Return data to histogram,from users in database
        /// </summary>
        /// <returns>Dictionary Key-lifetime in days, Value-count of users</returns>
        Task<Dictionary<int, int>> getHistogramData();
        Task createUsers(List<User> users);
        Task<List<User>> getAllUsersAsync();
        /// <summary>
        ///  Рассчитывается и выводится метрика с названием “Rolling Retention 7 day”
        ///  Rolling Retention X day = (количество пользователей, вернувшихся в систему в X-ый день или позже) / (количество пользователей, зарегистрировавшихся в приложении X дней назад или раньше) * 100%
        /// </summary>
        /// <param name="x">Days</param>
        /// <returns></returns>
        Task<int> getRollingRetention(int x = 7);

        Task deleteAllUsers();

    }
}