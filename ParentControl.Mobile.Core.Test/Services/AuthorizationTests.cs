using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ParentControl.Mobile.Core.Services;
using ParentControl.Mobile.Core.Services.Contracts;

namespace ParentControl.Mobile.Core.Test.Services
{
    [TestFixture]
    public class AuthorizationTests
    {
        private IAuthorizationService _authorizationService;

        [SetUp]
        public void Setup()
        {
            _authorizationService = new AuthorizationService();
        }

        [Test]
        [TestCase("test", "password")]
        public void LoginTests(string login, string password)
        {
            var result = _authorizationService.Login(login, password);
        }
    }
}
