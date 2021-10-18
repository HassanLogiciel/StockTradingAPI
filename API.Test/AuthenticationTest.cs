using API.Common;
using API.Services.Services.Model;
using API.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.Test
{
    [TestClass]
    public class AuthenticationTest
    {

        [TestMethod]
        public async Task Register()
        {
            var responseRequest = new List<KeyValuePair<UserVm, IRestResponse>>();
            for (int i = 0; i < 10000; i++)
            {
                var client = new RestClient("https://localhost:44352/api/Register");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = new UserVm()
                {
                    Name = $"Test {i}",
                    Address = $"Test Address {i}",
                    City = $"Test City {i}",
                    ConfirmPassword = $"Prometheus-X1",
                    Password = $"Prometheus-X1",
                    Country = $"Test Country {i}",
                    Email = $"TestEmail{i}@gmail.com",
                    Phone = $"090075860{i}",
                    State = $"Test State {i}",
                    Username = $"TestUsername{i}",
                };
                request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteAsync(request);
                responseRequest.Add(new KeyValuePair<UserVm, IRestResponse>(body, response));
            }
            string directory = Path.Combine(@"F:\Code\StockTrading\StockTradingAPI\API.Test\TestResults\Register");
            string file = $"/registerTest_{DateTime.Now.ToString("dd_mm_yyyy_hh_mm_ss_tt")}.txt";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            foreach (var item in responseRequest)
            {
                //File.AppendAllText(directory + file, $"Testasdsa ad asd asd asd");
                File.AppendAllText(directory + file, $"Request {Environment.NewLine} {JsonConvert.SerializeObject(item.Key)} {Environment.NewLine} Status Code: {item.Value.StatusCode} {Environment.NewLine} Response: {Environment.NewLine} {item.Value.Content} {Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
            }
        }

        [TestMethod]
        public async Task Login()
        {
            var responseRequest = new List<KeyValuePair<LoginVm, IRestResponse>>();
            for (int i = 0; i < 10000; i++)
            {
                var client = new RestClient("https://localhost:44317/api/login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = new LoginVm()
                {
                    Password = "Prometheus-X1",
                    Username = $"TestUsername{i}"
                };
                request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteAsync(request);
                responseRequest.Add(new KeyValuePair<LoginVm, IRestResponse>(body, response));
            }
            string directory = Path.Combine(@"F:\Code\StockTrading\StockTradingAPI\API.Test\TestResults\Login");
            string file = $"/loginTest_{DateTime.Now.ToString("dd_mm_yyyy_hh_mm_ss_tt")}.txt";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            foreach (var item in responseRequest)
            {
                //File.AppendAllText(directory + file, $"Testasdsa ad asd asd asd");
               await File.AppendAllTextAsync(directory + file, $"Request {Environment.NewLine} {JsonConvert.SerializeObject(item.Key)} {Environment.NewLine} Status Code: {item.Value.StatusCode} {Environment.NewLine} Response: {Environment.NewLine} {item.Value.Content} {Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
            }
        }
    }
}
