using API.Common;
using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ResponseObject<List<UserDto>>> ListUserAsync();
        public Task<ResponseObject<UserDto>> GetUserAsync(string userId);
        public Task<Response> ApproveUserAsync(string userId);
    }
}
