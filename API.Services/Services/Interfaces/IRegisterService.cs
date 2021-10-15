using API.Common;
using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface IRegisterService
    {
        public Task<Response> RegisterUser(UserDto userDto);
    }
}
