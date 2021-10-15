using API.Common;
using Login.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<Response> LoginUser(LoginVm model);
    }
}
