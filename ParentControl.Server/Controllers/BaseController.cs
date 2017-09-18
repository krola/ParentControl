using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Models;

namespace ParentControl.Server.Controllers
{
    public class BaseController : ApiController
    {
        private UserModel _userModel;
        private IUserRepository _userRepository;
        protected UserModel AppUser
        {
            get {
                _userRepository.MarkAsUnchanged(_userModel);
                return _userModel;
            }
        }

        public BaseController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userModel = userRepository.GetAuthorizatedUser(User?.Identity?.GetUserId());
        }
    }
}