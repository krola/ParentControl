﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Mobile.Core.Services.Contracts
{
    public interface IAuthorizationService
    {
        Task<bool> Login(string username, string password);
    }
}
