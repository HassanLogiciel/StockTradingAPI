using API.Common;
using API.Data.Data;
using API.Data.Interfaces;
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
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepo, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> ApproveUserAsync(string userId)
        {
            var response = new Response();
            if (userId != null)
            {
                var user = await _userRepo.GetById(userId);
                if (user != null)
                {
                    user.IsActive = true;
                    await _unitOfWork.SaveChanges();
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
                var user = await _userRepo.GetById(userId);
                if (user != null)
                {
                    var userDto = new UserDto()
                    {
                        Id = user.Id,
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

            var users = await _userRepo.GetAll();
            if (users.Any())
            {
                var listUsers = users.Select(c => new UserDto()
                {
                    Id = c.Id,
                    Address = c.Address,
                    City = c.City,
                    Country = c.Country,
                    Email = c.Email,
                    IsActive = c.IsActive,
                    Name = c.NormalizedUserName,
                    Phone = c.PhoneNumber,
                    State = c.State,
                    Username = c.UserName
                }).ToList();
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
