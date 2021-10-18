using API.Common;
using API.Data.Data;
using API.Data.Model;
using API.Services.Services.Interfaces;
using API.Services.Services.Model;
using API.Services.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterService(IdentityContext identityContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _identityContext = identityContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Response> RegisterUser(UserVm userDto)
        {
            var response = new Response();
            try
            {
                if (userDto != null)
                {
                    var user = new ApplicationUser()
                    {
                        Address = userDto.Address,
                        City = userDto.City,
                        Email = userDto.Email,
                        PhoneNumber = userDto.Phone,
                        UserName = userDto.Username,
                        State = userDto.State,
                        Country = userDto.Country,
                        NormalizedUserName = userDto.Name
                    };
                    var result = await _userManager.CreateAsync(user, userDto.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            response.Errors.Add(item.Description);
                        }
                    }
                    else
                    {
                        var addUserToRole = await _userManager.AddToRoleAsync(user, "NormalUser");
                        if (!addUserToRole.Succeeded)
                        {
                            response.Errors.Add(string.Join(',', result.Errors));
                        }

                        var addClaim = await _userManager.AddClaimAsync(user, new Claim("RoleType","NormalUser"));
                        if (!addClaim.Succeeded)
                        {
                            response.Errors.Add(string.Join(',', result.Errors));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
            }
            return response;
        }
    }
}
