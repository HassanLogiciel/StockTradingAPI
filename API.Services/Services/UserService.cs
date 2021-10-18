using API.Common;
using API.Data.Model;
using API.Services.Services.Interfaces;
using API.Services.Services.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> ApproveUserAsync(string userId)
        {
            var response = new Response();
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsActive = true;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        if (result.Errors.Any())
                        {
                            foreach (var item in result.Errors)
                            {
                                response.Errors.Add(item.ToString());
                            }
                        }
                        else
                        {
                            response.Errors.Add("Internal Server Error!");
                        }
                    }
                }
                else
                {
                    response.Errors.Add("Please enter the valid user id!");
                }
            }
            else
            {
                response.Errors.Add("Please enter the valid user id!");
            }
            return response;
        }

        public async Task<ResponseObject<UserDto>> GetUserAsync(string userId)
        {
            var response = new ResponseObject<UserDto>();
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var userDto = new UserDto()
                    {
                        Address = user.Address,
                        City = user.City,
                        Country = user.Country,
                        Email = user.Email,
                        IsActive = user.IsActive,
                        Name = user.NormalizedUserName,
                        Phone = user.PhoneNumber,
                        State = user.State,
                        Username = user.UserName
                    };
                    response.RequestedObject = userDto;
                }
                else
                {
                    response.Errors.Add("Please enter the valid user id!");
                }
            }
            else
            {
                response.Errors.Add("Please enter the valid user id!");
            }
            return response;
        }

        public async Task<ResponseObject<List<UserDto>>> ListUserAsync()
        {
            var response = new ResponseObject<List<UserDto>>();

            var users = _userManager.Users;
            if (users.Any())
            {
                var listUsers = await users.Select(c => new UserDto() 
                {
                    Address = c.Address,
                    City = c.City,
                    Country = c.Country,
                    Email = c.Email,
                    IsActive = c.IsActive,
                    Name = c.NormalizedUserName,
                    Phone = c.PhoneNumber,
                    State = c.State,
                    Username = c.UserName
                }).ToListAsync();
                response.RequestedObject = listUsers;
            }
            else
            {
                response.Errors.Add("No User Found!");
            }
            return response;
        }
    }
}
