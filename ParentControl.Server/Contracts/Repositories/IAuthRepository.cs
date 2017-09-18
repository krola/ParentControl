using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ParentControl.Server.Contracts.Repositories
{
    public interface IAuthRepository
    {
        Task<IdentityUser> FindUserById(string userId);
    }
}
