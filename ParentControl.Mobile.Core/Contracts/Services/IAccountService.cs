using ParentControl.Core.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Core.Contracts.Services
{
    public interface IAccountService
    {
        Task<bool> AuthenticateAsync(string username, string password);
    }
}
