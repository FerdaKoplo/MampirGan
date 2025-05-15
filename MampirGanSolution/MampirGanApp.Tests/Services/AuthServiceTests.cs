

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MampirGanApp.Services;

namespace MampirGanApp.Tests.Services
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void Register_ValidUser_ReturnsTrue()
        {
            var service = new AuthService();
            var result = service.Register("test@mail.com", "testuser", "pass123");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Register_DuplicateEmail_ReturnsFalse()
        {
            var service = new AuthService();
            service.Register("same@mail.com", "user1", "123");
            var result = service.Register("same@mail.com", "user2", "456");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsUser()
        {
            var service = new AuthService();
            service.Register("user@mail.com", "user", "abc123");
            var user = service.Login("user", "abc123");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void Login_InvalidPassword_ReturnsNull()
        {
            var service = new AuthService();
            service.Register("wrong@mail.com", "user", "abc123");
            var user = service.Login("user", "wrongpass");
            Assert.IsNull(user);
        }
    }
}