using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MampirGanApp.Models;
using MampirGanApp.Seeder;

namespace MampirGanApp.Services
{
    public class AuthService
    {
        //private static List<User> _users = new List<User>();
        private static List<User> _users = UserSeeder.SeedUsers();

        public bool Register(string email, string username, string password,  UserRole role = UserRole.Customer)
        {

            //if (_users.Any(u => u.Email == email || u.Username == username))
            //    return false;

            //if (role == UserRole.Admin)
            //    throw new InvalidOperationException("Admin tidak bisa register.");

            // Precondition
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Data tidak boleh kosong.");

            if (_users.Any(u => u.Email == email || u.Username == username))
                return false;
                
            User newUser = new User
            {
                UserID = _users.Count + 1,
                Email = email,
                Username = username,
                Password = password,
                Role = UserRole.Customer 
            };

            _users.Add(newUser);
            return true;

        }

        public User? Login(string usernameOrEmail, string password)
        {
            // Pre condition
            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username/email dan password tidak boleh kosong.");

            var user = _users.FirstOrDefault(u =>
                 (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password);

            return user;
        }

        public List<User> GetAllUsers() => _users;
    }
}
