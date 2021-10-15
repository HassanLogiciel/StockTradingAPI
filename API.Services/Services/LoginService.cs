using API.Common;
using API.Data.Model;
using API.Services.Services.Dtos;
using API.Services.Services.Interfaces;
using IdentityModel.Client;
using Login.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                                IConfigurationSection configurationClientId = root.GetSection("ClientId");
                                IConfigurationSection configurationSecret = root.GetSection("ClientSecret");

                                var identityServerUrl = configurationServerUrl.Value;
                                var clientId = configurationClientId.Value;
                                var clientSecret = configurationSecret.Value;
                                using (var cleint = new HttpClient())
                                {
                                    var discoveryDocument = await cleint.GetDiscoveryDocumentAsync(identityServerUrl);
                                    if (discoveryDocument.IsError)
                                    {
                                        response.Errors.Add(discoveryDocument.Json.ToString());
                                    }
                                    else
                                    {
                                        var tokenResponse = await cleint.RequestPasswordTokenAsync(new PasswordTokenRequest()
                                        {
                                            ClientId = clientId,
                                            ClientSecret = clientSecret,
                                            Password = model.Password,
                                            UserName = model.Username,
                                            RequestUri = new Uri(discoveryDocument.TokenEndpoint),
                                            Scope = "tradingAppLoginAPI",
                                        });
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
