using MampirGanApp.Models;
using MampirGanApp.Services;
using MampirGanApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Controllers
{
    public class AuthController
    {
        private readonly AuthService _authService;
        private readonly AuthView _authView;

        public AuthController()
        {
            _authService = new AuthService();
            _authView = new AuthView();
        }

            public void Register()
        {
            var (email, username, password) = _authView.GetRegisterInput();
            bool success = _authService.Register(email, username, password);
            _authView.ShowRegisterResult(success);
        }

            public User? Login()
        {
            var (usernameOrEmail, password) = _authView.GetLoginInput();
            var user = _authService.Login(usernameOrEmail, password);
            _authView.ShowLoginResult(user);
            return user;
        }
        }
    }
