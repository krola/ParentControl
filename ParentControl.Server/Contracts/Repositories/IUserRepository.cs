using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Server.Models;

namespace ParentControl.Server.Contracts.Repositories
{
    public interface IUserRepository
    {
        UserModel GetAuthorizatedUser(string userId);
        void CreateUser(string name, string userId);
        void MarkAsUnchanged(UserModel user);
    }
}
