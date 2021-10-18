using API.Common;
using API.Data.Model;
using API.Services.Services.Dtos;
using API.Services.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Service.ViewModels;
using System;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class ClaimService : IClaimService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Response> AddClaim(ClaimVm model)
        {
            var response = new Response();
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var addClaim = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(model.Type, model.Value));
                    if (!addClaim.Succeeded)
                    {
                        foreach (var item in addClaim.Errors)
                        {
                            response.Errors.Add(item.Description);
                        }
                    }
                }
                else
                {
                    response.Errors.Add("No User Found With This User Id!");
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
