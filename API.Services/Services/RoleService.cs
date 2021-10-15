using API.Common;
using API.Services.Services.Dtos;
using API.Services.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Response> AddRole(RoleDto roleDto)
        {
            var response = new Response();
            try
            {
                if (roleDto != null)
                {
                    var role = new IdentityRole()
                    {
                        Name = roleDto.RoleName,
                    };
                    var isRoleExists = await _roleManager.RoleExistsAsync(role.Name);
                    if (!isRoleExists)
                    {
                        var result = await _roleManager.CreateAsync(role);
                        if (!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                response.Errors.Add(item.Description);
                            }
                        }
                    }
                    else
                    {
                        response.Errors.Add("Role Already Exists!");
                    }
                }
                else
                {
                    response.Errors.Add("Role Name is null");
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}
