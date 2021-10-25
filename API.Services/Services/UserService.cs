using API.Common;
using API.Data.Data;
using API.Data.Entities;
using API.Data.Interfaces;
using API.Data.Model;
using API.Data.Specification;
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
        private readonly IWalletRepository _walletRepo;


        public UserService(IUserRepository userRepo, IUnitOfWork unitOfWork, IWalletRepository walletRepo)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _walletRepo = walletRepo;
        }

        public async Task<Response> ApproveUserAsync(string userId)
        {
            var response = new Response();
            if (userId != null)
            {
                var user = await _userRepo.GetUserAsync(ApplicationUserSpecification.ById(userId));
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        var wallet = new Wallet()
                        {
                            Amount = 0,
                            Created = DateTime.Now,
                            CreatedBy = "Backend",
                            IsActive = true,
                            UserId = user.Id,
                        };
                        var walletEvent = new WalletEvent()
                        {
                            Created = DateTime.Now,
                            CreatedBy = "Backend",
                            EventType = WalletEvent.WalletCreate,
                            IsActive = true,
                            Description = $"New Wallet Has been created by admin for user {user.NormalizedUserName} on {DateTime.Now.ToString("d")}",
                            UserId = user.Id,
                        };
                        wallet.WalletEvents.Add(walletEvent);
                        user.IsActive = true;
                        await _walletRepo.Create(wallet);
                        var result = await _unitOfWork.SaveChangesAsync();
                        if (!result.IsSuccess)
                        {
                            foreach (var error in result.Errors)
                            {
                                response.Errors.Add(error);
                            }
                        }
                    }
                    else
                    {
                        response.Errors.Add("User is already active!");
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
                var user = await _userRepo.GetUserAsync(ApplicationUserSpecification.ById(userId));
                if (user != null)
                {
                    var wallet = await _walletRepo.GetWalletAsync(WalletSpecification.ByUserId(userId));
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
                        Username = user.UserName,
                        WalletId = wallet != null ? wallet.Id : ""
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

            var users = await _userRepo.ListUsersAsync(ApplicationUserSpecification.All());
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
