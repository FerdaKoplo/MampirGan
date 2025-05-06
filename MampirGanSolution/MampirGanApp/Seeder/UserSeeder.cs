using System;
using System.Collections.Generic;
using MampirGanApp.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Seeder
{
    public static class UserSeeder
    {
        public static List<User> SeedUsers()
        {
            return new List<User>
            {
                new User
                {
                    UserID = 1,
                    Email = "admin@gmail.com",
                    Username = "admin",
                    Password = "admin123", 
                    Role = UserRole.Admin
                }
            };
        }
    }
}
