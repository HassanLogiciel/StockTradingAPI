using API.Common;
using API.Services.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<Response> AddRole(RoleDto roleDto);
    }
}
