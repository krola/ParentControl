using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ParentControl.Server.Contracts;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Models;
using ParentControl.Server.Services;

namespace ParentControl.Server.Repository
{
    public class UserRepository : IUserRepository
    {
        private IAppContext _appContext;

        public UserRepository(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public void CreateUser(string name, string userId)
        {
            if (_appContext.Users.Any(u => u.UserId == userId))
            {
                throw new Exception("User already exists");
            }

            _appContext.Users.Add(new UserModel()
            {
                UserId = userId,
                Name = name
            });

            _appContext.SaveChanges();
        }

        public void MarkAsUnchanged(UserModel user)
        {
            //_appContext.Entry(user).State = EntityState.Unchanged;
        }

        public UserModel GetAuthorizatedUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var result = _appContext.Users.FirstOrDefault(u => u.UserId == userId);
            return result;
        }
    }
}