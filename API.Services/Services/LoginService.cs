using API.Common;
using API.Data.Model;
using API.Services.Services.Dtos;
using API.Services.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginService(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }

        public async Task<Response> LoginUser(LoginVm model)
        {
            var response = new Response();
            try
            {
                if (model != null)
                {
                    var applicationUser = await _userManger.FindByNameAsync(model.Username);
                    if (applicationUser == null)
                    {
                        response.Errors.Add("Invalid Username or Password!");
                    }
                    else
                    {
                        if (applicationUser.IsActive)
                        {
                            var authenticateUser = await _signInManager.CheckPasswordSignInAsync(applicationUser, model.Password, false);
                            if (!authenticateUser.Succeeded)
                            {
                                response.Errors.Add("Invalid Username or Password!");
                            }
                            else
                            {
                                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                                string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                                configurationBuilder.AddJsonFile(path, false);
                                IConfigurationRoot root = configurationBuilder.Build();
                                IConfigurationSection configurationServerUrl = root.GetSection("IdentityServerUrl");
                                IConfigurationSection configurationNormalUserScopes = root.GetSection("NormalUserScopes");
                                IConfigurationSection configurationAdminScopes = root.GetSection("AdminScopes");

                                var identityServerUrl = configurationServerUrl.Value;
                                var clientId = model.ClientId;
                                var clientSecret = model.ClientSecret;
                                var adminScopes = configurationAdminScopes.Value;
                                var normalUserScopes = configurationNormalUserScopes.Value;
                                using (var cleint = new HttpClient())
                                {
                                    var discoveryDocument = await cleint.GetDiscoveryDocumentAsync(identityServerUrl);
                                    if (discoveryDocument.IsError)
                                    {
                                        if (!string.IsNullOrEmpty(discoveryDocument.Error))
                                        {
                                            response.Errors.Add(discoveryDocument.Json.ToString());
                                        }
                                        else
                                        {
                                            response.Errors.Add("Internal Server Error!");
                                        }
                                    }
                                    else
                                    {
                                        var role = await _userManger.GetRolesAsync(applicationUser);
                                        if (role.Any())
                                        {
                                            var passwordTokenRequest = new PasswordTokenRequest()
                                            {
                                                ClientId = clientId,
                                                ClientSecret = clientSecret,
                                                Password = model.Password,
                                                UserName = model.Username,
                                                RequestUri = new Uri(discoveryDocument.TokenEndpoint),
                                            };
                                            if (role.Contains("Admin"))
                                            {
                                                passwordTokenRequest.Scope = adminScopes;//"UserApi";
                                            }
                                            else if (role.Contains("NormalUser"))
                                            {
                                                passwordTokenRequest.Scope = normalUserScopes; //"tradingAppLoginAPI";
                                            }
                                            var tokenResponse = await cleint.RequestPasswordTokenAsync(passwordTokenRequest);
                                            if (tokenResponse.IsError)
                                            {
                                                response.Errors.Add(tokenResponse.Error);
                                            }
                                            else
                                            {
                                                var token = tokenResponse.AccessToken;
                                                if (!string.IsNullOrEmpty(token))
                                                {
                                                    var loginDto = new LoginDto()
                                                    {
                                                        AccessToken = token,
                                                        Expiration = tokenResponse.ExpiresIn,
                                                        UserId = applicationUser.Id
                                                    };
                                                    response.AdditionalData = JsonConvert.SerializeObject(loginDto);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            response.Errors.Add("User does not have any roles!");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            response.Errors.Add("Your Account Is not active!");
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
