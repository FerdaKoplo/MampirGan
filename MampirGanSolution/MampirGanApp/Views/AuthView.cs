using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MampirGanApp.Models;


namespace MampirGanApp.Views
{
    public class AuthView
    {
        public (string email, string username, string password) GetRegisterInput()
        {
            Console.WriteLine(" == REGISTER PAGE ==");

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            return (email, username, password);
        }

        public void ShowRegisterResult(bool success)
        {
            if (success)
                Console.WriteLine("Register berhasil!\n");
            else
                Console.WriteLine("Email atau Username sudah terdaftar.\n");
        }

        //public void ShowRegisterResult(Action registerAction)
        //{
        //    try
        //    {
        //        registerAction();
        //        Console.WriteLine("Register berhasil!\n");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Register gagal: {ex.Message}\n");
        //    }
        //}

        public (string usernameOrEmail, string password) GetLoginInput()
        {
            Console.WriteLine(" == LOGIN PAGE ==");

            Console.Write("Username atau Email: ");
            string usernameOrEmail = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            return (usernameOrEmail, password);
        }

        public void ShowLoginResult(User? user)
        {
            if (user != null)
                Console.WriteLine($"Login Berhasi!. Selamat Datang, {user.Username}! (Role: {user.Role})\n");
            else
                Console.WriteLine("Login gagal. Username/Email atau password salah.\n");
        }
    }
}
