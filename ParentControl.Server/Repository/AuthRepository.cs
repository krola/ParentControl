using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.DTO;
using ParentControl.Server.Models;
using ParentControl.Server.Services;

public class AuthRepository : IDisposable, IAuthRepository
{
    private AuthContext _ctx;

    private UserManager<IdentityUser> _userManager;
    private readonly IUserRepository _userRepository;

    public AuthRepository(IUserRepository userRepository)
    {
        _ctx = new AuthContext();
        _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        _userRepository = userRepository;
    }

    public async Task<IdentityResult> RegisterUser(UserDTO userDto)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = userDto.UserName
        };

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            user = await FindUser(userDto.UserName, userDto.Password);
            _userRepository.CreateUser(userDto.UserName, user.Id);
        }
       

        return result;
    }

    public async Task<IdentityUser> FindUserById(string userId)
    {
        IdentityUser user = await _userManager.FindByIdAsync(userId);

        return user;
    }

    public async Task<IdentityUser> FindUser(string userName, string password)
    {
        IdentityUser user = await _userManager.FindAsync(userName, password);

        return user;
    }

    public void Dispose()
    {
        _ctx.Dispose();
        _userManager.Dispose();

    }
}